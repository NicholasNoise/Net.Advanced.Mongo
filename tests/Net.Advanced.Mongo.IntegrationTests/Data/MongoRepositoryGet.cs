using Xunit;

namespace Net.Advanced.Mongo.IntegrationTests.Data;

public class MongoRepositoryGet : BaseMongoRepoTestFixture
{
  protected override string DbName => nameof(MongoRepositoryAdd);

  [Fact]
  public async Task GetsCartList()
  {
    // Arrange.
    var repository = GetRepository();

    // Act.
    var newCarts = await repository.ListAsync();

    // Assert.
    Assert.Empty(newCarts);
  }
}
