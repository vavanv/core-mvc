using Microsoft.Extensions.Caching.Memory;
using Moq;
using WebMVC.Data.Repositories;
using WebMVC.Models;
using WebMVC.Services;
using Xunit;

namespace WebMVC.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly IMemoryCache _cache;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _cache = new MemoryCache(new MemoryCacheOptions());
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_ExistingUser_ReturnsUser()
        {
            // Arrange
            var expectedUser = new User { Id = 1, Email = "test@example.com", FirstName = "John", LastName = "Doe" };
            _mockUserRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetUserByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByIdAsync_NonExistingUser_ReturnsNull()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((User?)null);

            // Act
            var result = await _userService.GetUserByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateUserAsync_ValidUser_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User
            {
                Email = "new@example.com",
                PasswordHash = "password123",
                FirstName = "Jane",
                LastName = "Smith"
            };
            var expectedUser = new User
            {
                Id = 1,
                Email = "new@example.com",
                PasswordHash = "hashedpassword",
                FirstName = "Jane",
                LastName = "Smith",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            _mockUserRepository.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.CreateUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Email, result.Email);
            Assert.True(result.IsActive);
            Assert.NotEqual(default(DateTime), result.CreatedAt);
        }

        [Fact]
        public async Task AuthenticateUserAsync_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                IsActive = true
            };
            _mockUserRepository.Setup(r => r.GetByEmailAndPasswordAsync("test@example.com", It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.AuthenticateUserAsync("test@example.com", "password123");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AuthenticateUserAsync_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetByEmailAndPasswordAsync("test@example.com", It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _userService.AuthenticateUserAsync("test@example.com", "wrongpassword");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task EmailExistsAsync_ExistingEmail_ReturnsTrue()
        {
            // Arrange
            var user = new User { Id = 1, Email = "test@example.com" };
            _mockUserRepository.Setup(r => r.GetByEmailAsync("test@example.com")).ReturnsAsync(user);

            // Act
            var result = await _userService.EmailExistsAsync("test@example.com");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task EmailExistsAsync_NonExistingEmail_ReturnsFalse()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetByEmailAsync("nonexistent@example.com")).ReturnsAsync((User?)null);

            // Act
            var result = await _userService.EmailExistsAsync("nonexistent@example.com");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateLastLoginAsync_ExistingUser_UpdatesLastLogin()
        {
            // Arrange
            var user = new User { Id = 1, Email = "test@example.com" };
            _mockUserRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            // Act
            await _userService.UpdateLastLoginAsync(1);

            // Assert
            _mockUserRepository.Verify(r => r.UpdateLastLoginAsync(1), Times.Once);
        }
    }
}