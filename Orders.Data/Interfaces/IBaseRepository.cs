
namespace Orders.Data.Interfaces
{
	/// <summary>
	/// interface for BaseRepository with some CRUD operations that Order and Product will inherit
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IBaseRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// get all entities of type TEntity
		/// </summary>
		/// <returns>List of all TEntities</returns>
		IList<TEntity> GetAll();
		/// <summary>
		/// insert a new TEntity into db
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>the inserted TEntity</returns>
		TEntity Insert(TEntity entity);
	}
}
