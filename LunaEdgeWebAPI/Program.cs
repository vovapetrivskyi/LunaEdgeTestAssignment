using LunaEdgeServiceLayer.Data;
using LunaEdgeServiceLayer.Implementations;
using LunaEdgeServiceLayer.Interfaces;
using LunaEdgeWebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace LunaEdgeWebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Configure Serilog with two sinks: one for requests/responses and another for errors
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build())
				.CreateLogger();

			// Add Serilog
			builder.Host.UseSerilog();

			builder.Services.AddControllers();
			
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new() { Title = "Test API", Version = "v1" });

				// Define the OAuth2.0 scheme that's in use (i.e., Implicit Flow)
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						new List<string>()
					}
				});
			});

			// Add JWT
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
				};
			});

			// Inject services
			builder.Services.AddScoped<ITokenService, TokenService>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<ITaskService, TaskService>();
			builder.Services.AddTransient<IPasswordService, PasswordService>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			// Add to have HttpContext access
			builder.Services.AddHttpContextAccessor();

			// Add DbContext
			var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
			builder.Services.AddDbContext<AppDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllers();

			// Add own middleware
			app.UseMiddleware<LoggingMiddleware>();

			app.Run();
		}
	}
}
