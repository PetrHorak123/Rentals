using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rentals.DL.Interfaces
{
	/// <summary>
	/// Bázový repozitář.
	/// </summary>
	public interface IRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// Získá entitu podle daného id.
		/// </summary>
		TEntity GetById(int id);

		/// <summary>
		/// Získá všechny entity.
		/// Používat pouze v krajní nouzi.
		/// </summary>
		/// <returns></returns>
		TEntity[] GetAll();

		/// <summary>
		/// Vrací entity podle zadané expression.
		/// </summary>
		TEntity[] Find(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Přidá entitu do databáze.
		/// </summary>
		/// <param name="entity"></param>
		void Add(TEntity entity);

		/// <summary>
		/// Přidá kolekci entit do databáze.
		/// </summary>
		void AddRange(IEnumerable<TEntity> entity);

		/// <summary>
		/// Smaže entitu z databáze.
		/// </summary>
		void Remove(TEntity entity);

		/// <summary>
		/// Smaže kolekci entit z databáze.
		/// </summary>
		void RemoveRange(IEnumerable<TEntity> entity);
	}
}
