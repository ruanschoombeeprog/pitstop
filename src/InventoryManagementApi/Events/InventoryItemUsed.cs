using Pitstop.Infrastructure.Messaging;
using System;

namespace InventoryManagementApi.Events
{
    public class InventoryItemUsed : Event
    {
        public InventoryItemUsed() : base(Guid.NewGuid())
        {

        }

        public string ProductCode { get; set; }
        public string JobId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}