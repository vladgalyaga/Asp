using OnlineStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    /// <summary>
    /// Provides CRUD actions with <typeparamref name="TEntity"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IKeyable<TKey>
    {
        private IEntitiesDbContext m_DbContext;
        private bool m_IsDisposed;

        public EntityRepository(IEntitiesDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        protected IDbSet<TEntity> DbEntitySet { get; private set; }

        public IEntitiesDbContext DbContext
        {
            get { return m_DbContext; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(DbContext));
                }

                IDbSet<TEntity> dbSet = value.TryGetSet<TEntity, TKey>();
                if (dbSet == null)
                {
                    throw new ArgumentException
                        ("Db context doesn't contains set for " +
                         $"'{typeof(TEntity).Name}'", nameof(DbContext));
                }

                this.m_DbContext = value;
                this.DbEntitySet = dbSet;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!m_IsDisposed && disposing)
            {
                DbContext.Dispose();
            }
            m_IsDisposed = true;
        }

        public Task<bool> ContainsAsync(TEntity entity)
        {
            return Task.Run(() => DbEntitySet.FirstOrDefault(p => p.Id.Equals(entity.Id)) != null);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return DbEntitySet.ToListAsync();
        }

        public Task<IEnumerable<TEntity>> GetWhereAsync(Func<TEntity, bool> predicate)
        {
            return Task.Run(() => DbEntitySet.Where(predicate));
        }
        public TEntity GetFirstOrDefaultAsync(Func<TEntity, bool> predicate)
        {
            return DbEntitySet.FirstOrDefault(predicate);
        }


        public Task<TEntity> FindByIdAsync(int id)
        {
            return DbEntitySet.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task CreateAsync(TEntity entity)
        {
            DbContext.CreateEntity<TEntity, TKey>(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            DbContext.UpdateEntity<TEntity, TKey>(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbContext.RemoveEntity<TEntity, TKey>(entity);
            await SaveChangesAsync();

        }
        public async Task DeleteAsync(int entityId)
        {
            TEntity entity = DbEntitySet.FirstOrDefault(p => p.Id.Equals(entityId));
            await DeleteAsync(entity);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

    }
}
