namespace OnlineStore.Domain
{
    using Entity;
    using Interfaces;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users : IdentityUser<int, Logins, UserRole, UserClaims>,IKeyable<int>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }


    }
}
