using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LunaEdgeServiceLayer.Data.Models;

namespace LunaEdgeServiceLayer.Implementations
{
	public class RepositoryBase<T> : ControllerBase, IRepository<T> where T : class
	{
		protected readonly DbContext _context;
		protected DbSet<T> dbSet;
		private readonly IUnitOfwork _unitOfWork;

		public RepositoryBase(IUnitOfwork unitOfwork)
		{
			_unitOfWork = unitOfwork;
			dbSet = _unitOfWork.Context.Set<T>();
		}

		//Get Request
		public async Task<ActionResult<IEnumerable<T>>> Get()
		{
			var data = await dbSet.ToListAsync();
			return Ok(data);
		}

		//Create Request
		public async Task<ActionResult<T>> Create(T entity)
		{
			dbSet.Add(entity);
			await _unitOfWork.SaveChangesAsync();
			return entity;
		}

		//Update Request
		public async Task<IActionResult> Update(Guid id, T entity)
		{
			var existingEntity = await dbSet.FindAsync(id);
			if (existingEntity == null)
			{
				return NotFound();
			}

			_unitOfWork.Context.Entry(existingEntity).CurrentValues.SetValues(entity);

			try
			{
				await _unitOfWork.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw;
			}

			return NoContent();
		}

		//Delete Request
		public async Task<IActionResult> Delete(Guid id)
		{
			var entity = await dbSet.FindAsync(id);
			if (entity == null)
			{
				return NotFound();
			}

			dbSet.Remove(entity);
			await _unitOfWork.SaveChangesAsync();
			return NoContent();
		}

		public async Task<ActionResult<T>> GetById(Guid id)
		{
			var entity = await dbSet.FindAsync(id);
			if (entity == null)
			{
				return NotFound();
			}
			return entity;
		}
	}
}
