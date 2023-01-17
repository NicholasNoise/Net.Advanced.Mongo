using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Net.Advanced.Mongo.SharedKernel.Interfaces;
using MongoDatabaseSettings = Net.Advanced.Mongo.Infrastructure.Data.MongoDatabaseSettings;

namespace Net.Advanced.Mongo.Infrastructure;

public static class StartupSetup
{
  public static IServiceCollection AddMongo<T>(this IServiceCollection services) where T : class, IAggregateRoot =>
      services.AddSingleton(provider =>
      {
        var connectionSettings = provider.GetRequiredService<MongoDatabaseSettings>();

        var mongoClient = new MongoClient(connectionSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(connectionSettings.DatabaseName);

        return mongoDatabase.GetCollection<T>(connectionSettings.CollectionName);
      });
}
