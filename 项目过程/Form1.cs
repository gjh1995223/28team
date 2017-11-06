using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static String portname;
        public static String baudrate;
        public static String datarate;
        public static String stopdata;
        public string inipath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath); 



        public void IniWriteValue(string Section,string Key,string Value,string path) //设置INI文件参数
        { 
            WritePrivateProfileString(Section,Key,Value,path); 
        } 

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            bool s = false;
            string[] PortNames = SerialPort.GetPortNames();    //获取本机串口名称，存入PortNames数组中
            if (PortNames.Count() == 0)
            {
                label_error.Text = "没有可用串口！";
                comboBox1.Text = "none COM port";
                comboBox1.Enabled = false;
            }
            string temp = IniReadValue("Section1", "COM port");
            for (int i = 0; i < PortNames.Count(); i++)
            {
                comboBox1.Items.Add(PortNames[i]);
                if(PortNames[i].ToString()==temp){
                    s = true;
                }
            }
            comboBox2.SelectedItem = IniReadValue("Section1", "Baud Rate");
            comboBox5.SelectedItem = IniReadValue("Section1", "Data Bits");
            comboBox6.SelectedItem = IniReadValue("Section1", "Stop Bits");
            if (s)
            {
                comboBox1.SelectedItem = IniReadValue("Section1", "COM port");
            }
        }
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(10);
            string path = Application.StartupPath + "\\Config.ini";
            int i = GetPrivateProfileString(Section, Key, "", temp, 10, path);
            return temp.ToString();
        } 



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            portname = comboBox1.Text.Trim();
            baudrate = comboBox2.Text.Trim(); 
            datarate = comboBox5.Text.Trim();
            stopdata = comboBox6.Text.Trim();
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\Config.ini";
                IniWriteValue("Section1", "Baud Rate", comboBox2.Text, path);
                IniWriteValue("Section1", "Data Bits", comboBox5.Text, path);
                IniWriteValue("Section1", "Stop Bits", comboBox6.Text, path);
                IniWriteValue("Section1", "COM port", comboBox1.Text, path);
                MessageBox.Show("设置保存成功！","消息");
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }
    }
}
