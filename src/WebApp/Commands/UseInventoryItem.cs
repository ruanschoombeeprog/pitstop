using Pitstop.Infrastructure.Messaging;
using System;

namespace WebApp.Commands
{
    public class UseInventoryItem : Command
    {
        public UseInventoryItem(Guid messageId, string jobId, string productCode, int quantity) : base(messageId)
        {
            JobId = jobId;
            ProductCode = productCode;
            Quantity = quantity;
        }

        public string JobId { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
