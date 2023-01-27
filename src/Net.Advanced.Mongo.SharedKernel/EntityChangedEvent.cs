namespace Net.Advanced.Mongo.SharedKernel;

public class EntityChangedEvent<T> : DomainEventBase
  where T : EntityBase
{
  private readonly List<string> _changedProps = new();

  public EntityChangedEvent(T entity, params string[] changedProps)
  {
    Entity = entity;
    _changedProps.AddRange(changedProps);
  }

  public T Entity { get; set; }

  public string EntityType => typeof(T).Name;

  public IReadOnlyCollection<string> ChangedProps => _changedProps.AsReadOnly();
}
