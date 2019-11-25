using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementApi.Commands.Registration
{
    public static class CommandHandlerRegistration
    {
        public static void RegisterCommandHandlers<T>(this IServiceCollection services, Assembly assembly,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var handlerTypes = assembly.DefinedTypes
                .Where(x => x.GetInterfaces().Contains(typeof(T)));

            foreach (var handlerType in handlerTypes)
                if (!handlerType.IsAbstract)
                    services.Add(new ServiceDescriptor(typeof(T), handlerType, lifetime));
        }
    }
}