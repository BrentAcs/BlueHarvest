using MediatR;

namespace BlueHarvest.WinUI.Controls
{
   internal interface IBlueHarvestUserControl
   {
      IMediator Mediator { get; set; }
      IServiceProvider ServiceProvider { get; set; }
   }
}
