using InventoryManagementApi.Commands;
using InventoryManagementApi.Models;
using System;

namespace Pitstop.InventoryManagementApi.Models.Extensions
{
    public static class CommandModelMapping
    {
        public static Inventory ToModel(this RegisterInventory command)
        {
            return new Inventory(
                command.ProductCode,
                command.Description,
                command.Quantity,
                command.UnitPrice);
        }

        public static Inventory ToModel(this UpdateInventory command)
        {
            return new Inventory(
                command.ProductCode,
                command.Description,
                command.Quantity,
                command.UnitPrice);
        }

        public static InventoryUsed ToModel(this UseInventoryItem command, double totalPrice = 0)
        {
            return ToModel(command, DateTime.Now, totalPrice);
        }
        
        public static InventoryUsed ToModel(this UseInventoryItem command, DateTime dateTime, double totalPrice = 0)
        {
            return new InventoryUsed(
                command.MessageId,
                command.ProductCode,
                command.JobId,
                command.Quantity,
                totalPrice,
                dateTime);
        }
    }
}
