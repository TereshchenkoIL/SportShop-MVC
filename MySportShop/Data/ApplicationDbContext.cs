﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<ProductInfo> ProductInfo{ get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderInfo> OrderInfo { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderInfo>()
                .HasKey(c => new {c.ProductId,c.OrderId});            
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
