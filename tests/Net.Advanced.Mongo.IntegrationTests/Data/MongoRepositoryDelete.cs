using Net.Advanced.Mongo.Core.CartAggregate;
using Xunit;

namespace Net.Advanced.Mongo.IntegrationTests.Data;

public class MongoRepositoryDelete : BaseMongoRepoTestFixture
{
  protected override string DbName => nameof(MongoRepositoryDelete);

  [Fact]
  public async Task DeletesItemAfterAddingIt()
  {
    // Arrange.
    var repository = GetRepository();
    var initialName = Guid.NewGuid().ToString();
    var cart = new Cart { Name = initialName };
    await repository.AddAsync(cart);

    // Act.
    await repository.DeleteAsync(cart);

    // Assert.
    Assert.DoesNotContain(await repository.ListAsync(),
        c => c.Name == initialName);
  }
}
