using Microsoft.AspNet.Identity;
using OnlineStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class AppUserManager : UserManager<Users, int>
    {
        public AppUserManager(IUserStore<Users, int> store) : base(store)
        {
        }
    }
}