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

    public partial class Roles : IdentityRole<int, UserRole>,IKeyable<int>
    {
    }
}
