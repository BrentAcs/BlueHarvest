namespace BlueHarvest.Core.Extensions;

public static class JsonCovertExtensions
{
   public static string? AsJson(this object? obj, JsonSerializerSettings? settings = null)
   {
      if (obj == null)
         return null;

      settings ??= new JsonSerializerSettings();

      return JsonConvert.SerializeObject(obj, settings);
   }

   public static string? AsJsonIndented(this object obj) =>
      obj.AsJson(new JsonSerializerSettings {Formatting = Formatting.Indented});

   public static void ToJsonFile(this object obj,
      string filename,
      // Formatting formatting = Formatting.Indented,
      JsonSerializerSettings? settings = null)
   {
      settings ??= new JsonSerializerSettings
      {
         DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
         Formatting = Formatting.Indented
      };

      File.WriteAllText(filename, obj.AsJson(settings));
   }

   public static T? FromJson<T>(this string json, JsonSerializerSettings? settings = null)
   {
      settings ??= new JsonSerializerSettings();
      settings.TypeNameHandling = TypeNameHandling.All;
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
