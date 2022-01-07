using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Large_Number_Verbalizer
{
  public partial class ReadMe : Form
  {
    public ReadMe()
    {
      InitializeComponent();
      richtxtReadMe.LoadFile("ReadMe.rtf", RichTextBoxStreamType.RichText);
    }
    
  }
}
