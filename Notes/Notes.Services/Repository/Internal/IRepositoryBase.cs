using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Notes.Services.Internal
{
    /// <summary>
    /// Common Interface requirements for Repositories
    /// </summary>
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Get All Items
        /// </summary>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Get All Items
        /// </summary>
        /// <param name="where">That match the expression</param>
        IEnumerable<T> GetAll(Expression<Func<T, bool>> where);

        /// <summary>
        /// Return a specific item
        /// </summary>
        /// <param name="id"></param>
        T GetById(Guid id);

        /// <summary>
        /// Add a new item
        /// </summary>
        void Insert(T entity);

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="Id">Id of the item</param>
        void Delete(Guid Id);

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="entity">item to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Update an existing item
        /// </summary>
        /// <param name="entity">item to update</param>
        void Update(T entity);

        /// <summary>
        /// Save changes
        /// </summary>
        void Save();

    }
}
