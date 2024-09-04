﻿using LunaEdgeServiceLayer.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface IUserService
	{
		Task<ActionResult<User>> SaveUser(User user);
		Task<User> FindByLoginAsync(string login);
	}
}
