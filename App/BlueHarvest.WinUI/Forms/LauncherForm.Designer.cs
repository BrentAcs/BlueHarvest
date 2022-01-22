namespace BlueHarvest.WinUI.Forms
{
   partial class LauncherForm
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
            this.gameButton = new System.Windows.Forms.Button();
            this.builderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameButton
            // 
            this.gameButton.Location = new System.Drawing.Point(34, 50);
            this.gameButton.Name = "gameButton";
            this.gameButton.Size = new System.Drawing.Size(120, 32);
            this.gameButton.TabIndex = 1;
            this.gameButton.Text = "Game";
            this.gameButton.UseVisualStyleBackColor = true;
            this.gameButton.Click += new System.EventHandler(this.gameButton_Click);
            // 
            // builderButton
            // 
            this.builderButton.Location = new System.Drawing.Point(34, 12);
            this.builderButton.Name = "builderButton";
            this.builderButton.Size = new System.Drawing.Size(120, 32);
            this.builderButton.TabIndex = 1;
            this.builderButton.Text = "Builder";
            this.builderButton.UseVisualStyleBackColor = true;
            this.builderButton.Click += new System.EventHandler(this.builderButton_Click_1);
            // 
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(179, 291);
            this.Controls.Add(this.gameButton);
            this.Controls.Add(this.builderButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(195, 330);
            this.MinimizeBox = false;
            this.Name = "LauncherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BlueHarvest Launcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LauncherForm_FormClosed);
            this.Load += new System.EventHandler(this.LauncherForm_Load);
            this.ResumeLayout(false);

      }

      #endregion
      private Button gameButton;
      private Button builderButton;
   }
}
