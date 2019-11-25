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

        public InventoryUsed(Guid id, string productCode, string jobId, int quantityUsed, double price, DateTime dateStamp)
        {
            this.Id = id;
            this.ProductCode = productCode;
            this.JobId = jobId;
            this.QuantityUsed = quantityUsed;
            this.Price = price;
            this.DateStamp = dateStamp;
        }
    }
}