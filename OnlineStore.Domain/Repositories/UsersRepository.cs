using Microsoft.AspNet.Identity.EntityFramework;
using OnlineStore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OnlineStore.Domain
{
    public class UsersRepository : UserStore<Users, Roles, int, Logins, UserRole, UserClaims>
    {
        public UsersRepository(DbContext context) 
            : base(context)
        {
        }
    }
}
