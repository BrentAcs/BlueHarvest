//#define USE_LINQ_TO_MONGO
namespace BlueHarvest.Core.Storage;

public interface IMongoRepository
{
   string? CollectionName { get; }
   Task InitializeIndexesAsync();
   Task SeedDataAsync();
}

// https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e
public interface IMongoRepository<TDoc> : IMongoRepository where TDoc : IMongoDocument
{
   long CalcPageCount(long count, int pageSize);

   IEnumerable<TDoc> All();
   IQueryable<TDoc> AsQueryable();

#if USE_LINQ_TO_MONGO
   IEnumerable<TDoc> FilterBy(Expression<Func<TDoc, bool>> filterExpression);
   IEnumerable<TProjected> FilterBy<TProjected>(
      Expression<Func<TDoc, bool>> filterExpression,
      Expression<Func<TDoc, TProjected>> projectionExpression);
#endif

#if USE_LINQ_TO_MONGO
   TDoc FindOne(Expression<Func<TDoc, bool>> filterExpression);
   Task<TDoc> FindOneAsync(Expression<Func<TDoc, bool>> filterExpression);
#endif
   TDoc FindById(string id);
   Task<TDoc> FindByIdAsync(string id);

   void InsertOne(TDoc document);
   Task InsertOneAsync(TDoc document);
   void InsertMany(ICollection<TDoc> documents);
   Task InsertManyAsync(ICollection<TDoc> documents);

   void ReplaceOne(TDoc document);
   Task ReplaceOneAsync(TDoc document);

   void DeleteById(string id);
   Task DeleteByIdAsync(string id);
#if USE_LINQ_TO_MONGO
   void DeleteOne(Expression<Func<TDoc, bool>> filterExpression);
   Task DeleteOneAsync(Expression<Func<TDoc, bool>> filterExpression);
   void DeleteMany(Expression<Func<TDoc, bool>> filterExpression);
   Task DeleteManyAsync(Expression<Func<TDoc, bool>> filterExpression);
#endif

   Task<(long totalRecords, IEnumerable<TDoc> data)> AggregateByPage(
      FilterDefinition<TDoc> filterDefinition,
      SortDefinition<TDoc> sortDefinition,
      int page,
      int pageSize = 20);
}
