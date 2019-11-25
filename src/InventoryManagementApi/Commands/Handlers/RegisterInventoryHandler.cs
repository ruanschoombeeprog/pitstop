using System;
using System.Threading.Tasks;
using InventoryManagementApi.Events;
using InventoryManagementApi.Models;
using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;
using Pitstop.InventoryManagementApi.Domain.Rules;

namespace InventoryManagementApi.Commands.Handlers
{
    public class RegisterInventoryHandler : IHandler<RegisterInventory>
    {
        private readonly IInventoryRepository repository;
        private readonly IMessagePublisher messagePublisher;

        public RegisterInventoryHandler(IInventoryRepository repository, IMessagePublisher messagePublisher)
        {
            this.repository = repository;
            this.messagePublisher = messagePublisher;
        }

        public Type CommandType => typeof(RegisterInventory);

        public Task HandleCommandAsync(Command command)
        {
            return HandleRegisterInventoryAsync((RegisterInventory)command);
        }

        private async Task HandleRegisterInventoryAsync(RegisterInventory command)
        {
            command.InventoryPriceShouldBeZerorOrLarger();
            command.InventoryProductCodeLengthShouldBeNineCharactersLong();
            command.InventoryQuantityShouldBeZeroOrLarger();

            var inventoryItem = new Inventory(
                command.ProductCode,
                command.Description,
                command.Quantity,
                command.UnitPrice);

            await repository.InsertItem(inventoryItem);

            var inventoryUpdated = new InventoryRegistered()
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