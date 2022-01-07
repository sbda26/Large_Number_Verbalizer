namespace Large_Number_Verbalizer
{
  partial class ReadMe
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
      this.richtxtReadMe = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // richtxtReadMe
      // 
      this.richtxtReadMe.DetectUrls = false;
      this.richtxtReadMe.Location = new System.Drawing.Point(13, 13);
      this.richtxtReadMe.Name = "richtxtReadMe";
      this.richtxtReadMe.ReadOnly = true;
      this.richtxtReadMe.Size = new System.Drawing.Size(800, 518);
      this.richtxtReadMe.TabIndex = 0;
      this.richtxtReadMe.Text = "";
      // 
      // ReadMe
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(825, 543);
      this.Controls.Add(this.richtxtReadMe);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ReadMe";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Large Number Verbalizer ReadMe";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox richtxtReadMe;
  }
}