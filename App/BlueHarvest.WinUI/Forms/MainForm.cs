using Microsoft.Extensions.Configuration;

namespace BlueHarvest.WinUI.Forms;

public partial class MainForm : Form
{
   private readonly IAppOptions _appOptions;

   public MainForm(
      IConfiguration configuration,
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
