using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orders.Data.Interfaces;

namespace Orders.Data.Repositories
{
	/// <summary>
	/// BaseRepository with some CRUD operations that Order and Product will inherit
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		protected readonly OrdersDbContext ordersDbContext;	
		protected readonly DbSet<TEntity> dbSet;			

		public BaseRepository(OrdersDbContext ordersDbContext)
		{ 
			this.ordersDbContext = ordersDbContext;
			dbSet = ordersDbContext.Set<TEntity>();
		}
		/// <summary>
		/// get all entities of type TEntity
		/// </summary>
		/// <returns>List of all TEntities</returns>
		public IList<TEntity> GetAll()
		{
			return dbSet.ToList();
		}
		/// <summary>
		/// insert a new TEntity into db
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>the inserted TEntity</returns>
		public TEntity Insert(TEntity entity)
		{ 
			EntityEntry<TEntity> entityEntry = dbSet.Add(entity);
			ordersDbContext.SaveChanges();
			return entityEntry.Entity;
		}
	}
}
