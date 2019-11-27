using InventoryManagementApi.Commands;
using InventoryManagementApi.Events;

namespace Pitstop.InventoryManagementApi.Events.Extensions
{
    public static class CommandEventMapping
    {
        public static InventoryRegistered ToEvent(this RegisterInventory command)
        {
            return new InventoryRegistered()
            {
                ProductCode = command.ProductCode,
                Description = command.Description,
                Quantity = command.Quantity,
                UnitPrice = command.UnitPrice
            };
        }
        public static InventoryUpdated ToEvent(this UpdateInventory command)
        {
            return new InventoryUpdated()
            {
                ProductCode = command.ProductCode,
                Description = command.Description,
                Quantity = command.Quantity,
                UnitPrice = command.UnitPrice
            };
        }

        public static InventoryItemUsed ToEvent(this UseInventoryItem command)
        {
            return new InventoryItemUsed()
            {
                JobId = command.JobId,
                Quantity = command.Quantity,
                ProductCode = command.ProductCode,
                Price = 0
            };
        }
    }
}
