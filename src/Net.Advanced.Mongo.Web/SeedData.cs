using Net.Advanced.Mongo.Core.CartAggregate;
using Net.Advanced.Mongo.SharedKernel.Interfaces;

namespace Net.Advanced.Mongo.Web;

public static class SeedData
{
  public static readonly Cart Cart1 = new Cart { Id = 1, };
  public static readonly Cart Cart2 = new Cart { Id = 2, Name = "Test cart", };
  public static readonly CartItem CartItem1 = new CartItem(1, "CPU", null, 1m, 1);
  public static readonly CartItem CartItem2 = new CartItem(2, "Picture", "https://example.com/favicon.ico", 322m, 2);
  public static readonly CartItem CartItem3 = new CartItem(1, "CPU", null, 1m, 3);

  public static async Task Initialize(IServiceProvider serviceProvider)
  {
    var repository = serviceProvider.GetService<IRepository<Cart>>();
    if (repository is null)
    {
      throw new ArgumentNullException(nameof(repository));
    }

    // Look for any carts.
    if (await repository.AnyAsync())
    {
      return;   // DB has been seeded
    }

    await PopulateTestData(repository);
  }

  public static async Task PopulateTestData(IRepository<Cart> repository)
  {
    Cart1.AddItem(CartItem1);
    Cart1.AddItem(CartItem2);
    await repository.AddAsync(Cart1);

    Cart2.AddItem(CartItem3);
    await repository.AddAsync(Cart2);
  }
}
