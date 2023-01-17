namespace Net.Advanced.Mongo.Core.CartAggregate;

public record CartItem(int ProductId, string Name, string? Image, decimal Price, decimal Quantity);
