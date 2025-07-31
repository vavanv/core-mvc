using System.Security.Cryptography;
using System.Text;
using WebMVC.Data.Repositories;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _userRepository.GetActiveUsersAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            // Hash the password before saving
            user.PasswordHash = HashPassword(user.PasswordHash);
            user.CreatedAt = DateTime.UtcNow;
            user.IsActive = true;

            return await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
                throw new ArgumentException("User not found");

            // Only hash password if it has changed
            if (user.PasswordHash != existingUser.PasswordHash)
            {
                user.PasswordHash = HashPassword(user.PasswordHash);
            }

            return await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = await _userRepository.GetByEmailAndPasswordAsync(email, hashedPassword);
            return user != null;
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            await _userRepository.UpdateLastLoginAsync(userId);
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _userRepository.ExistsAsync(id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}