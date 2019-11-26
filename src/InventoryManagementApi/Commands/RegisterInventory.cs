using System;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands
{
    public class RegisterInventory : Command
    {
        public RegisterInventory(Guid messageId, string productCode, string description, int quantity, double unitPrice) : base(messageId)
        {
            ProductCode = productCode;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}