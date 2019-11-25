using System;
using System.Threading.Tasks;
using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Handlers
{
    public class UseInventoryItemHandler : IHandler<UseInventoryItem>
    {
        private readonly IInventoryRepository repository;

        public UseInventoryItemHandler(IInventoryRepository repository)
        {
            this.repository = repository;
        }
        
        public Type CommandType => typeof(UseInventoryItem);
        
        public Task HandleCommandAsync(Command command)
        {
            Console.WriteLine($"Command Handled : {command.GetType().Name}");
            return Task.CompletedTask;
        }
    }
}