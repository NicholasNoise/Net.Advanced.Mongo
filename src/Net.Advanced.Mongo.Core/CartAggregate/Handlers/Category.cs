using Ardalis.GuardClauses;
using Net.Advanced.Mongo.SharedKernel;

namespace Net.Advanced.Mongo.Core.CartAggregate.Handlers;
public class Category : EntityBase
{
  public Category(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }

  public string Name { get; set; }
  public string? Image { get; set; }
  public Category? Parent { get; set; }
}
