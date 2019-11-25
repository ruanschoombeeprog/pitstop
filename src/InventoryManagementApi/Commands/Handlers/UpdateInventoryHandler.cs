using System;
using System.Threading.Tasks;
using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Handlers
{
    public class UpdateInventoryHandler : IHandler<UpdateInventory>
    {
        private readonly IInventoryRepository repository;

        public UpdateInventoryHandler(IInventoryRepository repository)
        {
            this.repository = repository;
        }
        
        public Type CommandType => typeof(UpdateInventory);

        public Task HandleCommandAsync(Command command)
        {
            Console.WriteLine($"Command Handled : {command.GetType().Name}");
            return Task.CompletedTask;
        }
    }
}