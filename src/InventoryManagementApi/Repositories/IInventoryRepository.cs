using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementApi.Models;

namespace InventoryManagementApi.Repositories
{
    public interface IInventoryRepository : IInventoryUsedSegment
    {
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory> GetItemByProductCodeAsync(string productCode);
        Task InsertAsync(Inventory registerInventory);
        Task UpdateAsync(Inventory updateInvetory);
    }

    public interface IInventoryUsedSegment
    {
        Task UseInventoryAsync(InventoryUsed inventoryUsed);
    }
}