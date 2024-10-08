﻿using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LunaEdgeServiceLayer.Data.Models;

namespace LunaEdgeServiceLayer.Implementations
{
	/// <summary>
	/// Base CRUD operations repository
	/// </summary>
	/// <typeparam name="T">Entity class</typeparam>
	public class RepositoryBase<T> : ControllerBase, IRepository<T> where T : class
	{
		protected readonly DbContext _context;
		protected DbSet<T> dbSet;
		private readonly IUnitOfWork _unitOfWork;

		public RepositoryBase(IUnitOfWork unitOfwork)
		{
			_unitOfWork = unitOfwork;
			dbSet = _unitOfWork.Context.Set<T>();
		}

		/// <summary>
		/// Read data
		/// </summary>
		/// <returns>Collection of entities of T class</returns>
		public async Task<IEnumerable<T>> Get()
		{
			var data = await dbSet.ToListAsync();
			return data;
		}

		/// <summary>
		/// Create new entity of t class
		/// </summary>
		/// <param name="entity">New entity</param>
		/// <returns>New entity</returns>
		public async Task<T> Create(T entity)
		{
			dbSet.Add(entity);
			await _unitOfWork.SaveChangesAsync();
			return entity;
		}

		/// <summary>
		/// Update entity
		/// </summary>
		/// <param name="id">Id of th e entity for update</param>
		/// <param name="entity">Entity with new properties values</param>
		/// <param name="ignoredProperties">Properties that ignored during update</param>
		/// <returns>Update result</returns>
		public async Task<IActionResult> Update(Guid id, T entity, params string[] ignoredProperties)
		{
			var existingEntity = await dbSet.FindAsync(id);
			if (existingEntity == null)
			{
				return NotFound();
			}

			var entry = _unitOfWork.Context.Entry(existingEntity);

			entry.CurrentValues.SetValues(entity);

			foreach (var propertyName in ignoredProperties)
			{
				entry.Property(propertyName).IsModified = false;
			}

			try
			{
				await _unitOfWork.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw;
			}

			return Ok();
		}

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="id">Id of the entity for delete</param>
		/// <returns>Delete result</returns>
		public async Task<IActionResult> Delete(Guid id)
		{
			var entity = await dbSet.FindAsync(id);
			if (entity == null)
			{
				return NotFound();
			}

			dbSet.Remove(entity);
			await _unitOfWork.SaveChangesAsync();
			return Ok();
		}
	}
}
