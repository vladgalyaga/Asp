using OnlineStore.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        private readonly IEntitiesDbContext m_DbContext;
        private readonly Hashtable m_Repositories;

        private bool m_IsDisposed;

        public BaseUnitOfWork(IEntitiesDbContext dbContext)
        {
            m_DbContext = dbContext;
            m_Repositories = new Hashtable();
        }

        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IKeyable<TKey>
        {
            string typeName = typeof(TEntity).Name;
            if (m_Repositories.ContainsKey(typeName))
            {
                return (IRepository<TEntity, TKey>)m_Repositories[typeName];
            }

            IRepository<TEntity, TKey> repository = CreateRepository<TEntity, TKey>();
            m_Repositories.Add(typeName, repository);

            return repository;
        }

        protected virtual IRepository<TEntity, TKey> CreateSpecificRepository<TEntity, TKey>() where TEntity : class, IKeyable<TKey>
        {
            return null;
        }

        private IRepository<TEntity, TKey> CreateRepository<TEntity, TKey>() where TEntity : class, IKeyable<TKey>
        {
            //ThrowIfSetDoesNotExist<TEntity>();

            IRepository<TEntity, TKey> repository = CreateSpecificRepository<TEntity, TKey>();
            if (repository != null)
            {
                return repository;
            }
            Type repositoryType = typeof(EntityRepository<,>);

            repository = (IRepository<TEntity, TKey>)Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), m_DbContext);

            return repository;
        }

        private void ThrowIfSetDoesNotExist<TEntity, TKey>() where TEntity : class, IKeyable<TKey>
        {
            if (!m_DbContext.IsSetExist<TEntity,TKey>())
            {
                throw new ArgumentException
                    ($"Can't get repository for {typeof(TEntity).FullName}"
                    + " Db context doesn't have set for that type");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!m_IsDisposed && disposing)
            {
                m_DbContext.Dispose();
                foreach (IDisposable repository in m_Repositories.Values)
                {
                    //dispose all repositries
                    repository.Dispose();
                }
            }
            m_IsDisposed = true;
        }

        public int SaveChanges()
        {
            return m_DbContext.SaveChanges();
        }
        public Task<int> SaveChangesAsync()
        {
            return m_DbContext.SaveChangesAsync();
        }
    }
}
