namespace Large_Number_Verbalizer
{
  partial class About
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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
      this.components = new System.ComponentModel.Container();
      this.lblTitle = new System.Windows.Forms.Label();
      this.lblCopyright = new System.Windows.Forms.Label();
      this.lblFreeware = new System.Windows.Forms.Label();
      this.lblSend_Questions = new System.Windows.Forms.Label();
      this.lnklblEMail = new System.Windows.Forms.LinkLabel();
      this.btnView_ReadMe = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.tmrAbout = new System.Windows.Forms.Timer(this.components);
      this.pnlLogo = new System.Windows.Forms.Panel();
      this.SuspendLayout();
      // 
      // lblTitle
      // 
      this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTitle.Location = new System.Drawing.Point(155, 9);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(268, 23);
      this.lblTitle.TabIndex = 1;
      this.lblTitle.Text = "Dave\'s Large Number Verbalizer";
      this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblCopyright
      // 
      this.lblCopyright.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCopyright.Location = new System.Drawing.Point(155, 44);
      this.lblCopyright.Name = "lblCopyright";
      this.lblCopyright.Size = new System.Drawing.Size(268, 23);
      this.lblCopyright.TabIndex = 2;
      this.lblCopyright.Text = "(c) 2010 All Rights Refused";
      this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblFreeware
      // 
      this.lblFreeware.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
      this.lblFreeware.Location = new System.Drawing.Point(155, 114);
      this.lblFreeware.Name = "lblFreeware";
      this.lblFreeware.Size = new System.Drawing.Size(268, 23);
      this.lblFreeware.TabIndex = 3;
      this.lblFreeware.Text = "This program is freeware.";
      this.lblFreeware.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblSend_Questions
      // 
      this.lblSend_Questions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSend_Questions.Location = new System.Drawing.Point(156, 158);
      this.lblSend_Questions.Name = "lblSend_Questions";
      this.lblSend_Questions.Size = new System.Drawing.Size(123, 23);
      this.lblSend_Questions.TabIndex = 4;
      this.lblSend_Questions.Text = "Send all questions to:";
      this.lblSend_Questions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lnklblEMail
      // 
      this.lnklblEMail.AutoSize = true;
      this.lnklblEMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
      this.lnklblEMail.Location = new System.Drawing.Point(285, 161);
      this.lnklblEMail.Name = "lnklblEMail";
      this.lnklblEMail.Size = new System.Drawing.Size(132, 17);
      this.lnklblEMail.TabIndex = 5;
      this.lnklblEMail.TabStop = true;
      this.lnklblEMail.Text = "sbda26@gmail.com";
      this.lnklblEMail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // btnView_ReadMe
      // 
      this.btnView_ReadMe.Location = new System.Drawing.Point(159, 257);
      this.btnView_ReadMe.Name = "btnView_ReadMe";
      this.btnView_ReadMe.Size = new System.Drawing.Size(120, 23);
      this.btnView_ReadMe.TabIndex = 6;
      this.btnView_ReadMe.Text = "View ReadMe";
      this.btnView_ReadMe.UseVisualStyleBackColor = true;
      this.btnView_ReadMe.Click += new System.EventHandler(this.btnView_ReadMe_Click);
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(303, 257);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(120, 23);
      this.btnOK.TabIndex = 7;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // tmrAbout
      // 
      this.tmrAbout.Tick += new System.EventHandler(this.tmrAbout_Tick);
      // 
      // pnlLogo
      // 
      this.pnlLogo.BackgroundImage = global::Large_Number_Verbalizer.Properties.Resources.Exponent10;
      this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.pnlLogo.Location = new System.Drawing.Point(4, 4);
      this.pnlLogo.Name = "pnlLogo";
      this.pnlLogo.Size = new System.Drawing.Size(145, 277);
      this.pnlLogo.TabIndex = 0;
      // 
      // About
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(435, 283);
      this.ControlBox = false;
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnView_ReadMe);
      this.Controls.Add(this.lnklblEMail);
      this.Controls.Add(this.lblSend_Questions);
      this.Controls.Add(this.lblFreeware);
      this.Controls.Add(this.lblCopyright);
      this.Controls.Add(this.lblTitle);
      this.Controls.Add(this.pnlLogo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "About";
      this.Padding = new System.Windows.Forms.Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "About Large Number Verbalizer...";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlLogo;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Label lblCopyright;
    private System.Windows.Forms.Label lblFreeware;
    private System.Windows.Forms.Label lblSend_Questions;
    private System.Windows.Forms.LinkLabel lnklblEMail;
    private System.Windows.Forms.Button btnView_ReadMe;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Timer tmrAbout;


  }
}
