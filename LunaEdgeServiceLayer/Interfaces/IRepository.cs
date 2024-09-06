using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	/// <summary>
	/// Base CRUD operations repository
	/// </summary>
	/// <typeparam name="T">Entity class</typeparam>
	public interface IRepository<T> where T : class
	{
		/// <summary>
		/// Read data
		/// </summary>
		/// <returns>Collection of entities of T class</returns>
		public Task<IEnumerable<T>> Get();

		/// <summary>
		/// Create new entity of t class
		/// </summary>
		/// <param name="entity">New entity</param>
		/// <returns>New entity</returns>
		public Task<T> Create(T entity);

		/// <summary>
		/// Update entity
		/// </summary>
		/// <param name="id">Id of th e entity for update</param>
		/// <param name="entity">Entity with new properties values</param>
		/// <param name="ignoredProperties">Properties that ignored during update</param>
		/// <returns>Update result</returns>
		public Task<IActionResult> Update(Guid id, T entity, params string[] ignoredProperties);

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="id">Id of the entity for delete</param>
		/// <returns>Delete result</returns>
		public Task<IActionResult> Delete(Guid id);
	}
}
