using BlueHarvest.Shared.DTOs.Cosmic;
using Flurl;
using Flurl.Http;

namespace BlueHarvest.WinUI;

public interface IStarClusterApi
{
   Task<IEnumerable<StarClusterDto>> GetAll();
}

public abstract class BlueHarvestApi
{
   public BlueHarvestApi(IAppSettings appSettings)
   {
      AppSettings = appSettings;
   }

   public IAppSettings AppSettings { get; }

   protected abstract string RouteBase { get; }
   protected virtual string ApiUrl => AppSettings.BaseApiUrl.AppendPathSegment(RouteBase);
}


public class StarClusterApi : BlueHarvestApi, IStarClusterApi
{
   public StarClusterApi(IAppSettings appSettings) : base(appSettings)
   {
   }

   protected override string RouteBase => "StarClusters";

   public async Task<IEnumerable<StarClusterDto>> GetAll()
   {
      //curl - X 'GET' \
      //  'https://localhost:7013/api/v1/StarClusters' \
      //  -H 'accept: application/json'

      return await ApiUrl.GetJsonAsync<IEnumerable<StarClusterDto>>();
   }
}
