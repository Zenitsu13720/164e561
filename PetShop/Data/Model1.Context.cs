﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetShop.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class PetShopEntities1 : DbContext
    {
        private static PetShopEntities1 _context;
        public PetShopEntities1()
            : base("name=PetShopEntities1")
        {
        }

        public static PetShopEntities1 GetContext()
        {
            if (_context == null)
            {
                _context = new PetShopEntities1();
            }
            return _context;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<PickPoint> PickPoint { get; set; }
        public virtual DbSet<PickPointCity> PickPointCity { get; set; }
        public virtual DbSet<PickPointIndex> PickPointIndex { get; set; }
        public virtual DbSet<PickPointStreet> PickPointStreet { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductManufacturer> ProductManufacturer { get; set; }
        public virtual DbSet<ProductName> ProductName { get; set; }
        public virtual DbSet<ProductProvider> ProductProvider { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
    }
}