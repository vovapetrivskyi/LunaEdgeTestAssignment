using LunaEdgeServiceLayer.Data.Models;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUnitOfwork _unitOfWork;
		IUserRepository _repository;

		public UserService(IUnitOfwork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_repository = new UserRepository(_unitOfWork);
		}

		public async Task<User> FindByLoginAsync(string login)
		{
			return await _repository.FindByLoginAsync(login);
		}

		public async Task<ActionResult<User>> SaveUser(User user)
		{
			return await _repository.Create(user);
		}
	}
}
