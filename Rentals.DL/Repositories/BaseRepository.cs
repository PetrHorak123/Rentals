using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Repositories
{
	/// <summary>
	/// Zákkladní repozitář, obsahující základní metody pro prácí s entitami.
	/// </summary>
	public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class 
	{
		protected readonly EntitiesContext Context;

		public BaseRepository(EntitiesContext context)
		{
			this.Context = context;
		}

		public TEntity GetById(int id)
		{
			return this.Context.Set<TEntity>().Find(id);
		}
		
		public TEntity[] GetAll()
		{
			return this.Context.Set<TEntity>().ToArray();
		}

		public TEntity[] Find(Expression<Func<TEntity, bool>> predicate)
		{
			return this.Context.Set<TEntity>().Where(predicate).ToArray();
		}

		public void Add(TEntity entity)
		{
			this.Context.Set<TEntity>().Add(entity);
		}

		public void AddRange(IEnumerable<TEntity> entity)
		{
			this.Context.Set<TEntity>().AddRange(entity);
		}

		public void Remove(TEntity entity)
		{
			this.Context.Set<TEntity>().Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entity)
		{
			this.Context.Set<TEntity>().RemoveRange(entity);
		}
	}
}
