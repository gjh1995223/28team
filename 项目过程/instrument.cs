using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class instrument : Form
    {
        public instrument()
        {
            InitializeComponent();
        }

        public string signame;
        public string phyvalue;
        private void instrument_Load(object sender, EventArgs e)
        {
            string value = CanTool_App.treevalue;
            int namebegin = value.IndexOf(":");
            string temp = value.Substring(namebegin+1);
            int nameend = value.IndexOf("phy");
            signame = value.Substring(namebegin,nameend-namebegin);
            int phyindex = value.IndexOf("-");
            phyvalue = value.Substring(phyindex);
            double finalphy = Convert.ToInt32(phyvalue) + 30;
            meter1.ChangeValue = 8;
            labsignal.Text = signame;
            labphy.Text = phyvalue;
            textBox1.Text = phyvalue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       


    }
}
