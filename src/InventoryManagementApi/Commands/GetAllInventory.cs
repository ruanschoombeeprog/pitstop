using System;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands
{
    public class GetAllInventory : Command
    {
        public GetAllInventory() :  base(Guid.NewGuid())
        {
            
        }
    }
}