using Pitstop.Models;
using System.Collections.Generic;

namespace Pitstop.ViewModels
{
    public class InventoryManagementViewModel
    {
        public IEnumerable<Inventory> Inventories { get; set; }
    }
}
