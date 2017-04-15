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

        public List<Categories> GetAllCategories()
        {
            return db.Categories.ToList();
          
        }
        public Categories GetCategoryByName(string categoryName)
        {
            return db.Categories.FirstOrDefault(x => x.CategoryName == categoryName);
        }

        public Products GetProduct(int productId)
        {
            return db.Products.FirstOrDefault(x => x.ProductID == productId);
        }
    }
}
