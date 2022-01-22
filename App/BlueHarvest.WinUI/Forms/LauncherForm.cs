using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueHarvest.WinUI.Forms
{
   public partial class LauncherForm : Form
   {
      private readonly IAppOptions _appOptions;
      private readonly MainForm _mainForm;

      public LauncherForm(IAppOptions appOptions, MainForm mainForm)
      {
         InitializeComponent();
         _appOptions = appOptions;
         _mainForm = mainForm;
      }

      private void LauncherForm_Load(object sender, EventArgs e)
      {
         _appOptions.LauncherForm.Load(this);
      }

      private void LauncherForm_FormClosed(object sender, FormClosedEventArgs e)
      {
         _appOptions.LauncherForm.Save(this);
      }

      private void builderButton_Click_1(object sender, EventArgs e)
      {
         _mainForm.ShowDialog(this);
      }

      private void gameButton_Click(object sender, EventArgs e)
      {

      }

   }
}
