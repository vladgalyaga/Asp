using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public interface IStoreRepository
    {
        void Dispose();
        List<Categories> GetAllCategories();
        Categories GetCategoryByName(string categoryName);
        Products GetProduct(int productId);
    }
}
