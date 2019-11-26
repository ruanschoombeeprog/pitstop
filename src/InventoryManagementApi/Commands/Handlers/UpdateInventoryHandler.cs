using System;
using System.Threading.Tasks;
using InventoryManagementApi.Events;
using InventoryManagementApi.Models;
using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;
using Pitstop.InventoryManagementApi.Domain.Rules;

namespace InventoryManagementApi.Commands.Handlers
{
    public class UpdateInventoryHandler : IHandler<UpdateInventory>
    {
        private readonly IInventoryRepository repository;
        private readonly IMessagePublisher messagePublisher;

        public UpdateInventoryHandler(IInventoryRepository repository, IMessagePublisher messagePublisher)
        {
            this.repository = repository;
            this.messagePublisher = messagePublisher;
        }
        
        public Type CommandType => typeof(UpdateInventory);

        public Task HandleCommandAsync(Command command) => HandleCommandAsync((UpdateInventory)command);

        private async Task HandleCommandAsync(UpdateInventory command)
        {
            command.InventoryPriceShouldBeZerorOrLarger();
            command.InventoryProductCodeLengthShouldBe12CharactersLong();
            command.InventoryQuantityShouldBeZeroOrLarger();

            var inventoryItem = new Inventory(
                command.ProductCode,
                command.Description,
                command.Quantity,
                command.UnitPrice);

            await repository.UpdateItem(inventoryItem);

            var inventoryUpdated = new InventoryUpdated()
            {
                ProductCode = command.ProductCode,
                Description = command.Description,
                Quantity = command.Quantity,
                UnitPrice = command.UnitPrice
            };

            await messagePublisher.PublishMessageAsync(inventoryUpdated.MessageType, inventoryUpdated, "");

            Console.WriteLine($"Command Handled : {command.GetType().Name}");
        }
    }
}