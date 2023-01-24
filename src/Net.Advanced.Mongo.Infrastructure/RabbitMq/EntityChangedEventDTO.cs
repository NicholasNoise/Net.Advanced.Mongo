namespace Net.Advanced.Mongo.Infrastructure.RabbitMq;

public class EntityChangedEventDTO
{
  public string EventName { get; set; } = null!;

  public string EntityType { get; set; } = null!;

  public object Entity { get; set; } = null!;

  public string[] ChangedProps { get; set; } = Array.Empty<string>();
}
