using Pitstop.Infrastructure.Messaging;
using System.Threading.Tasks;

namespace Pitstop.InventoryManagementApi.Events
{
    public static class IMessagePublisherExtensions
    {
        public static async Task publishEventAsync(this IMessagePublisher publisher, Event @event)
        {
            await publisher.PublishMessageAsync(@event.MessageType, @event, string.Empty);
        }
    }
}
