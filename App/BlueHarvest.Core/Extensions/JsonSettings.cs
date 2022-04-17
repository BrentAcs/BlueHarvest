namespace BlueHarvest.Core.Extensions;

public static class JsonSettings
{
   public static JsonSerializerSettings Formatted => new() {Formatting = Formatting.Indented};

   public static JsonSerializerSettings FormattedNamedEnums
   {
      get
      {
         var settings = new JsonSerializerSettings
         {
            Formatting = Formatting.Indented,
            //NullValueHandling = NullValueHandling.Ignore,
         };
         settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

         return settings;
      }
   }

   public static JsonSerializerSettings FormattedTypedNamedEnums
   {
      get
      {
         var settings = new JsonSerializerSettings
         {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
         };
         settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

         return settings;
      }
   }
}
