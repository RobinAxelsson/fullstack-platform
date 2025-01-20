﻿using TenStar.App.Entities;
using Microsoft.EntityFrameworkCore;

namespace TenStar.App.DataAccess
{
    internal class TenStarDbContext : DbContext
    {
        public TenStarDbContext() { }

        public TenStarDbContext(DbContextOptions<TenStarDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.DbId);
                entity.HasIndex(u => u.TsId).IsUnique();
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(255);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            });
        }
    }
}