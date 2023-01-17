using Net.Advanced.Mongo.Core.CartAggregate;
using Xunit;

namespace Net.Advanced.Mongo.IntegrationTests.Data;

public class MongoRepositoryUpdate : BaseMongoRepoTestFixture
{
  protected override string DbName => nameof(MongoRepositoryUpdate);

  [Fact]
  public async Task UpdatesItemAfterAddingIt()
  {
    // Arrange
    var repository = GetRepository();
    var initialName = Guid.NewGuid().ToString();
    var cart = new Cart { Name = initialName };

    await repository.AddAsync(cart);

    // fetch the item and update its title
    var newCart = (await repository.ListAsync())
        .FirstOrDefault(c => c.Name == initialName);
    Assert.NotNull(newCart);
    Assert.NotSame(cart, newCart);
    var newName = Guid.NewGuid().ToString();
    newCart.Name = newName;

    // Update the item
    await repository.UpdateAsync(newCart);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
        .FirstOrDefault(c => c.Name == newName);

    Assert.NotNull(updatedItem);
    Assert.NotEqual(cart.Name, updatedItem.Name);
    Assert.Equal(newCart.Id, updatedItem.Id);
  }
}
