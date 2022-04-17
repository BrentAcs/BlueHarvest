using System.Reflection;
using MongoDB.Bson.Serialization;

namespace BlueHarvest.Core.Infrastructure.Storage;

public static class Misc
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
