using OnlineStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public class UnitOfWork : BaseUnitOfWork
    {
        private readonly IEntitiesDbContext m_DbContext;

        public UnitOfWork(IEntitiesDbContext dbContext)
            : base(dbContext)
        {
            m_DbContext = dbContext;
        }
        

        protected override IRepository<TEntity, TKey> CreateSpecificRepository<TEntity, TKey>() 
        {
            //if (typeof(TEntity) == typeof(Users))
            //    return new UsersRepository(m_DbContext) as IRepository<TEntity>;

            //if (typeof(TEntity) == typeof(Roles))
            //    return new RolesRepository(m_DbContext) as IRepository<TEntity>;

            return base.CreateSpecificRepository<TEntity, TKey>();
        }
    }
}
