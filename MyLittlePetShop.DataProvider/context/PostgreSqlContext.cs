using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyLittlePetShop.DataProvider.models;
using MyLittlePetShop.Entity.entities;

namespace MyLittlePetShop.DataProvider.context
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
        }

        public DbSet<OwnerDb> Owner { get; set; }
        public DbSet<ContactDb> Contacts { get; set; }
        public DbSet<PetDb> Pets { get; set; }
        public DbSet<UserDb> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //entities relations
            modelBuilder.Entity<OwnerDb>()
                .HasMany(a => a.Pets)
                .WithOne(b => b.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OwnerDb>()
                .HasMany(a => a.Contacts)
                .WithOne(b => b.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PetDb>()
                .HasOne(a => a.Owner)
                .WithMany(b => b.Pets)
                .HasForeignKey(c => c.OwnerId);

            modelBuilder.Entity<ContactDb>()
                .HasOne(a => a.Owner)
                .WithMany(b => b.Contacts)
                .HasForeignKey(c => c.OwnerId);

            //soft delete
            modelBuilder.Entity<OwnerDb>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<PetDb>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<ContactDb>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<UserDb>().HasQueryFilter(i => !i.IsDeleted);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder
                    .UseLazyLoadingProxies();
        }

        public override int SaveChanges()
        {
            //soft delete
            foreach (var item in this.ChangeTracker.Entries())
            {
                if (item.State == EntityState.Deleted && item.Entity is ISoftDelete)
                {
                    item.CurrentValues[nameof(ISoftDelete.IsDeleted)] = true;
                    item.State = EntityState.Modified;
                }
            }
            return base.SaveChanges();
        }
    }
}