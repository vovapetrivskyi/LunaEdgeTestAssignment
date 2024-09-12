using LunaEdgeServiceLayer.Data;
using LunaEdgeServiceLayer.Implementations;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LunaEdgeServiceLayer.Tests
{
	public class UserServiceTests
	{
		private AppDBContext _context;
		private IUnitOfWork _unitOfWork;
		private IUserService _userService;

		public UserServiceTests()
		{
			var options = new DbContextOptionsBuilder<AppDBContext>()
							.UseInMemoryDatabase(databaseName: "TestDatabase")
							.Options;

			_context = new AppDBContext(options, new ConfigurationBuilder().Build());
			_unitOfWork = new UnitOfWork(_context);
			_userService = new UserService(_unitOfWork);

			// Seed the in-memory database with test data
			_context.Users.Add(new Data.Models.User { Id = Guid.NewGuid(), Username = "User1", Email = "user1@example.com", PasswordHash = "", PasswordSalt = "" });
			_context.Users.Add(new Data.Models.User { Id = Guid.NewGuid(), Username = "User2", Email = "user2@example.com", PasswordHash = "", PasswordSalt = "" });
			_context.SaveChanges();
		}

		[Fact]
		public async Task FindByLoginAsync_UserExists_ReturnsUser()
		{
			// Act
			var result = await _userService.FindByLoginAsync("User1");

			// Assert
			Assert.NotNull(result);
			Assert.Equal("User1", result.Username);
		}

		[Fact]
		public async Task FindByLoginAsync_UserDoesNotExist_ReturnsNull()
		{
			// Act
			var result = await _userService.FindByLoginAsync("NonExistentUser");

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public async Task SaveUser_ReturnsSavedUser()
		{
			//Ararnge
			string newUserName = "NewUser";

			var newUser = new Data.Models.User()
			{
				Id = Guid.NewGuid(),
				Username = newUserName,
				PasswordSalt = "",
				PasswordHash = "",
				Email = "mail@mail.mail",
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			// Act
			var result = (await _userService.SaveUser(newUser)).Value;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(newUserName, result.Username);
		}
	}
}
