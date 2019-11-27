using System;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands
{
    public class UpdateInventory : Command
    {
        public UpdateInventory(Guid messageId, Guid id, string productCode, string description, int quantity, double unitPrice) : base(messageId)
        {
            Id = id;
            ProductCode = productCode;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}