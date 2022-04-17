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
   public partial class GameMainForm : Form
   {
      private readonly IAppOptions _appOptions;

      public GameMainForm(IAppOptions appOptions)
      {
         _appOptions = appOptions;
         InitializeComponent();
      }

      private void GameMainForm_Load(object sender, EventArgs e)
      {

      }

      private void GameMainForm_FormClosed(object sender, FormClosedEventArgs e)
      {

      }
   }
}
