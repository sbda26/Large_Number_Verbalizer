using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Large_Number_Verbalizer
{
  public class Verbalize_Class
  {
    private string m_sResult;
    private string m_sData_File;
    private List<string> m_lstLarge_Numbers_Short = new List<string>();
    private List<string> m_lstLarge_Numbers_Long = new List<string>();

    public string Result
    {
      get
      {
        return m_sResult;
      }
    }
    
    public Verbalize_Class(string sData_File)
    {
      m_sData_File = sData_File;
      Load_Large_Numbers_Short_Scale();
      Load_Large_Numbers_Long_Scale();
    }

    private void Load_Large_Numbers_Short_Scale()
    {
      int iIndex;
      string sLine;
      StreamReader srVery_Large_Numbers;

      m_lstLarge_Numbers_Short.Add("");
      srVery_Large_Numbers = new StreamReader(m_sData_File);
        while (srVery_Large_Numbers.EndOfStream == false)
        {
          sLine = srVery_Large_Numbers.ReadLine();
          iIndex = sLine.IndexOf(" ");
          if (iIndex == -1)
            iIndex = sLine.IndexOf((char) 9);
          if (iIndex > -1)
            m_lstLarge_Numbers_Short.Add(sLine.Substring(0, iIndex).Trim().ToLower());
        }
        srVery_Large_Numbers.Close();
      srVery_Large_Numbers.Dispose();
    }

    private void Load_Large_Numbers_Long_Scale()
    {
      string sValue;
      
      m_lstLarge_Numbers_Long.Add("");
      m_lstLarge_Numbers_Long.Add(m_lstLarge_Numbers_Short[1]);  //  thousand
      for(int iIndex = 2; iIndex < m_lstLarge_Numbers_Short.Count; iIndex++)
      {
        sValue = m_lstLarge_Numbers_Short[iIndex];
        m_lstLarge_Numbers_Long.Add(sValue);
        if (sValue != "millillion")
          m_lstLarge_Numbers_Long.Add(sValue.Replace("ion", "iard"));
      }
    }

    private string Set_String_Even_3(string sValue)
    {
      // set string length to a length evenly divisible by 3, if not already
      
      switch(sValue.Length % 3)
      {
        case 1: sValue = "00" + sValue;  break;
        case 2: sValue = "0" + sValue; break;
      }
      
      return(sValue);
    }

    private string Fixed_Sub_Value(string sSub_Value)
    {
      string sReturn;
      
      if (sSub_Value == "000")
        sReturn = null;
      else if(sSub_Value.StartsWith("00") == true)
        sReturn = sSub_Value.Substring(2, 1);
      else if(sSub_Value.StartsWith("0") == true)
        sReturn = sSub_Value.Substring(1, 2);
      else
        sReturn = sSub_Value;
        
      return(sReturn);
    }
    
    public string Generate_Special_Number(int iZeroes)
    {
      StringBuilder sbValue = new StringBuilder("1");
      
      for (int iIndex = 1; iIndex <= iZeroes; iIndex++)
        sbValue.Append("0");
      
      return(sbValue.ToString());
    }

    public string[] Large_Numbers(bool bLong_Scale)
    {
      string[] arr_sLarge_Numbers;

      if (bLong_Scale == false)
      {
        arr_sLarge_Numbers = new string[m_lstLarge_Numbers_Short.Count];
        m_lstLarge_Numbers_Short.CopyTo(arr_sLarge_Numbers);
      }
      else
      {
        arr_sLarge_Numbers = new string[m_lstLarge_Numbers_Long.Count];
        m_lstLarge_Numbers_Long.CopyTo(arr_sLarge_Numbers);
      }
      return (arr_sLarge_Numbers);
    }
          
    public void Generate_Result(string sValue, bool bLong_Scale)
    {
      bool bOut_of_Range = false;
      int iLarge_Number_List_Index = 0;
      string sSub_Value;
      Stack<string> stkResult = new Stack<string>();

      sValue = Set_String_Even_3(sValue);
      try
      {
        for(int iValue_Index = (sValue.Length - 3); iValue_Index >= 0; iValue_Index -= 3)
        {
          sSub_Value = sValue.Substring(iValue_Index, 3);
          sSub_Value = Fixed_Sub_Value(sSub_Value);
          if (sSub_Value != null)
          {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append(sSub_Value);
            sbResult.Append(" ");
            if(bLong_Scale == false)
              sbResult.Append(m_lstLarge_Numbers_Short[iLarge_Number_List_Index]);
            else
              sbResult.Append(m_lstLarge_Numbers_Long[iLarge_Number_List_Index]);
            stkResult.Push(sbResult.ToString());
            sbResult = null;
          }
          iLarge_Number_List_Index++;
        }
      }
      catch (ArgumentOutOfRangeException)
      {
        stkResult.Clear();
        bOut_of_Range = true;
      }
      finally
      {
        if (stkResult.Count > 0)
        {
          StringBuilder sbResult = new StringBuilder();
          while (stkResult.Count > 0)
          {
            sbResult.Append(stkResult.Pop());
            if (stkResult.Count > 0)
              sbResult.Append(", ");  
          }
          m_sResult = sbResult.ToString().Trim();
        }
        else
          m_sResult = null;
      } // end finally
      if(bOut_of_Range == true)
        throw new ArgumentOutOfRangeException("Cannot display number in short scale - it is beyond 10 to the 3,005th power (100 millillion).");
    } // end function
  } // end class
} // end namespace
