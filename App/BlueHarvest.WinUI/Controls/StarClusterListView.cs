using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;

namespace BlueHarvest.WinUI.Controls
{
   public partial class StarClusterListView : UserControl //, IBlueHarvestUserControl
   {
      public StarClusterListView(IStarClusterApi api)
      {
         InitializeComponent();
      }

      //public IMediator Mediator { get; set; }
      //public IServiceProvider ServiceProvider { get; set; }
   }
}
