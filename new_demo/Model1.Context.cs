﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace new_demo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class demo7Entities : DbContext
    {
        public demo7Entities()
            : base("name=demo7Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderStatuses> OrderStatuses { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffPositions> StaffPositions { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
