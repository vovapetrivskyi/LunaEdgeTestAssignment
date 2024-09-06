using LunaEdgeServiceLayer.Data.Models;
using LunaEdgeServiceLayer.Interfaces;
using LunaEdgeWebAPI.Models.Requests;
using LunaEdgeWebAPI.Models.Responses;
using Microsoft.AspNetCore.Mvc;

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

		/// <summary>
		/// Register(crate) new user
		/// </summary>
		/// <param name="request">Create user DTO</param>
		/// <returns>Username and JWT token</returns>
		[HttpPost]
		public async Task<IActionResult> Register(RegistrationRequest request)
		{
			if (ModelState.IsValid)
			{
				User existingUser = await _userService.FindByLoginAsync(request.Username);

				if (existingUser != null) 
				{
					return BadRequest("User with same username is exist");
				}

				existingUser = await _userService.FindByLoginAsync(request.Email);

				if (existingUser != null)
				{
					return BadRequest("User with same email is exist");
				}

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
			else
			{ 
				return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
			}
		}

		/// <summary>
		/// Login exist user
		/// </summary>
		/// <param name="request">Username and password</param>
		/// <returns>Username and JWT token</returns>
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

			var token = _tokenService.CreateToken(user);

			return Ok(new AuthResponse { Username = user.Username, Token = token });
		}
	}
}
