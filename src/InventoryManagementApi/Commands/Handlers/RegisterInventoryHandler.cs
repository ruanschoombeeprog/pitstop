using System;
using System.Threading.Tasks;
using InventoryManagementApi.Models;
using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Handlers
{
    public class RegisterInventoryHandler : IHandler<RegisterInventory>
    {
        private readonly IInventoryRepository repository;

        public RegisterInventoryHandler(IInventoryRepository repository)
        {
            this.repository = repository;
        }
        public Type CommandType => typeof(RegisterInventory);

        public Task HandleCommandAsync(Command command)
        {
            return HandleRegisterInventoryAsync((RegisterInventory)command);
        }

        private Task HandleRegisterInventoryAsync(RegisterInventory command)
        {
            var inventoryItem = new Inventory(
                command.ProductCode, 
                command.Description, 
                command.Quantity.ToString(), 
                command.UnitPrice.ToString());

            var task = repository.InsertItem(inventoryItem);

            Console.WriteLine($"Command Handled : {command.GetType().Name}");

            return task;
        }
    }
}