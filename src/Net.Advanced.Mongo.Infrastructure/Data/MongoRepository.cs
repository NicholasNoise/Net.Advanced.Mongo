using Ardalis.Specification;
using MongoDB.Driver;
using Net.Advanced.Mongo.SharedKernel.Interfaces;

namespace Net.Advanced.Mongo.Infrastructure.Data;

// inherit from Ardalis.Specification type
public class MongoRepository<T> : IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  private readonly IMongoCollection<T> _mongoCollection;

  public MongoRepository(IMongoCollection<T> mongoCollection)
  {
    _mongoCollection = mongoCollection;
  }

  public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
  {
    var cursor = await _mongoCollection.FindAsync(x => x.Id == Convert.ToInt32(id), cancellationToken: cancellationToken);
    return await cursor.FirstOrDefaultAsync(cancellationToken);
  }

  public Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification,
    CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification,
    CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification,
    CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification,
    CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
  {
    var cursor = await GetCursorAsync(cancellationToken);
    return await cursor.ToListAsync(cancellationToken);
  }

  public async Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    var filters = specification.WhereExpressions.ToList();
    var cursor = await _mongoCollection.FindAsync(
      t => filters.All(f => f.FilterFunc(t)),
      cancellationToken: cancellationToken);
    return await cursor.ToListAsync(cancellationToken);
  }

  public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public async Task<int> CountAsync(CancellationToken cancellationToken = default)
  {
    var result = await _mongoCollection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);
    return (int)result;
  }

  public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
  {
    var cursor = await GetCursorAsync(cancellationToken);
    return await cursor.AnyAsync(cancellationToken);
  }

  public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
  {
    await _mongoCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    return entity;
  }

  public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
  {
    var list = entities.ToList();
    await _mongoCollection.InsertManyAsync(list, cancellationToken: cancellationToken);
    return list;
  }

  public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
  {
    return _mongoCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
  }

  public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
  {
    return _mongoCollection.DeleteOneAsync(x => x.Id == entity.Id, cancellationToken: cancellationToken);
  }

  public Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
  {
    var list = entities.ToList();
    return _mongoCollection.DeleteManyAsync(x => list.Contains(x), cancellationToken: cancellationToken);
  }

  public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return Task.FromResult(0);
  }

  private Task<IAsyncCursor<T>> GetCursorAsync(CancellationToken cancellationToken = default)
  {
    return _mongoCollection.FindAsync(_ => true, cancellationToken: cancellationToken);
  }
}
