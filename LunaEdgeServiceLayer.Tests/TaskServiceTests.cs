using LunaEdgeServiceLayer.Data;
using LunaEdgeServiceLayer.Implementations;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaEdgeServiceLayer.Tests
{
	public class TaskServiceTests
	{
		private AppDBContext _context;
		private IUnitOfWork _unitOfWork;
		private ITaskService _taskService;

		private readonly Guid UserGID = Guid.NewGuid();
		private readonly Guid TaskGID = Guid.NewGuid();
		private readonly Guid UnexistTask = Guid.NewGuid();
		private readonly Guid UnexistUser = Guid.NewGuid();

		public TaskServiceTests()
		{
			var options = new DbContextOptionsBuilder<AppDBContext>()
							.UseInMemoryDatabase(databaseName: "TestDatabase")
							.Options;

			_context = new AppDBContext(options, new ConfigurationBuilder().Build());
			_unitOfWork = new UnitOfWork(_context);
			_taskService = new TaskService(_unitOfWork);

			// Seed the in-memory database with test data
			_context.Users.Add(new Data.Models.User
			{
				Id = UserGID,
				Username = "User1",
				Email = "user1@example.com",
				PasswordHash = "",
				PasswordSalt = ""
			});

			_context.Tasks.Add(new Data.Models.Task
			{
				Id = TaskGID,
				Description = "Description",
				Priority = Data.Models.Enums.TaskPriorityEnum.Low,
				Status = Data.Models.Enums.TaskStatusEnum.Pending,
				Title = "Title",
				UserId = UserGID,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			});
			_context.SaveChanges();
		}

		[Fact]
		public async Task CreateNewTask_ReturnTask()
		{
			//Arrange
			Guid newTaskGID = Guid.NewGuid();

			var newTask = new Data.Models.Task()
			{
				Id = newTaskGID,
				Description = "NewDescription",
				Priority = Data.Models.Enums.TaskPriorityEnum.Low,
				Status = Data.Models.Enums.TaskStatusEnum.Pending,
				Title = "NewTitle",
				UserId = UserGID,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			//Act
			var savedTask = await _taskService.CreateNewTask(newTask);

			// Assert
			Assert.NotNull(savedTask);
			Assert.Equal(newTaskGID, savedTask.Id);
		}

		[Fact]
		public async Task DeleteTask_Success()
		{
			//Arrange
			Guid newTaskGID = Guid.NewGuid();

			var newTask = new Data.Models.Task()
			{
				Id = newTaskGID,
				Description = "NewDescription",
				Priority = Data.Models.Enums.TaskPriorityEnum.Low,
				Status = Data.Models.Enums.TaskStatusEnum.Pending,
				Title = "NewTitle",
				UserId = UserGID,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			//Act
			await _taskService.CreateNewTask(newTask);
			var deleteResult = await _taskService.DeleteTask(newTaskGID, UserGID);

			// Assert
			Assert.IsType<OkResult>(deleteResult);
		}

		[Fact]
		public async Task DeleteTask_NotFound()
		{
			var deleteResult = await _taskService.DeleteTask(UnexistTask, UserGID);

			// Assert
			Assert.IsType<NotFoundResult>(deleteResult);
		}

		[Fact]
		public async Task GetTaskById_TaskFound()
		{
			//Act
			var task = await _taskService.GetTaskById(TaskGID, UserGID);
			
			// Assert
			Assert.NotNull(task);
			Assert.Equal(TaskGID, task.Id);
		}

		[Fact]
		public async Task GetTaskById_TaskNotFound()
		{
			//Act
			var task = await _taskService.GetTaskById(UnexistTask, UserGID);

			// Assert
			Assert.Null(task);
		}

		[Fact]
		public async Task GetTasks_OneTask()
		{
			//Act
			var tasks = await _taskService.GetTasks(new Data.Models.TaskQueryFilterParameters(), UserGID, "", "", 1, 10);

			//Assert
			Assert.True(tasks.Any());
			Assert.Equal(1, tasks.Count());
		}
		
		[Fact]
		public async Task GetTasks_NoTasks()
		{
			//Act
			var tasks = await _taskService.GetTasks(new Data.Models.TaskQueryFilterParameters(), UnexistUser, "", "", 1, 10);

			//Assert
			Assert.False(tasks.Any());
		}

		[Fact]
		public async Task UpdateTask_Success()
		{
			//Arrange
			Guid newTaskGID = Guid.NewGuid();

			var newTask = new Data.Models.Task()
			{
				Id = newTaskGID,
				Description = "NewDescription",
				Priority = Data.Models.Enums.TaskPriorityEnum.Low,
				Status = Data.Models.Enums.TaskStatusEnum.Pending,
				Title = "NewTitle",
				UserId = UserGID,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			var editedTask = new Data.Models.Task()
			{
				Id = newTaskGID,
				Description = "EditedDescription",
				Priority = Data.Models.Enums.TaskPriorityEnum.High,
				Status = Data.Models.Enums.TaskStatusEnum.Completed,
				Title = "EditedTitle",
				UserId = UserGID,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			//Act
			await _taskService.CreateNewTask(newTask);

			var taskUpdateResult = await _taskService.UpdateTask(editedTask, UserGID);

			var updatedTask = await _taskService.GetTaskById(newTaskGID, UserGID);

			//Assert
			Assert.IsType<OkResult>(taskUpdateResult);
			Assert.NotNull(updatedTask);
			Assert.Equal("EditedDescription", updatedTask.Description);
			Assert.Equal("EditedTitle", updatedTask.Title);
			Assert.Equal(Data.Models.Enums.TaskPriorityEnum.High, updatedTask.Priority);
			Assert.Equal(Data.Models.Enums.TaskStatusEnum.Completed, updatedTask.Status);
		}

		[Fact]
		public async Task UpdateTask_NotFound()
		{
			//Arrange
			Guid newTaskGID = Guid.NewGuid();

			var editedTask = new Data.Models.Task()
			{
				Id = newTaskGID,
				Description = "EditedDescription",
				Priority = Data.Models.Enums.TaskPriorityEnum.High,
				Status = Data.Models.Enums.TaskStatusEnum.Completed,
				Title = "EditedTitle",
				UserId = UserGID,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			//Act
			var taskUpdateResult = await _taskService.UpdateTask(editedTask, UserGID);

			Assert.IsType<NotFoundResult>(taskUpdateResult);
		}
	}
}
