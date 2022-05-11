using BlueHarvest.WinUI.Controls;
using MediatR;

namespace BlueHarvest.WinUI;

public partial class MainForm : Form
{
   public MainForm(IMediator mediator, IServiceProvider serviceProvider, IStarClusterApi starClusterApi)
   {
      InitializeComponent();
      Mediator = mediator;
      ServiceProvider = serviceProvider;
      StarClusterApi = starClusterApi;

      //Console.WriteLine($"Main form...");
      //var baseApiUrl = Settings.Default["BaseApiUrl"];
      //Console.WriteLine($"Base url... {baseApiUrl}");

      //var test = await starClusterApi.GetAll();

   }

   public IMediator Mediator { get; }
   public IServiceProvider ServiceProvider { get; }
   public IStarClusterApi StarClusterApi { get; }

   private async void MainForm_Load(object sender, EventArgs e)
   {
      var children = Controls.OfType<IBlueHarvestUserControl>().ToList();
      children.First().Mediator = Mediator;
      children.First().ServiceProvider = ServiceProvider;

      //var test = await Task.Run(() => StarClusterApi.GetAll());
   }
}
