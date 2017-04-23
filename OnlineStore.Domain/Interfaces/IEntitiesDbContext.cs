using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IEntitiesDbContext : ISaveable, IDisposable
    {
        /// <summary>
        /// Returns an IDbSet of <typeparamref name="TEntity"/>
        /// </summary>
        /// <typeparam name="TEntity">Type of entity, for which DbSet is required</typeparam>
        /// <returns></returns>
        bool IsSetExist<TEntity, TKey>() where TEntity : class, IKeyable<TKey>;

        /// <summary>
        /// Returns true if Set of <typeparamref name="TEntity"/> exist
        /// in current db context
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IDbSet<TEntity> TryGetSet<TEntity, TKey>() where TEntity : class, IKeyable<TKey>;

        /// <summary>
        /// Set entity as created in DbContext
        /// </summary>
        /// <typeparam name="TEntity">Type of entity that is needed to add to db</typeparam>
        /// <param name="entity">Entity that is needed to add to db</param>
        void CreateEntity<TEntity, TKey>(TEntity entity) where TEntity : class, IKeyable<TKey>;
        /// <summary>
        /// Set entity as modified in DbContext
        /// </summary>
        /// <typeparam name="TEntity">Type of entity that is needed to update</typeparam>
        /// <param name="entity">Entity that is needed to update</param>
        void UpdateEntity<TEntity, TKey>(TEntity entity) where TEntity : class, IKeyable<TKey>;
        /// <summary>
        /// Set entity as deleted in DbContext
        /// </summary>
        /// <typeparam name="TEntity">Type of entity that is needed to delete</typeparam>
        /// <param name="entity">Entity that is needed to delete</param>
        void RemoveEntity<TEntity, TKey>(TEntity entity) where TEntity : class, IKeyable<TKey>;
    }
}
