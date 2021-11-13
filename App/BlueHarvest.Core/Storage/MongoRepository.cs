namespace BlueHarvest.Core.Storage;

public class MongoRepository<TDoc> : IMongoRepository<TDoc> where TDoc : IDocument
{
   private readonly IMongoContext _mongoContext;

   public MongoRepository(IMongoContext mongoContext)
   {
      _mongoContext = mongoContext;
      Collection = _mongoContext.Db.GetCollection<TDoc>(CollectionName);
   }

   protected IMongoCollection<TDoc> Collection { get; }

   private static string? GetCollectionName(Type documentType) =>
      ((BsonCollectionAttribute)documentType.GetCustomAttributes(
            typeof(BsonCollectionAttribute),
            true)
         .FirstOrDefault()!)?.CollectionName;

   public string? CollectionName => GetCollectionName(typeof(TDoc));
   
   public virtual Task InitializeAsync() =>
      Task.CompletedTask;

   public long CalcPageCount(long count, int pageSize) =>
      (long)Math.Ceiling((double)count / pageSize);

   public IEnumerable<TDoc> All() =>
      AsQueryable()
         .ToList();

   public virtual IQueryable<TDoc> AsQueryable() =>
      Collection.AsQueryable();

   public virtual IEnumerable<TDoc> FilterBy(Expression<Func<TDoc, bool>> filterExpression) =>
      Collection.Find(filterExpression).ToEnumerable();

   public virtual IEnumerable<TProjected> FilterBy<TProjected>(
      Expression<Func<TDoc, bool>> filterExpression,
      Expression<Func<TDoc, TProjected>> projectionExpression) =>
      Collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();

   public virtual TDoc FindOne(Expression<Func<TDoc, bool>> filterExpression) =>
      Collection.Find(filterExpression).FirstOrDefault();

   public virtual Task<TDoc> FindOneAsync(Expression<Func<TDoc, bool>> filterExpression) =>
      Task.Run(() => Collection.Find(filterExpression).FirstOrDefaultAsync());

   public virtual TDoc FindById(string id)
   {
      var objectId = new ObjectId(id);
      var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, objectId);
      return Collection.Find(filter).SingleOrDefault();
   }

   public virtual Task<TDoc> FindByIdAsync(string id) =>
      Task.Run(() =>
      {
         var objectId = new ObjectId(id);
         var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, objectId);
         return Collection.Find(filter).SingleOrDefaultAsync();
      });

   public virtual void InsertOne(TDoc document) =>
      Collection.InsertOne(document);

   public virtual Task InsertOneAsync(TDoc document) =>
      Task.Run(() => Collection.InsertOneAsync(document));

   public void InsertMany(ICollection<TDoc> documents) =>
      Collection.InsertMany(documents);

   public virtual async Task InsertManyAsync(ICollection<TDoc> documents) =>
      await Collection.InsertManyAsync(documents);

   public void ReplaceOne(TDoc document)
   {
      var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, document.Id);
      Collection.FindOneAndReplace(filter, document);
   }

   public virtual async Task ReplaceOneAsync(TDoc document)
   {
      var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, document.Id);
      await Collection.FindOneAndReplaceAsync(filter, document).ConfigureAwait(false);
   }

   public void DeleteOne(Expression<Func<TDoc, bool>> filterExpression) =>
      Collection.FindOneAndDelete(filterExpression);

   public Task DeleteOneAsync(Expression<Func<TDoc, bool>> filterExpression) =>
      Task.Run(() => Collection.FindOneAndDeleteAsync(filterExpression));

   public void DeleteById(string id)
   {
      var objectId = new ObjectId(id);
      var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, objectId);
      Collection.FindOneAndDelete(filter);
   }

   public Task DeleteByIdAsync(string id) =>
      Task.Run(() =>
      {
         var objectId = new ObjectId(id);
         var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, objectId);
         Collection.FindOneAndDeleteAsync(filter);
      });

   public void DeleteMany(Expression<Func<TDoc, bool>> filterExpression) =>
      Collection.DeleteMany(filterExpression);

   public Task DeleteManyAsync(Expression<Func<TDoc, bool>> filterExpression) =>
      Task.Run(() => Collection.DeleteManyAsync(filterExpression));

   // --- Enhanced functionality

   public async Task<(long totalRecords, IEnumerable<TDoc> data)> AggregateByPage(
      FilterDefinition<TDoc> filterDefinition,
      SortDefinition<TDoc> sortDefinition,
      int page,
      int pageSize = 20)
   {
      // ref:  https://kevsoft.net/2020/01/27/paging-data-in-mongodb-with-csharp.html
      var countFacet = AggregateFacet.Create("count",
         PipelineDefinition<TDoc, AggregateCountResult>
            .Create(new[] {PipelineStageDefinitionBuilder.Count<TDoc>()}));

      var dataFacet = AggregateFacet.Create("data",
         PipelineDefinition<TDoc, TDoc>.Create(new[]
         {
            PipelineStageDefinitionBuilder
               .Sort(sortDefinition),
            PipelineStageDefinitionBuilder.Skip<TDoc>((page - 1) * pageSize), PipelineStageDefinitionBuilder
               .Limit<TDoc>(pageSize),
         }));

      var aggregation = await Collection.Aggregate()
         .Match(filterDefinition)
         .Facet(countFacet, dataFacet)
         .ToListAsync();

      var count = aggregation.First()
         .Facets.First(x => x.Name == "count")
         .Output<AggregateCountResult>()
         ?.FirstOrDefault()
         ?.Count;

      //var totalPages = (int)Math.Ceiling((double)count / pageSize);

      var data = aggregation.First()
         .Facets.First(x => x.Name == "data")
         .Output<TDoc>();

      return (count.Value, data);
      //return (totalPages, data);
   }
}
