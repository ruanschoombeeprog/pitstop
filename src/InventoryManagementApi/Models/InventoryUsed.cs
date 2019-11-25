using System;

namespace InventoryManagementApi.Models
{
    public class InventoryUsed
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public string JobId { get; set; }
        public int QuantityUsed { get; set; }
        public double Price { get; set; }

        public DateTime DateStamp { get; set; }
    }
}