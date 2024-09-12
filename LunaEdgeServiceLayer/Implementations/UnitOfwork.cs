using LunaEdgeServiceLayer.Data;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LunaEdgeServiceLayer.Implementations
{
	/// <summary>
	/// Implementation unit of work pattern
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDBContext _context;
		private bool _disposed = false;

		public UnitOfWork(AppDBContext context)
		{
			_context = context;
		}
		public DbContext Context => _context;

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}

				_disposed = true;
			}
		}
	}
}
