using Microsoft.EntityFrameworkCore;

namespace LunaEdgeServiceLayer.Interfaces
{
	/// <summary>
	/// Unit of work pattern interface
	/// </summary>
	public interface IUnitOfwork : IDisposable
	{
		DbContext Context { get; }
		/// <summary>
		/// Save changes for context
		/// </summary>
		/// <returns></returns>
		public Task SaveChangesAsync();
	}
}
