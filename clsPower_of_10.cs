using System;
using System.Text;

namespace Large_Number_Verbalizer
{
  public class Power_of_10_Class
  {
    public string Get_Power_of_10(bool bLong_Scale)
    {
      Max_Digits_Class clsMax_Digits = new Max_Digits_Class();
      Length_InputBox_Class clsLength_InputBox = new Length_InputBox_Class();
      int iMax_Digits;
      int iPower;
      string sPrompt;
      string sResult;
      
      iMax_Digits = clsMax_Digits.Max_Digits(bLong_Scale);
      sPrompt = "Please enter the power of 10 (max: " + iMax_Digits.ToString() + ")";
      iPower = clsLength_InputBox.Length_InputBox(iMax_Digits, sPrompt);
      sResult = Generate_Number(iPower);
      
      return(sResult);
    }
    
    private string Generate_Number(int iPower)
    {
      StringBuilder sbNumber = new StringBuilder("1");
      
      for(int iIndex = 1; iIndex <= iPower; iIndex++)
        sbNumber.Append("0");
        
      return(sbNumber.ToString());
    }
   
  }
}
