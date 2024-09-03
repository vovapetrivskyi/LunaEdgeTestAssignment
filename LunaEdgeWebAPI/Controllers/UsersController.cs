using LunaEdgeRepositoryLayer.Models;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LunaEdgeWebAPI.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class UsersController : Controller
	{
		private IUserService _userService;
		private ITokenService _tokenService;
		private IPasswordService _passwordService;

		public UsersController(IUserService userService, ITokenService tokenService, IPasswordService passwordService)
		{
			_userService = userService;
			_tokenService = tokenService;
			_passwordService = passwordService;
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegistrationRequest request)
		{
			var salt = _passwordService.GenerateSalt();

			var user = new User
			{
				Id = Guid.NewGuid(),
				Username = request.Username,
				Email = request.Email,
				PasswordHash = _passwordService.HashPassword(request.Password, salt),
				PasswordSalt = Convert.ToBase64String(salt),
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			await _userService.SaveUser(user);

			var token = _tokenService.CreateToken(user);

			return Ok(new AuthResponse { Username = user.Username, Token = token });
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest request)
		{
			User? user = await _userService.FindByLoginAsync(request.Login);

			if (user == null)
			{
				return Unauthorized("User not found");
			}

			var result = _passwordService.VerifyPassword(user.PasswordHash, request.Password, Convert.FromBase64String(user.PasswordSalt));

			if (!result)
			{
				return Unauthorized("Invalid credentials");
			}

			// Generate token
			var token = _tokenService.CreateToken(user);

			// Return the token
			return Ok(new AuthResponse { Username = user.Username, Token = token });
		}

		[HttpPost]
		[Authorize]
		public IActionResult Test()
		{
			return Ok();
		}
	}
}
