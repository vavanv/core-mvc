using WebMVC.Models;

namespace WebMVC.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByEmailAndPasswordAsync(string email, string passwordHash);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task UpdateLastLoginAsync(int userId);
    }
}