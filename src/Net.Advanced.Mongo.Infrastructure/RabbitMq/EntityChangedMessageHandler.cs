using Microsoft.Extensions.DependencyInjection;
using Net.Advanced.Mongo.Core.CartAggregate.Handlers;
using Net.Advanced.Mongo.SharedKernel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Core.DependencyInjection.Models;

namespace Net.Advanced.Mongo.Infrastructure.RabbitMq;

public class EntityChangedMessageHandler : IMessageHandler
{
  private readonly IServiceProvider _serviceProvider;

  public EntityChangedMessageHandler(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public void Handle(MessageHandlingContext context, string matchingRoute)
  {
    var msg = context.Message.GetMessage();
    dynamic obj = JObject.Parse(msg);

    // TODO: fix this hardcoded stuff (use matchingRoute?)
    if (obj.EventName == "EntityChangedEvent`1")
    {
      var entityChangedDto = JsonConvert.DeserializeObject<EntityChangedEventDTO>(msg);
      if (entityChangedDto.EntityType == "Product")
      {
        var product = JsonConvert.DeserializeObject<Product>(entityChangedDto.Entity.ToString()!);
        var entityChangedEvent = new EntityChangedEvent<Product>(product, entityChangedDto.ChangedProps.ToArray());
        var handler = _serviceProvider.GetRequiredService<ProductChangeHandler>();
        handler.Handle(entityChangedEvent).GetAwaiter().GetResult();
      }
    }

    context.AcknowledgeMessage();
  }
}
