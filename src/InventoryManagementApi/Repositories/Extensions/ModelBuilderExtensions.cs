using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApi.Repositories.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void RegisterEntity<T>(this ModelBuilder builder, Expression<Func<T, object>> hashKeyExpression, string tableName) where T : class
        {
            builder.Entity<T>().HasKey(hashKeyExpression);
            builder.Entity<T>().ToTable(typeof(T).GetType().Name);
        }
    }
}