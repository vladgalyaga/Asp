using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IKeyable <TKey>
    { 
        /// <summary>
      /// Gets object's id
      /// </summary>
        TKey Id { get; }
    }
}
