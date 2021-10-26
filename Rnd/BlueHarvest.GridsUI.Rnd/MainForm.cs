using System.Diagnostics;
using BlueHarvest.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BlueHarvest.GridsUI.Rnd;

public partial class MainForm : Form, IHostedService
{
   private readonly AppOptions _appOptions;

   public MainForm(IHostApplicationLifetime hostApplicationLifetime,
      IConfiguration configuration,
      AppOptions appOptions)
   {
      // hostApplicationLifetime.ApplicationStarted.Register(() =>
      // {
      //    Trace.WriteLine("app stared.");
      // });
      // hostApplicationLifetime.ApplicationStopped.Register(() =>
      // {
      //    Trace.WriteLine("app stopped.");
      // });
      
      
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

   public Task StartAsync(CancellationToken cancellationToken)
   {
      return Task.CompletedTask;
   }

   public Task StopAsync(CancellationToken cancellationToken)
   {
      return Task.CompletedTask;
   }
}

