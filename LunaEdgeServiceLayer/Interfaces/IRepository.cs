using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface IRepository<T> where T : class
	{
		public Task<IEnumerable<T>> Get();
		public Task<T> GetById(Guid id);
		public Task<T> Create(T entity);
		public Task<IActionResult> Update(Guid id, T entity, params string[] ignoredProperties);
		public Task<IActionResult> Delete(Guid id);
	}
}
