using Pitstop.Infrastructure.Messaging;
using System;

namespace InventoryManagementApi.Events
{
    public class InventoryRegistered : Event
    {
        public InventoryRegistered() : base(Guid.NewGuid())
        {

        }

        public string  ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}