using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApi.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryManagementDbContext dbContext;

        public InventoryRepository(InventoryManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Inventory>> GetAll()
        {
            return await dbContext.Inventories
                .ToListAsync();
        }

        public async Task<Inventory> GetItemByProductCode(string productCode)
        {
            return await dbContext.Inventories
                .FirstOrDefaultAsync(c => c.ProductCode == productCode);
        }

        public async Task InsertItem(Inventory registerInventory)
        {
            dbContext.Inventories.Add(registerInventory);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateItem(Inventory updateInvetory)
        {
            var item = await dbContext.Inventories
                .FirstOrDefaultAsync(i => i.ProductCode == updateInvetory.ProductCode);

            item.Quantity = updateInvetory.Quantity;

            if (item == null)
                throw new DbUpdateException("Product code does not exist.");

            dbContext.Inventories.Update(item);

            await dbContext.SaveChangesAsync();
        }

        public async Task UseInventory(InventoryUsed inventoryUsed)
        {
            dbContext.InventoryUseds.Add(inventoryUsed);
            await dbContext.SaveChangesAsync();
        }
    }
}