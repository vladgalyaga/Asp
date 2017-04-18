namespace OnlineStore.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class NorthwindDbContext : IdentityDbContext<Users, Roles, int, Logins, UserRole, UserClaims>
    {
        public NorthwindDbContext()
            : base("name=Northwind")
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CustomerDemographics> CustomerDemographics { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Territories> Territories { get; set; }
        public virtual DbSet<UserClaims> UserClaims { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserClaims>().ToTable("UserClaims");
            modelBuilder.Entity<Logins>().ToTable("Logins");
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");

            modelBuilder.Entity<CustomerDemographics>()
                .Property(e => e.CustomerTypeID)
                .IsFixedLength();

            modelBuilder.Entity<CustomerDemographics>()
                .HasMany(e => e.Customers)
                .WithMany(e => e.CustomerDemographics)
                .Map(m => m.ToTable("CustomerCustomerDemo").MapLeftKey("CustomerTypeID").MapRightKey("CustomerID"));

            modelBuilder.Entity<Customers>()
                .Property(e => e.CustomerID)
                .IsFixedLength();

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Employees1)
                .WithOptional(e => e.Employees2)
                .HasForeignKey(e => e.ReportsTo);

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Territories)
                .WithMany(e => e.Employees)
                .Map(m => m.ToTable("EmployeeTerritories").MapLeftKey("EmployeeID").MapRightKey("TerritoryID"));

            modelBuilder.Entity<Order_Details>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.CustomerID)
                .IsFixedLength();

            modelBuilder.Entity<Orders>()
                .Property(e => e.Freight)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Orders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>()
                .Property(e => e.RegionDescription)
                .IsFixedLength();

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Territories)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shippers>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Shippers)
                .HasForeignKey(e => e.ShipVia);

            modelBuilder.Entity<Territories>()
                .Property(e => e.TerritoryDescription)
                .IsFixedLength();
        }
    }
}
