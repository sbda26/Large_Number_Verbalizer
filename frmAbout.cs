using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Large_Number_Verbalizer
{
  partial class About : Form
  {
    private double m_dOpacity = 1.0;
    
    public About()
    {
      InitializeComponent();
    }
    
    private void btnOK_Click(object sender, EventArgs e)
    {
      Humor_Class clsHumor = new Humor_Class();
      
      if(clsHumor.Humor == true)
      {
        const int ciMS = 1500;
        Random_Texturizer cls = new Random_Texturizer(this.Controls);
        tmrAbout.Interval = ciMS / 10;
        tmrAbout.Enabled = true;
        clsHumor.Play_Sound("About_Close.wav", false);
        cls.Scramble(this.Controls, ciMS);
      }
      this.Close();
    }

    private void tmrAbout_Tick(object sender, EventArgs e)
    {
      m_dOpacity -= 0.1;
      this.Opacity = m_dOpacity;
    }

    private void btnView_ReadMe_Click(object sender, EventArgs e)
    {
      ReadMe frmReadMe = new ReadMe();
      frmReadMe.ShowDialog();
    }
  }
}
