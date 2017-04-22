using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface ISaveable
    {
        /// <summary>
        /// Save data context changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// Asynchronously save data context changes
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
