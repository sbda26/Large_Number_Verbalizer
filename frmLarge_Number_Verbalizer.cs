using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Large_Number_Verbalizer
{
  public partial class frmLarge_Number_Verbalizer : Form
  {
    private Verbalize_Class m_clsVerbalize = new Verbalize_Class("Very_Large_Numbers.txt");
    private Light_Measurements_Class m_clsLight = new Light_Measurements_Class();
    private Humor_Class m_clsHumor = new Humor_Class();
    private int m_iForm_Left;

#region "Private non-event subs"

    private void Adjust_ListBox()
    {
      int iText_Index = txtNumber.Text.Length / 3;

      if ((txtNumber.Text.Length % 3) == 0)
        iText_Index--;

      lstCurrently_At.SelectedIndex = iText_Index;
    }

    private void Adjust_Base()
    {
      int iLength = txtNumber.Text.Length;

      switch (iLength)
      {
        case 0:
          lbl10_Base.Text = "0";
          break;
        case 1:
          lbl10_Base.Text = "10";
          break;
        default:
          int iDecimal_Places;
          if (iLength > 2)
            iDecimal_Places = 2;
          else
            iDecimal_Places = 1;
          lbl10_Base.Text = txtNumber.Text.Substring(0, 1) + "." + txtNumber.Text.Substring(1, iDecimal_Places) + " X 10";
          break;
      }
    }

    private void Adjust_Exponent()
    {
      int iLength = txtNumber.Text.Length;

      switch (iLength)
      {
        case 0:
          lbl10_Exponent.Text = "0";
          break;
        case 1:
          double dBase = Convert.ToDouble(txtNumber.Text);
          lbl10_Exponent.Text = Math.Log10(dBase).ToString();
          break;
        default:
          lbl10_Exponent.Text = (iLength - 1).ToString();
          break;
      }
    }

    private void Set_Light(decimal decValue)
    {
      txtNumber.Text = decValue.ToString();
    }
    
    private void Set_Light(decimal decPicometers_Value, int iZeroes)
    {
      StringBuilder sbZeroes = new StringBuilder(decPicometers_Value.ToString());
      
      for (int iIndex = 1; iIndex <= iZeroes; iIndex++)
        sbZeroes.Append("0");
        
      txtNumber.Text = sbZeroes.ToString();
    }
    
    private void ListBox_Handler()
    {
      StringBuilder sb = new StringBuilder("1");
      int iLimit = lstCurrently_At.SelectedIndex;

      for (int iThreeZeroes = 1; iThreeZeroes <= iLimit; iThreeZeroes++)
        sb.Append("000");

      txtNumber.Text = sb.ToString();
      sb = null;
    }
    
    private void Scale_Change_Handler()
    {
      lstCurrently_At.Items.Clear();
      lstCurrently_At.Items.AddRange(m_clsVerbalize.Large_Numbers(rbLong_Scale.Checked));
      m_clsVerbalize.Generate_Result(txtNumber.Text, rbLong_Scale.Checked);
      txtResult.Text = m_clsVerbalize.Result;
      if (m_clsVerbalize.Result != null)
        Adjust_ListBox();
      else
        btnClear.PerformClick();
    }
    
    private void Random_Main(Random_Class.Random_Enum enumMode)
    {
      Random_Class clsRandom = new Random_Class(rbLong_Scale.Checked);
      
      clsRandom.Compute(enumMode);
      txtNumber.Text = clsRandom.Result;
    }
    
  

#endregion

#region "Constructor"

    public frmLarge_Number_Verbalizer()
    {
      InitializeComponent();
      this.FormClosing += new FormClosingEventHandler(frmLarge_Number_Verbalizer_FormClosing);
      txtNumber.KeyPress += new KeyPressEventHandler(txtNumber_KeyPress);
      txtIllion.KeyUp += new KeyEventHandler(txtIllion_KeyUp);
      lstCurrently_At.Click += new EventHandler(lstCurrently_At_Click);
      rbShort_Scale.Click += new EventHandler(rbShort_Scale_Click);
      rbLong_Scale.Click += new EventHandler(rbLong_Scale_Click);
    }

#endregion

#region "Control Events"

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if(m_clsHumor.Humor == true)
        m_clsHumor.About_Form();
      else
      {
        About frmAbout = new About();
        frmAbout.ShowDialog();
      }
    }

    void txtIllion_KeyUp(object sender, KeyEventArgs e)
    {
      object oIllion;

      txtIllion.Text = txtIllion.Text.ToLower();
      oIllion = (from object oValue in lstCurrently_At.Items where oValue.ToString().StartsWith(txtIllion.Text) == true select oValue).FirstOrDefault();
      if(oIllion != null)
      {
        txtIllion.BackColor = Color.White;
        lstCurrently_At.Text = oIllion.ToString();
        ListBox_Handler();
      }
      else
        txtIllion.BackColor = Color.Red;
    }

    void lstCurrently_At_Click(object sender, EventArgs e)
    {
      ListBox_Handler();
    }

    void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
    {
      char ch = e.KeyChar;
      bool bBS = (ch == (char) 8);
      bool bETX = (ch == (char) 3);
      bool bSYN = (ch == (char) 22);
      
      if (
           ((rbShort_Scale.Checked == true) && (txtNumber.Text.Length == 3004)) ||
           (!bBS && !bETX && !bSYN) &&
           ((ch < '0') || (ch > '9'))
         )
         e.KeyChar = (char) 0;
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "";
      txtResult.Text = "";
      txtIllion.Text = "";
      txtIllion.BackColor = Color.White;
    }

    private void frmLarge_Number_Verbalizer_Load(object sender, EventArgs e)
    {
      m_clsHumor.Set_Humor_Menu_Items(ref humoronToolStripMenuItem, ref humoroffToolStripMenuItem);
      
      if (m_clsHumor.Humor == true)
      {
        // this code would not execute properly from the Humor class
        m_clsHumor.Instantiate_Nuke_Form();
        m_iForm_Left = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (this.Width / 2);
        tmrIntro.Enabled = true;
        m_clsHumor.Play_Sound("CARSKID.wav", false);
        this.Left = -this.Width;
      }
      else
        tmrIntro.Enabled = false;

      lstCurrently_At.Items.AddRange(m_clsVerbalize.Large_Numbers(rbLong_Scale.Checked));
    }

    private void tmrIntro_Tick(object sender, EventArgs e)
    {
      if (this.Left < m_iForm_Left)
        this.Left += 20;
      else
      {
        tmrIntro.Enabled = false;
        m_clsHumor.Launch_Nuke_Form();
        this.Left = m_iForm_Left;
      }
    }

    private void txtNumber_TextChanged(object sender, EventArgs e)
    {
      Adjust_ListBox();
      Adjust_Base();
      Adjust_Exponent();
      m_clsVerbalize.Generate_Result(txtNumber.Text, rbLong_Scale.Checked);
      txtResult.Text = m_clsVerbalize.Result;      
    }
    
    private void btnCopy_Result_to_Clipboard_Click(object sender, EventArgs e)
    {
      if ((txtResult.Text != null) && (txtResult.Text.Length > 0))
        Clipboard.SetText(txtResult.Text);
    }

    void rbShort_Scale_Click(object sender, EventArgs e)
    {
      Scale_Change_Handler();    
    }

    void rbLong_Scale_Click(object sender, EventArgs e)
    {
      Scale_Change_Handler();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    void frmLarge_Number_Verbalizer_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (m_clsHumor.Humor == true)
      {
        m_clsHumor.Close_Flush(this);
      }
    }

    private void humoronToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_clsHumor.Humor = true;
      m_clsHumor.Set_Humor_Menu_Items(ref humoronToolStripMenuItem, ref humoroffToolStripMenuItem);
    }

    private void humoroffToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_clsHumor.Humor = false;
      m_clsHumor.Set_Humor_Menu_Items(ref humoronToolStripMenuItem, ref humoroffToolStripMenuItem);
    }

    
#endregion

    private void powerOf10ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Power_of_10_Class clsPower10 = new Power_of_10_Class();
      txtNumber.Text = clsPower10.Get_Power_of_10(rbLong_Scale.Checked);
    }


#region "Constants"

    private void lcillionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(50);
    }

    private void avogadroToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "602214150000000000000000";
    }

    private void googolToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(100);
    }

    private void eleventyplexToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(110);
    }

    private void gargoogolToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(200);
    }

    private void mentaggoogolToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(210);
    }

    private void bentaggoogolToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(420);
    }

    private void tentaggoogolToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(630);
    }

    private void smallest6perfectNumberToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "154345556085770649600";
    }

    private void sumOf10thPowersFrom11000ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "1409924241424243424241924242500";
    }

    private void largestArmstrongNumberbase10ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "115132219018763992565095597973971522401";
    }

    private void catalansToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "170141183460469231731687303715884105727";
    }

    private void ferriersToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "20988936657440586486151264256610222593863921";
    }

    private void numberOfIPv6AddressesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "340282366920938463463374607431768211456";
    }

    private void zàiToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(44);
    }

    private void largestPerfectSquareWithAllPerfectSquareDigitsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "419994999149149944149149944191494441";
    }

    private void asankhyeyaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(140);
    }

    private void megafugafourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(1530);
    }

    private void googoodToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = m_clsVerbalize.Generate_Special_Number(3000);
    }

    private void Rubicks3x3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "101097362223624462291180422369532000000";
    }

    private void Rubicks4x4ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "7401196841564901869874093974498574336000000000";
    }

    private void Rubicks6x6ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "157152858401024063281013959519483771508510790313968742344694684829502629887168573442107637760000000000000000000000000";
    }

    private void Rubicks7x7ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "19500551183731307835329126754019748794904992692043434567152132912323232706135469180065278712755853360682328551719137311299993600000000000000000000000000000000000";
    }

    private void Rubicks5x5ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "282870942277741856536180333107150328293127731985672134721536000000000000000";
    }

    private void largestPrimeEverFoundByECMFactorizationToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "4444349792156709907895752551798631908946180608768737946280238078881";
    }

    private void eddingtonNumberToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "31495448272550005155211307922363110936089435829054233418732462850152371262062592";
    }

    private void SudokuGrids9x9ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      txtNumber.Text = "6670903752021072936960";
    }
#endregion

#region "Light"

    private void Light_Speed_Imperial_Miles_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILES_YEAR);
    }

    private void Light_Speed_Imperial_Miles_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILES_DAY);
    }

    private void Light_Speed_Imperial_Miles_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILES_HOUR);
    }

    private void Light_Speed_Imperial_Miles_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILES_MINUTE);
    }

    private void Light_Speed_Imperial_Miles_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILES_SECOND);
    }

    private void Light_Speed_Imperial_Feet_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.FEET_YEAR);
    }

    private void Light_Speed_Imperial_Feet_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.FEET_DAY);
    }

    private void Light_Speed_Imperial_Feet_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.FEET_HOUR);
    }

    private void Light_Speed_Imperial_Feet_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.FEET_MINUTE);
    }

    private void Light_Speed_Imperial_Feet_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.FEET_SECOND);
    }

    private void Light_Speed_Imperial_Inches_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.INCHES_YEAR);
    }

    private void Light_Speed_Imperial_Inches_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.INCHES_DAY);
    }

    private void Light_Speed_Imperial_Inches_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.INCHES_HOUR);
    }

    private void Light_Speed_Imperial_Inches_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.INCHES_MINUTE);
    }

    private void Light_Speed_Imperial_Inches_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.INCHES_SECOND);
    }

    private void Light_Speed_Metric_Kilometers_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.KILOMETERS_YEAR);
    }

    private void Light_Speed_Metric_Kilometers_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.KILOMETERS_DAY);
    }

    private void Light_Speed_Metric_Kilometers_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.KILOMETERS_HOUR);
    }

    private void Light_Speed_Metric_Kilometers_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.KILOMETERS_MINUTE);
    }

    private void Light_Speed_Metric_Kilometers_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.KILOMETERS_SECOND);
    }

    private void Light_Speed_Metric_Meters_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.METERS_YEAR);
    }

    private void Light_Speed_Metric_Meters_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.METERS_DAY);
    }

    private void Light_Speed_Metric_Meters_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.METERS_HOUR);
    }

    private void Light_Speed_Metric_Meters_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.METERS_MINUTE);
    }

    private void Light_Speed_Metric_Meters_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.METERS_SECOND);
    }

    private void Light_Speed_Metric_Centimeters_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.CENTIMETERS_YEAR);
    }

    private void Light_Speed_Metric_Centimeters_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.CENTIMETERS_DAY);
    }

    private void Light_Speed_Metric_Centimeters_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.CENTIMETERS_HOUR);
    }

    private void Light_Speed_Metric_Centimeters_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.CENTIMETERS_MINUTE);
    }

    private void Light_Speed_Metric_Centimeters_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.CENTIMETERS_SECOND);
    }

    private void Light_Year_Metric_millimeters_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILLIMETERS_YEAR);
    }

    private void Light_Year_Metric_millimeters_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILLIMETERS_DAY);
    }

    private void Light_Year_Metric_millimeters_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILLIMETERS_HOUR);
    }

    private void Light_Year_Metric_millimeters_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILLIMETERS_MINUTE);
    }

    private void Light_Year_Metric_millimeters_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MILLIMETERS_SECOND);
    }

    private void Light_Speed_metric_micrometers_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MICROMETERS_YEAR);
    }

    private void Light_Speed_metric_micrometers_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MICROMETERS_DAY);
    }

    private void Light_Speed_metric_micrometers_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MICROMETERS_HOUR);
    }

    private void Light_Speed_metric_micrometers_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MICROMETERS_MINUTE);
    }

    private void Light_Speed_metric_micrometers_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.MICROMETERS_SECOND);
    }
    
    private void Light_Speed_metric_nanometers_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.NANOMETERS_YEAR);
    }

    private void Light_Speed_metric_nanometers_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.NANOMETERS_DAY);
    }

    private void Light_Speed_metric_nanometers_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.NANOMETERS_HOUR);
    }

    private void Light_Speed_metric_nanometers_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.NANOMETERS_MINUTE);
    }

    private void Light_Speed_metric_nanometers_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.NANOMETERS_SECOND);
    }

    private void Light_Speed_Metric_Angstroms_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.ANGSTROMS_YEAR);
    }

    private void Light_Speed_Metric_Angstroms_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.ANGSTROMS_DAY);
    }

    private void Light_Speed_Metric_Angstroms_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.ANGSTROMS_HOUR);
    }

    private void Light_Speed_Metric_Angstroms_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.ANGSTROMS_MINUTE);
    }

    private void Light_Speed_Metric_Angstroms_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.ANGSTROMS_SECOND);
    }

    private void Light_Speed_Metric_picometers_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_YEAR);
    }

    private void Light_Speed_Metric_picometers_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_DAY);
    }

    private void Light_Speed_Metric_picometers_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_HOUR);
    }

    private void Light_Speed_Metric_picometers_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_MINUTE);
    }

    private void Light_Speed_Metric_picometers_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_SECOND);
    }

    private void Light_Speed_Metric_femtometers_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_YEAR, 3);
    }

    private void Light_Speed_Metric_femtometers_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_DAY, 3);
    }

    private void Light_Speed_Metric_femtometers_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_HOUR, 3);
    }

    private void Light_Speed_Metric_femtometers_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_MINUTE, 3);
    }

    private void Light_Speed_Metric_femtometers_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_SECOND, 3);
    }

    private void Light_Speed_Metric_attometers_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_YEAR, 6);
    }

    private void Light_Speed_Metric_attometers_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_DAY, 6);
    }

    private void Light_Speed_Metric_attometers_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_HOUR, 6);
    }

    private void Light_Speed_Metric_attometers_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_MINUTE, 6);
    }

    private void Light_Speed_Metric_attometers_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_SECOND, 6);
    }

    private void Light_Speed_Metric_zeptometers_yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_YEAR, 9);
    }

    private void Light_Speed_Metric_zeptometers_dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_DAY, 9);
    }

    private void Light_Speed_Metric_zeptometers_hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_HOUR, 9);
    }

    private void Light_Speed_Metric_zeptometers_minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_MINUTE, 9);
    }

    private void Light_Speed_Metric_zeptometers_secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_SECOND, 9);
    }

    private void yearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_YEAR, 12);
    }

    private void dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_DAY, 12);
    }

    private void hourToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_HOUR, 12);
    }

    private void minuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_MINUTE, 12);
    }

    private void secondToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Set_Light(Light_Measurements_Class.PICOMETERS_SECOND, 12);
    }

#endregion

#region "Random"


    private void setNumberOfDigitsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Random_Main(Random_Class.Random_Enum.SET_LENGTH_MODE);
    }

    private void maximumNumberOfDigitsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Random_Main(Random_Class.Random_Enum.MAXIMUM_LENGTH_MODE);
    }

    private void totallyRandomToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Random_Main(Random_Class.Random_Enum.ANY_LENGTH_MODE);
    }



#endregion
    private void readMeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ReadMe frmReadMe = new ReadMe();
      
      frmReadMe.ShowDialog();
      frmReadMe.Dispose();
    }
  }
}
