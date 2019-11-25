using System;
using System.Threading.Tasks;
using InventoryManagementApi.Events;
using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;
using Pitstop.InventoryManagementApi.Domain.Rules;

namespace InventoryManagementApi.Commands.Handlers
{
    public class UseInventoryItemHandler : IHandler<UseInventoryItem>
    {
        private readonly IInventoryRepository repository;
        private readonly IMessagePublisher messagePublisher;

        public UseInventoryItemHandler(IInventoryRepository repository, IMessagePublisher messagePublisher)
        {
            this.repository = repository;
            this.messagePublisher = messagePublisher;
        }
        
        public Type CommandType => typeof(UseInventoryItem);

        public Task HandleCommandAsync(Command command)
        {
            return HandleUseInventoryItemAsync((UseInventoryItem)command);
        }

        private async Task HandleUseInventoryItemAsync(UseInventoryItem command)
        {
            var inventory = await repository.GetItemByProductCode(command.ProductCode);

            command.UseInventoryQuantityMustBeSmallerOrEqualToInventoryLevel(inventory);

            inventory.Quantity -= command.Quantity;

            await repository.UpdateItem(inventory);

            var useInventoryItem = new UseInventoryItem(Guid.NewGuid(), 
                command.JobId, 
                command.ProductCode, 
                command.Quantity);

            

            var @event = new InventoryItemUsed()
            {
                ProductCode = command.ProductCode,
                Quantity = command.Quantity,
                JobId =command.JobId
            };

            await messagePublisher.PublishMessageAsync(@event.MessageType, @event, "");

            Console.WriteLine($"Command Handled : {command.GetType().Name}");
        }
    }
}