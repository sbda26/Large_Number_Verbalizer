using System;
using System.Text;
using Microsoft.VisualBasic;

namespace Large_Number_Verbalizer
{
  public class Length_InputBox_Class
  {
    public int Length_InputBox(int iMax_Digits, string sPrompt)
    {
      bool bIsNumeric = false;
      bool bIsValidRange = false;
      int iResult = 0;
      string sResult = null;

      do
      {
        sResult = Microsoft.VisualBasic.Interaction.InputBox(sPrompt, "Large Number Verbalizer", "", -1, -1);
        bIsNumeric = Information.IsNumeric(sResult);
        if (bIsNumeric == true)
        {
          iResult = Convert.ToInt32(sResult);
          bIsValidRange = ((iResult > 0) && (iResult <= iMax_Digits));
        }
      } while ((bIsNumeric == false) || (bIsValidRange == false));

      return (iResult);
    }
  
  }
}
