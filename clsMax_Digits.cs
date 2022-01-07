using System;
using System.Text;

namespace Large_Number_Verbalizer
{
  public class Max_Digits_Class
  {
    public int Max_Digits(bool bLong_Scale)
    {
      if(bLong_Scale == false)
        return(3006);
      else
        return(6003);
    }
  }
}
