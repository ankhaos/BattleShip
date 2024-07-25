using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;

namespace Морской_Бой
{
    public partial class Form2 : Form
    {
        string p;
        public Form2()
        {
            InitializeComponent();
            Data();
        }
        public void Data()
        {
            XmlDocument doc = new XmlDocument();
            DataSet myDS = new DataSet();
            myDS.ReadXml(Environment.CurrentDirectory + @"\data.xml");
            this.crystalReport31 = new CrystalReport3();
            this.crystalReport31.SetDataSource(myDS);
            crystalReportViewer1.ReportSource = crystalReport31;
        }

    }
}
