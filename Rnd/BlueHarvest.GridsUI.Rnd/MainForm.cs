using BlueHarvest.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace BlueHarvest.GridsUI.Rnd;

public partial class MainForm : Form
{
   public MainForm(IConfiguration configuration, AppOptions appOptions)
   {
      InitializeComponent();

      // var path= configuration[ "OptionsPath" ];
      // var opts = new FormOptions();
      //
      // opts.ToJsonFile(path);

      Location = appOptions.MainForm.Location;
      Size = appOptions.MainForm.Size;

      // configuration.GetSection("MainForm").Bind(opts);

   }
}

