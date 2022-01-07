using System;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace Large_Number_Verbalizer
{
  public class Humor_Class
  {
    private Properties.Settings m_ps = new Properties.Settings();
    private Nuke m_frmNuke;
    
    public bool Humor
    {
      get
      {
        return(m_ps.Humor);
      }
      set
      {
        m_ps.Humor = value;
        m_ps.Save();
      }
    }

    public void Set_Humor_Menu_Items(ref ToolStripMenuItem tsmiOn, ref ToolStripMenuItem tsmiOff)
    {
      tsmiOn.Checked = m_ps.Humor;
      tsmiOff.Checked = !tsmiOn.Checked;
    }

    public void Instantiate_Nuke_Form()
    {
      m_frmNuke = new Nuke();
    }
    
    public void Launch_Nuke_Form()
    {
      m_frmNuke.Width = Screen.PrimaryScreen.WorkingArea.Width;
      m_frmNuke.Height = Screen.PrimaryScreen.WorkingArea.Height;
      m_frmNuke.Show();
        Play_Sound("Explosion.wav", true);
      m_frmNuke.Close();
      m_frmNuke.Dispose();            
    }

    public void Close_Flush(Form frmMain)
    {
      Play_Sound("Flush.wav", false);
      while (frmMain.Top <= Screen.PrimaryScreen.WorkingArea.Height)
      {
        frmMain.Top += 2;
        Thread.Sleep(25);
      }
    }

    public void Play_Sound(string sWAV_File, bool bPlaySync)
    {
      SoundPlayer sp = new SoundPlayer(sWAV_File);

      if (bPlaySync == true)
        sp.PlaySync();
      else
        sp.Play();
    }
    
    public void About_Form()
    {
      About frmAbout = new About();
      
      int iTop = ((Screen.PrimaryScreen.WorkingArea.Height / 2) - (frmAbout.Height / 2));
      frmAbout.Show();  // must be before .Top and .Left
      frmAbout.Top = -(Screen.PrimaryScreen.WorkingArea.Height - 90);
      frmAbout.Left = ((Screen.PrimaryScreen.WorkingArea.Width / 2) - (frmAbout.Width / 2));
      Play_Sound("About1.wav", false);
      while (frmAbout.Top < iTop)
      {
        frmAbout.Top += 5;
        System.Threading.Thread.Sleep(10);
      }
      Play_Sound("About2.wav", false);
    }
  }
}
