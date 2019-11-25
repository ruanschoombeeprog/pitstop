using System;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands
{
    public class GetInventoryByCode : Command
    {
        public string ProductCode { get; set; }
        public GetInventoryByCode(string productCode) : base(Guid.NewGuid())
        {
            this.ProductCode = productCode;
        }
    }
}