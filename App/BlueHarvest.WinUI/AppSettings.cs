using BlueHarvest.Shared.DTOs.Cosmic;

namespace BlueHarvest.WinUI;

public interface IAppSettings
{
   string BaseApiUrl { get; }
}

public class AppSettings : IAppSettings
{
   public string BaseApiUrl => (string)Settings.Default[nameof(BaseApiUrl)];
}
