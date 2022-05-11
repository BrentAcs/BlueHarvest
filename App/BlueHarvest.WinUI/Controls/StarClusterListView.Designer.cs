namespace BlueHarvest.WinUI.Controls
{
   partial class StarClusterListView
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
            this.theListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // theListView
            // 
            this.theListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theListView.Location = new System.Drawing.Point(0, 0);
            this.theListView.Name = "theListView";
            this.theListView.Size = new System.Drawing.Size(220, 308);
            this.theListView.TabIndex = 0;
            this.theListView.UseCompatibleStateImageBehavior = false;
            // 
            // StarClusterListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.theListView);
            this.Name = "StarClusterListView";
            this.Size = new System.Drawing.Size(220, 308);
            this.ResumeLayout(false);

      }

      #endregion

      private ListView theListView;
   }
}
