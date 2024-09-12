using LunaEdgeServiceLayer.Data.Models;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Implementations
{
	/// <summary>
	/// Service for operations with users
	/// </summary>
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		public IUserRepository _repository;

		public UserService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_repository = new UserRepository(_unitOfWork);
		}

		/// <summary>
		/// Find user by uesrname or email
		/// </summary>
		/// <param name="login">Username or email</param>
		/// <returns>Existing user or null</returns>
		public async Task<User> FindByLoginAsync(string login)
		{
			return await _repository.FindByLoginAsync(login);
		}

		/// <summary>
		/// Add new user
		/// </summary>
		/// <param name="user">New user</param>
		/// <returns>Saved users</returns>
		public async Task<ActionResult<User>> SaveUser(User user)
		{
			return await _repository.Create(user);
		}
	}
}
