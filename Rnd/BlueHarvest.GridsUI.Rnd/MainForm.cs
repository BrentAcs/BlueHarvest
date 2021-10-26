using System.Diagnostics;
using BlueHarvest.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BlueHarvest.GridsUI.Rnd;

public partial class MainForm : Form
{
   private readonly IAppOptions _appOptions;

   public MainForm(
      IConfiguration configuration,
      IAppOptions appOptions)
   {
      _appOptions = appOptions;
      InitializeComponent();
   }

   private void MainForm_Load(object sender, EventArgs e)
   {
      _appOptions.MainForm.Load(this);
   }

   private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
   {
      _appOptions.MainForm.Save(this);
   }
}

