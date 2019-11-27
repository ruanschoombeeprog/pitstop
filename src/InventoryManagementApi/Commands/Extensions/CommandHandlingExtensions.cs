using System.Linq;
using System.Reflection;
using InventoryManagementApi.Commands.Executors;
using InventoryManagementApi.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementApi.Commands.Extensions
{
    public static class CommandHandlingExtensions
    {
        public static void AddCommandHandling(this IServiceCollection services, Assembly assembly)
        {
            // Register all command handlers in this assembly which will be injected into an executor class.
            services.RegisterCommandHandlers<ICommandHandler>(assembly);
            services.AddTransient<ICommandExecutor, CommandExecutor>();
        }

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