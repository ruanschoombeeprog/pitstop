using Pitstop.Infrastructure.Messaging;
using System;

namespace CustomerEventHandler
{
  public class CustomerRegistered : Event
  {
    public Guid customerId { get; set; }

    public string Name { get; set; }
  }
}