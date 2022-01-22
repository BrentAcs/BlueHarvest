using MediatR;
using Microsoft.Extensions.Configuration;

namespace BlueHarvest.WinUI.Forms;

public partial class BuilderMainForm : Form
{
   private readonly IAppOptions _appOptions;

   public BuilderMainForm(
      IConfiguration configuration,
      IMediator mediator,
      IAppOptions appOptions)
   {
      InitializeComponent();
      _appOptions = appOptions;
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
