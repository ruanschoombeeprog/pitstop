using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementApi.Models;

namespace InventoryManagementApi.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAll();
        Task<Inventory> GetItemByProductCode(string productCode);
        Task InsertItem(Inventory registerInventory);
        Task UpdateItem(Inventory updateInvetory);
        Task UseInventory(InventoryUsed inventoryUsed);
    }
}