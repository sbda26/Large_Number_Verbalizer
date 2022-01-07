using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Large_Number_Verbalizer
{
  public class Random_Texturizer
  {
    private struct Texturizer_Struct
    {
      public string sOriginal_Text;
      public string sControl_Name;
    }
    
    List<Texturizer_Struct> m_lstOriginal_Text = new List<Texturizer_Struct>();
    
    public Random_Texturizer(Control.ControlCollection ccControls)
    {
      Texturizer_Struct udtOrig;
      
      for(int iIndex = 0; iIndex < ccControls.Count; iIndex++)
      {
        if (ccControls[iIndex].GetType() != typeof(MenuStrip))
        {
          udtOrig.sOriginal_Text = ccControls[iIndex].Text;
          udtOrig.sControl_Name = ccControls[iIndex].Name;
          m_lstOriginal_Text.Add(udtOrig);
        }
        else
        {
          MenuStrip ms = (MenuStrip) ccControls[iIndex]; 
          for (int iMS_Index = 0; iMS_Index < ms.Items.Count; iMS_Index++)
          {
            udtOrig.sOriginal_Text = ms.Items[iMS_Index].Text;
            udtOrig.sControl_Name = ms.Items[iMS_Index].Name;
            m_lstOriginal_Text.Add(udtOrig);
          }
        }
      }
    }
    
    public void Scramble(Control.ControlCollection ccControls, double dMilliseconds)
    {
      DateTime dtFinish = DateTime.Now.AddMilliseconds(dMilliseconds);
      
      do
      {
        for(int iIndex = 0; iIndex < ccControls.Count; iIndex++)
        {
          if (ccControls[iIndex].GetType() != typeof(ListBox))
          {
            if (ccControls[iIndex].GetType() != typeof(MenuStrip))
              ccControls[iIndex].Text = Scramble_Text(ccControls[iIndex].Text);
            else
            {
              MenuStrip ms = (MenuStrip) ccControls[iIndex];
              for(int iMS_Index = 0; iMS_Index < ms.Items.Count; iMS_Index++)
                ms.Items[iMS_Index].Text = Scramble_Text(ms.Items[iMS_Index].Text);
            }
          }
        }
        Application.DoEvents();
      } while (DateTime.Now <= dtFinish);
      Restore_Text(ccControls);
    }
    
    private void Restore_Text(Control.ControlCollection ccControls)
    {
      for (int iIndex = 0; iIndex < ccControls.Count; iIndex++)
      {
        if (ccControls[iIndex].GetType() != typeof(MenuStrip))
          for(int iOrig_Index = 0; iOrig_Index < m_lstOriginal_Text.Count; iOrig_Index++)
          {
            if(ccControls[iIndex].Name == m_lstOriginal_Text[iOrig_Index].sControl_Name)
            {
              ccControls[iIndex].Text = m_lstOriginal_Text[iOrig_Index].sOriginal_Text;
              break;
            }
          }
        else
        {
          MenuStrip ms = (MenuStrip) ccControls[iIndex];
          for(int iMS_Index = 0; iMS_Index < ms.Items.Count; iMS_Index++)
          {
            for(int iOrig_Index = 0; iOrig_Index < m_lstOriginal_Text.Count; iOrig_Index++)
            {
              if(ms.Items[iMS_Index].Name == m_lstOriginal_Text[iOrig_Index].sControl_Name)
              {
                ms.Items[iMS_Index].Text = m_lstOriginal_Text[iOrig_Index].sOriginal_Text; 
                break;
              }
            }
          }
        }
      }
    }
    
    private string Scramble_Text(string sText)
    {
      StringBuilder sbScramble = new StringBuilder();
      int iLength = sText.Length;

      if (iLength > 1)
      {
        int[] arr_iIndex = new int[iLength];
        int iUBound = arr_iIndex.GetUpperBound(0);
        int iIndex;
        for(iIndex = 0; iIndex <= iUBound; iIndex++)
          arr_iIndex[iIndex] = -1;
        for(iIndex = 0; iIndex <= iUBound; iIndex++)
        {
          bool bGood = false;
          int iRandom_Index;
          Random cls = new Random();
          do
          {
            iRandom_Index = cls.Next(iUBound + 1);
            if (arr_iIndex.Contains(iRandom_Index) == false)
            {
              arr_iIndex[iIndex] = iRandom_Index;
              bGood = true;
            }
          } while (bGood == false);
        }
        for(iIndex = 0; iIndex <= iUBound; iIndex++)
          sbScramble.Append(sText.Substring(arr_iIndex[iIndex], 1));
        sText = sbScramble.ToString();
      }
      return(sText);
    }
  }
}
