using Ardalis.GuardClauses;
using Net.Advanced.Mongo.SharedKernel;

namespace Net.Advanced.Mongo.Core.CartAggregate.Handlers;
public class Product : EntityBase
{
  public Product(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }
  
  public string Name { get; set; }
  public string? Description { get; set; }
  public string? Image { get; set; }
  public Category? Category { get; set; }
  public decimal Price { get; set; }
  public uint Amount { get; set; }
}
