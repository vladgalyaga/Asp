using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public class StoreRepository : IDisposable, IStoreRepository
    {
        static private NorthwindEntities db = new NorthwindEntities();

        public void Dispose()
        {
            
            ((IDisposable)db).Dispose();
        }

        public List<Categories> GetCategories()
        {
            return db.Categories.ToList();
          
        }
    }
}
