namespace BlueHarvest.Core.Storage.Services;

public interface ICollectionsService
{
   IEnumerable<string> CollectionNames { get; }
}

public class CollectionsService : ICollectionsService
{
   public IEnumerable<string> CollectionNames { get; } = typeof(CollectionNames)
      .GetFields(BindingFlags.Public | BindingFlags.Static)
      .Select(f => f.Name);
}

