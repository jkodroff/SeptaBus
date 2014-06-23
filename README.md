SeptaBus
========

Yet another in-memory message bus for .NET.  "It's getting there."

SeptaBus is an in-memory message bus.  It allows you to structure the logic of your application using the [pub-sub pattern](http://en.wikipedia.org/wiki/Publish%E2%80%93subscribe_pattern).  When used properly it'll help keep your code clean so that you focus on expressing your domain logic.

## The basics
In SeptaBus, there are two types of messages:

1. Commands, which tell the system to do something.  Commands should have exactly 1 handler.
2. Events, which indicate that something has happened.  Events may have 0 to many handlers.

## An example
```
// This is a command.  Sending it on the bus tells the system to submit 
// the order with the supplied id.
public class SubmitOrder : ICommand
{
  public Guid Id { get; set; }
}

// This is the controller that gets hit when the user clicks the 
// Submit Order button in the UI.
public class OrderController : Controller
{
  private IBus _bus;

  // ctor...

  [HttpPost]
  public ActionResult Submit(Guid id)
  {
    _bus.Send(new SubmitOrder { Id = id });

    // maybe redirect the user to a confirmation page...
  }
}

// This is the handler for the SubmitOrder command.
// Each command should have exactly 1 handler - no more, no less.
public class SubmitOrderHandler : IHander<SubmitOrder> 
{
  private IBus _bus;

  pubic void Handle(SubmitOrder message)
  {
    // change the order status to Submitted, save changes to the db

    _bus.Publish(new OrderSubmitted { Id = message.Id });
  }
}

// This is the event that gets published when an order is successfully submitted.
public class OrderSubmitted : IEvent
{
  public Guid Id { get; set; }
}

// This is an event handler for the OrderSubmitted event.
// Events may have 0 to many handlers.
public class OrderSubmittedHandler : IHandler<OrderSubmitted>
{
  public void Handle()
  {
    // send the customer an email
  }
}

```

The architecture of SeptaBus is influenced by the writings of [Udi Dahan](http://www.udidahan.com/), creator of [NServiceBus](http://particular.net/).
