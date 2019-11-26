using System;
using System.Linq.Expressions;
using InventoryManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polly;

namespace InventoryManagementApi.Repositories
{
    public class InventoryManagementDbContext : DbContext
    {
        public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options) : base(options)
        {

        }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryUsed> InventoryUseds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new GenericEntityTypeConfiguration<Inventory>(i => i.ProductCode));
            builder.ApplyConfiguration(new GenericEntityTypeConfiguration<InventoryUsed>(i => i.Id));
            base.OnModelCreating(builder);
        }

        public void MigrateDB()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.Migrate());
        }
    }

    public class GenericEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        private readonly Expression<Func<T, object>> hashKeyExpression;

        public GenericEntityTypeConfiguration(Expression<Func<T, object>> hashKeyExpression)
        {
            this.hashKeyExpression = hashKeyExpression;
        }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(hashKeyExpression);
            builder.ToTable(typeof(T).Name);
        }
    }
}