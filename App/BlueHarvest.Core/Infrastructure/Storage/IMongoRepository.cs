//#define USE_LINQ_TO_MONGO

using BlueHarvest.Shared.Models;

namespace BlueHarvest.Core.Infrastructure.Storage;

// https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e

public interface IMongoRepository
{
   string? CollectionName { get; }
   Task InitializeIndexesAsync(CancellationToken cancellationToken = default);
   Task SeedDataAsync(CancellationToken cancellationToken = default);
}

public interface IMongoRepoCreate<TDoc> where TDoc : IMongoDocument
{
   void InsertOne(TDoc document);
   Task InsertOneAsync(TDoc document, CancellationToken cancellationToken = default);
   void InsertMany(ICollection<TDoc> documents);
   Task InsertManyAsync(ICollection<TDoc> documents, CancellationToken cancellationToken = default);
}

public interface IMongoRepoRead<TDoc> where TDoc : IMongoDocument
{
   TDoc FindById(string id);
   Task<TDoc> FindByIdAsync(string id, CancellationToken cancellationToken=default);
#if USE_LINQ_TO_MONGO
   TDoc FindOne(Expression<Func<TDoc, bool>> filterExpression);
   Task<TDoc> FindOneAsync(Expression<Func<TDoc, bool>> filterExpression);
#endif
}

public interface IMongoRepoUpdate<TDoc> where TDoc : IMongoDocument
{
   void ReplaceOne(TDoc document);
   Task ReplaceOneAsync(TDoc document, CancellationToken cancellationToken = default);
}

public interface IMongoRepoDelete<TDoc> where TDoc : IMongoDocument
{
   void DeleteById(string id);
   Task DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
#if USE_LINQ_TO_MONGO
   void DeleteOne(Expression<Func<TDoc, bool>> filterExpression);
   Task DeleteOneAsync(Expression<Func<TDoc, bool>> filterExpression);
   void DeleteMany(Expression<Func<TDoc, bool>> filterExpression);
   Task DeleteManyAsync(Expression<Func<TDoc, bool>> filterExpression);
#endif
}

public interface IMongoRepoCrud<TDoc> :
   IMongoRepoCreate<TDoc>,
   IMongoRepoRead<TDoc>,
   IMongoRepoUpdate<TDoc>,
   IMongoRepoDelete<TDoc> where TDoc : IMongoDocument
{
}

public interface IMongoRepoQueryable<TDoc> where TDoc : IMongoDocument
{
   IEnumerable<TDoc> All();
   IQueryable<TDoc> AsQueryable();
#if USE_LINQ_TO_MONGO
   IEnumerable<TDoc> FilterBy(Expression<Func<TDoc, bool>> filterExpression);
   IEnumerable<TProjected> FilterBy<TProjected>(
      Expression<Func<TDoc, bool>> filterExpression,
      Expression<Func<TDoc, TProjected>> projectionExpression);
#endif
}

public interface IMongoRepoPageable<TDoc> where TDoc : IMongoDocument
{
   long CalcPageCount(long count, int pageSize);

   Task<(long totalRecords, IEnumerable<TDoc> data)> AggregateByPage(
      FilterDefinition<TDoc> filterDefinition,
      SortDefinition<TDoc> sortDefinition,
      int page,
      int pageSize = 20);
}

public interface IMongoRepository<TDoc> :
   IMongoRepository,
   IMongoRepoCrud<TDoc>,
   IMongoRepoQueryable<TDoc>,
   IMongoRepoPageable<TDoc> where TDoc : IMongoDocument
{
}

