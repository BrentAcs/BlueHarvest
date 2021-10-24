using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlueHarvest.Core.Extensions
{
   // Ref:  https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to?pivots=dotnet-5-0

   public static class JsonCovertExtensions
   {
      public static string? ToJson(this object? obj,
         Formatting formatting = Formatting.None,
         JsonSerializerSettings? settings = null)
      {
         if (obj == null)
            return null;

         settings ??= new JsonSerializerSettings();

         return JsonConvert.SerializeObject(obj, formatting, settings);
      }

      public static string? ToJsonIndented(this object obj) =>
         obj.ToJson(Formatting.Indented);

      public static void ToJsonFile(this object obj,
         string filename,
         Formatting formatting = Formatting.Indented,
         JsonSerializerSettings? settings = null)
      {
         settings ??= new JsonSerializerSettings {DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate};

         File.WriteAllText(filename, obj.ToJson(formatting, settings));
      }

      public static T? FromJson<T>(this string json, JsonSerializerSettings? settings = null)
      {
         settings ??= new JsonSerializerSettings();
         return JsonConvert.DeserializeObject<T>(json, settings);
      }

      public static T? FromJsonFile<T>(this string filename, JsonSerializerSettings? settings = null)
      {
         settings ??= new JsonSerializerSettings {DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate};
         return File.ReadAllText(filename).FromJson<T>(settings);
      }

      public static JObject ToJObject(this Stream stream)
      {
         stream.Position = 0;
         using var reader = new StreamReader(stream);
         var body = reader.ReadToEnd();
         return JObject.Parse(body);
      }

      public static async Task<JObject> ToJObjectAsync(this Stream stream)
      {
         stream.Position = 0;
         using var reader = new StreamReader(stream);
         var body = await reader.ReadToEndAsync();
         return JObject.Parse(body);
      }
   }
}
