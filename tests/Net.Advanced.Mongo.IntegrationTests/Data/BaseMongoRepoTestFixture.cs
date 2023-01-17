using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Net.Advanced.Mongo.Core.CartAggregate;
using Net.Advanced.Mongo.Infrastructure;
using Net.Advanced.Mongo.Infrastructure.Data;
using MongoDatabaseSettings = Net.Advanced.Mongo.Infrastructure.Data.MongoDatabaseSettings;

namespace Net.Advanced.Mongo.IntegrationTests.Data;

public abstract class BaseMongoRepoTestFixture
{
  protected IMongoCollection<Cart> _mongoCollection;

  protected BaseMongoRepoTestFixture()
  {
    _mongoCollection = CreateNewContextCollection();
  }

  protected abstract string DbName { get; }

  protected IMongoCollection<Cart> CreateNewContextCollection()
  {
    var settings = new MongoDatabaseSettings
    {
      ConnectionString = "mongodb://localhost:27017",
      DatabaseName = "CartStore",
      CollectionName = DbName,
    };
    var serviceProvider = new ServiceCollection()
      .AddSingleton(settings)
      .AddMongo<Cart>()
      .BuildServiceProvider();

    return serviceProvider.GetRequiredService<IMongoCollection<Cart>>();
  }

  protected MongoRepository<Cart> GetRepository()
  {
    return new MongoRepository<Cart>(_mongoCollection);
  }
}
