namespace BlueHarvest.Core.Storage;

public static class Utilities
{
   public static void RegisterKnownTypes<T>(Assembly? assembly=null)
   {
      var type = typeof(T);
      assembly ??= type.Assembly;

      BsonClassMap.RegisterClassMap<T>(cm => {
         cm.AutoMap();
         cm.SetIsRootClass(true);
         
         assembly.GetTypes()
            .Where(type.IsAssignableFrom)
            .ToList()
            .ForEach(cm.AddKnownType);
      });
   }
}
