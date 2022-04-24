using BlueHarvest.Shared.DTOs.Cosmic;

namespace BlueHarvest.Web.Shared.Services;

public interface IStarClusterService
{
   public Task<IEnumerable<StarClusterDto>?> GetAll();
}

public class StarClusterService : IStarClusterService
{
   private readonly HttpClient _httpClient;

   public StarClusterService(HttpClient httpClient)
   {
      _httpClient = httpClient;
      _httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin","*");
   }

   public async Task<IEnumerable<StarClusterDto>?> GetAll()
   {
      return await JsonSerializer.DeserializeAsync<IEnumerable<StarClusterDto>>
         (await _httpClient.GetStreamAsync($"api/v1/StarClusters").ConfigureAwait(false),
            new JsonSerializerOptions() {PropertyNameCaseInsensitive = true})
         .ConfigureAwait(false);

      // try
      // {
      //    _httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin","*");
      //    var response = await _httpClient.GetStreamAsync($"api/v1/StarClusters").ConfigureAwait(false);
      //    var result = await JsonSerializer.DeserializeAsync<IEnumerable<StarClusterDto>>
      //          (response, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true})
      //       .ConfigureAwait(false);
      //
      //    return result;
      // }
      // catch (Exception ex)
      // {
      //    Console.WriteLine(ex);
      //    throw;
      // }
   }
}
