using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface IRepository<T> where T : class
	{
		public Task<ActionResult<IEnumerable<T>>> Get();
		public Task<ActionResult<T>> GetById(Guid id);
		public Task<ActionResult<T>> Create(T entity);
		public Task<IActionResult> Update(Guid id, T entity);
		public Task<IActionResult> Delete(Guid id);
	}
}
