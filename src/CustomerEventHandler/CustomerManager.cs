using Microsoft.Extensions.Hosting;
using Pitstop.Infrastructure.Messaging;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerEventHandler
{
  public class CustomerManager : IHostedService, IMessageHandlerCallback
  {
    private readonly IMessageHandler messageHandler;

    public CustomerManager(IMessageHandler messageHandler)
    {
      this.messageHandler = messageHandler;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      this.messageHandler.Start((IMessageHandlerCallback) this);
      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      this.messageHandler.Stop();
      return Task.CompletedTask;
    }

    public Task<bool> HandleMessageAsync(string messageType, string message)
    {
      Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff") + " - " + message + Environment.NewLine ?? "");
      Log.Information<string, string>("{MessageType} - {Body}", messageType, message);
      return Task.FromResult<bool>(true);
    }
  }
}