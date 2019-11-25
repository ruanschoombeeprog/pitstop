using Pitstop.Infrastructure.Messaging;
using System;

namespace InventoryManagementApi.Events
{
    public class InventoryUpdated : Event
    {
        public InventoryUpdated() : base(Guid.NewGuid())
        {

        }

        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}