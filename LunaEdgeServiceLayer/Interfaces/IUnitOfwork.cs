using Microsoft.EntityFrameworkCore;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface IUnitOfwork : IDisposable
	{
		DbContext Context { get; }
		public Task SaveChangesAsync();
	}
}
