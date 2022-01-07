using System;
using System.Text;
using Microsoft.VisualBasic;

namespace Large_Number_Verbalizer
{
  public class Random_Class
  {
    Random m_clsRnd = new Random();
    int m_iMax_Digits;
    string m_sResult;

    public enum Random_Enum
    {
      SET_LENGTH_MODE,
      MAXIMUM_LENGTH_MODE,
      ANY_LENGTH_MODE
    }
    
    public string Result
    {
      get
      {
        return(m_sResult);
      }
    }
    
    public Random_Class(bool bLong_Scale)
    {
      Max_Digits_Class clsMax_Digits = new Max_Digits_Class();
      m_iMax_Digits = clsMax_Digits.Max_Digits(bLong_Scale);
    }

    public void Compute(Random_Enum enumMode)
    {
      switch(enumMode)
      {
        case Random_Enum.SET_LENGTH_MODE:     Set_Length_Mode();      break;
        case Random_Enum.MAXIMUM_LENGTH_MODE: Maximum_Length_Mode();  break;
        case Random_Enum.ANY_LENGTH_MODE:     Any_Length_Mode();      break;
      }
    }
    
    private void Set_Length_Mode()
    {
      Length_InputBox_Class clsLength_InputBox = new Length_InputBox_Class();
      string sPrompt = "Enter the set number of digits from 1 - " + m_iMax_Digits.ToString() + ":";
      int iLength = clsLength_InputBox.Length_InputBox(m_iMax_Digits, sPrompt);

      Generate_Result(iLength);
    }
    
    private void Maximum_Length_Mode()
    {
      Length_InputBox_Class clsLength_InputBox = new Length_InputBox_Class();
      string sPrompt = "Enter the maximum number of digits from 1 - " + m_iMax_Digits.ToString() + ":";
      int iLength = m_clsRnd.Next(clsLength_InputBox.Length_InputBox(m_iMax_Digits, sPrompt));

      Generate_Result(iLength);
    }
        
    private void Any_Length_Mode()
    {
      int iLength = m_clsRnd.Next(m_iMax_Digits) + 1;
      Generate_Result(iLength);
    }
    
    private void Generate_Result(int iLength)
    {
      int iDigit = m_clsRnd.Next(9) + 1;   // do not want first digit to be '0'
      StringBuilder sbResult = new StringBuilder(iDigit);
      
      for(int iIndex = 1; iIndex < iLength; iIndex++)
      {
        do
        {
          iDigit = m_clsRnd.Next(10);
        } while ((iIndex == 0) && (iDigit == 0));
        sbResult.Append(iDigit);
      }
      m_sResult = sbResult.ToString();
    }
  }
}

