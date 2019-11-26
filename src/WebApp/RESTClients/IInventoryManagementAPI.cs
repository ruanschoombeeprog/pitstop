using Pitstop.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Commands;

namespace WebApp.RESTClients
{
    public interface IInventoryManagementAPI
    {

        [Get("/inventory")]
        Task<List<Inventory>> GetInventory();

        [Get("/inventory/{productCode}")]
        Task<Inventory> GetInventoryByProductCode(string productCode);

        [Post("/inventory")]
        Task RegisterInventory(RegisterInventory command);

        [Put("/inventory")]
        Task UpdateInventory(UpdateInventory command);

        [Put("/inventory/{productCode}")]
        Task UseInventoryItem(string productCode, UseInventoryItem command);
    }
}
