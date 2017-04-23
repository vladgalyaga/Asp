using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IRepository<TEntity, TKey> : ISaveable, IDisposable
        where TEntity : class, IKeyable<TKey>
        
    {
        TEntity GetFirstOrDefaultAsync(Func<TEntity, bool> predicate);
        /// <summary>
        /// Returns true if entity exist in the repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> ContainsAsync(TEntity entity);

        /// <summary>
        /// Gets all instances
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Returns a list of elements, that satisfy a predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetWhereAsync(Func<TEntity, bool> predicate);

        /// <summary>
        /// Look for an entity
        /// </summary>
        /// <param name="id">Entity's identifier</param>
        /// <returns>Instance of <typeparamref name="TEntity"/> if success, else null</returns>
        TEntity FindById(TKey id);
        /// <summary>
        /// Look for an entity
        /// </summary>
        /// <param name="id">Entity's identifier</param>
        /// <returns>Instance of <typeparamref name="TEntity"/> if success, else null</returns>
        Task<TEntity> FindByIdAsync(TKey id);

        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete an entity by it's key
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        Task DeleteAsync(TKey entityId);

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
    }
}
