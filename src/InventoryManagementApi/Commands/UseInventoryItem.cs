using System;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands
{
    public class UseInventoryItem : Command
    {
        public UseInventoryItem(Guid messageId, string jobId, string productCode, int quantity) : base(messageId)
        {
            this.JobId = jobId;
            this.ProductCode = productCode;
            this.Quantity = quantity;
        }
        
        public string JobId { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }


    }
}