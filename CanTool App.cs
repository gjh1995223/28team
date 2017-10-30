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
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;


namespace WindowsFormsApplication1
{
    public partial class CanTool_App : Form
    {
        public static string source;
        public static string IDstr;
        public static string DLCstr;
        public static string[] data;
        public static string[,] storedata; // 二维存储数组（8*8）
        //public string messageID = "";
        //public string messageDLC = "";
        public string messageName;
        public string signalx;
        public string startarray;
        public string endarray;
        public static string signalName;
        public string signalA;
        public string signalB;
        public string signalSaveMode;//数据存储方式msb or lsb.
        public string signalC;
        public string signalD;
        public string signalUnit;//物理单位
        public static string phy;   //物理值
        public int signalnum;
        public ArrayList phys;
        public ArrayList signalnames;
        public TreeNode tn;
        public static string treevalue;


        public MySqlConnection Connectdatabase()
        {
            MySqlConnection mconn = null;
            //mconn = new MySqlConnection("server=172.26.39.8;user id=root;Password=123;database=cantool");//创建远程对象
            mconn = new MySqlConnection("server=172.26.52.151;user id=root;Password=123;database=cantool");//创建本地对象
            try
            {
                mconn.Open();
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return mconn;
        }
        public CanTool_App()
        {
            InitializeComponent();
        }
        private void button_begin_Click(object sender, EventArgs e)
        {
            if (Form1.portname == null)
            {
                MessageBox.Show("请先进入Setting->COM setting设置串口设置", "提示");

            }
            else
            {
                sp.PortName = Form1.portname;
                sp.BaudRate = Convert.ToInt32(Form1.baudrate);
                sp.Parity = Parity.None;
                sp.DataBits = Convert.ToInt32(Form1.datarate);
                sp.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);

                try
                {
                    if (!sp.IsOpen)
                    {
                        sp.Open();
                    }
                    label5.Text = "串口" + Form1.portname + "已打开！";
                    button_stop.Enabled = true;
                    button_begin.Enabled = false;
                }
                catch
                {
                    MessageBox.Show("无法打开串口：" + Form1.portname + "!");
                }
            }
        }
        public int IDmatch(string oriid)
        {
            int a = Convert.ToInt32(oriid, 16);
            string sql = "select messageid from can_message where messagetext like" + "'BO_ " + a.ToString() + "%'";
            int msgid = 0;
            try
            {
                MySqlCommand mcmd = new MySqlCommand();
                mcmd.Connection = Connectdatabase();
                mcmd.CommandText = sql;
                msgid = Convert.ToInt32(mcmd.ExecuteScalar());
                //comText.CommandTimeout = 2;//设置等待时间  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return msgid;
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            button_stop.Enabled = false;
            button_begin.Enabled = true;
            sp.Close();
            label5.Text = Form1.portname + "串口已关闭";
        }
        private void cOMSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sp.Close();
            this.Close();
        }
        private void button_show1_Click(object sender, EventArgs e)
        {

        }
        private void button_savefile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "请选择要保存的文件路径";
            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "文本文件|*.txt|音频文件|*.wav|图片文件|*.jpg|所有文件|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filePath = sfd.FileName;
                using (FileStream fsWrite = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] buffer = Encoding.Default.GetBytes(textBox1.Text.ToString().Trim());
                    fsWrite.Write(buffer, 0, buffer.Length);
                }
            }
        }
        private void button_send_Click(object sender, EventArgs e)    //发送信息
        {
            if (Form1.portname == null)
            {
                MessageBox.Show("请先进入Setting->COM setting设置串口设置", "提示");
            }
            else
            {
                bool a = true, b = true, c = true;
                string strid = textBox_CANID.Text.Trim();   //ID 的合法检测
                if (strid == "")
                {
                    iderror.Text = "请输入ID值";
                    a = false;
                }
                if (a == true && IDmatch(strid) == 0)
                {
                    iderror.Text = "输入的ID没有匹配的message";
                    a = false;
                }
                string strdlc = textBox_DLC.Text.Trim();     //DLC 的合法检测
                if (strdlc == "")
                {
                    dlcerror.Text = "请输入DLC的值";
                    b = false;
                }
                if (b == true && Convert.ToInt16(strdlc) < 0 && Convert.ToInt16(strdlc) > 8)
                {
                    dlcerror.Text = "输入的DLC应在0-8之前取值";
                    b = false;
                }
                string strdata = textBox_DATA.Text.Trim();    //DATA 的合法检测
                if (strdata == "")
                {
                    dlcerror.Text = "请输入DATA的值";
                    c = false;
                }
                else
                {
                    if (strdata.Length != (Convert.ToInt16(strdlc) * 2) && c == true)
                    {
                        dataerror.Text = "输入的DATA长度应为DLC*2";
                    }
                    else
                    {
                        char[] chardata = strdata.ToCharArray();
                        for (int i = 0; i < chardata.Length; i++)
                        {
                            if (chardata[i] < 48 || chardata[i] > 70)
                            {
                                dataerror.Text = "请输入正确的16进制";
                                c = false;
                            }
                            if (chardata[i] > 57 && chardata[i] < 65)
                            {
                                dataerror.Text = "请输入正确的16进制";
                                c = false;
                            }
                        }

                    }
                }

                if (a && b && c)
                {
                    DateTime t = DateTime.Now;
                    string finalsend = "t" + strid + strdlc + strdata;
                    if (!sp.IsOpen)
                    {
                        sp.Open();
                    }
                    sp.WriteLine(finalsend);
                    sp.Close();
                    listBox1.Items.Add("发送成功 " + System.DateTime.Now.ToString("hh:mm:ss") + "\n");
                    iderror.Text = "";
                    dlcerror.Text = "";
                    dataerror.Text = "";
                    //MessageBox.Show("数据发送成功！", "系统提示");
                }
                else
                {
                    listBox1.Items.Add("发送失败 " + System.DateTime.Now.ToString("hh:mm:ss") + "\n");
                }
            }
            //sp.Close();

        }
        private void CanTool_App_Load(object sender, EventArgs e)  //页面加载
        {

        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)   //Serialport接收信息
        {
            //sp.NewLine = "\\" + "r";
            if (!sp.IsOpen)
            {
                sp.Open();
                sp.DiscardInBuffer();
            }
            int length = sp.BytesToRead;
            if (length == 13 || length == 6 || length == 4)
            {
                try
                {
                    char[] zl = new char[length];
                    sp.Read(zl, 0, length);
                    string text = "";
                    for (int i = 0; i < length; i++)
                    {
                        text += zl[i];
                    }
                    this.Invoke(new MethodInvoker(delegate { listBox1.Items.Add("接收反馈： " + text); }));
                }
                catch (IOException)
                {
                    this.Invoke(new MethodInvoker(delegate { listBox1.Items.Add("接收指令反馈失败 " + System.DateTime.Now.ToString("hh:mm:ss") + "\n"); }));
                }
            }
            else
            {
                try
                {
                    string temp = sp.ReadTo("\\" + "r");
                    char[] k = temp.ToCharArray();
                    int begin = temp.IndexOf("t");
                    source = temp.Substring(begin);
                    if (this.InvokeRequired)
                    {
                        datahandle(source);
                        int matchid = IDmatch(IDstr);
                        tn = new TreeNode(source);
                        this.Invoke(new MethodInvoker(delegate { this.treeView1.Nodes.Add(tn); }));
                        xianshiphy(matchid.ToString());
                    }
                    else
                    {
                        this.treeView1.Nodes.Add(source);
                    }
                }
                catch (IOException)
                {
                    sp.Close();
                    this.Invoke(new MethodInvoker(delegate { button_begin.Enabled = true; }));
                    this.Invoke(new MethodInvoker(delegate { button_stop.Enabled = false; }));
                }
            }
        }
        public void datahandle(string shuru)   //数据切分
        {
            //IDstr = shuru.IndexOf("t") ;
            IDstr = shuru.Substring(1, 3);
            DLCstr = shuru.Substring(4, 1);
            int DLC = Convert.ToInt32(DLCstr);
            data = new string[DLC];
            string[] data2 = new string[DLC];
            string DATAstr = shuru.Substring(5);
            storedata = new string[8, 8];
            for (int i = 0; i < 8; i++)  //存储数组初始化
            {
                for (int j = 0; j < 8; j++)
                {
                    storedata[i, j] = "0";
                }
            }
            for (int i = 0; i < DLC; i++)
            {
                data[i] = DATAstr.Substring(2 * i, 2);//每次截两位算，两位一换行
                data2[i] = HexString2BinString(data[i]);
                for (int j = 0; j < 8; j++)
                {
                    storedata[i, j] = data2[i].Substring(j, 1);
                }
            }
        }

        public double huoquphy(string signalx)            //计算物理值
        {
            //  byte x1 = Convert.ToByte(signalx);
            double phy = 0;
            int x = Convert.ToInt32(signalx, 2);
            phy = Convert.ToInt32(signalA) * x + Convert.ToInt32(signalB);
            return phy;
        }
        static string HexString2BinString(string hexString)  //将16进制字符串转化成2进制字符串
        {
            List<string> list = new List<string>();
            string result = string.Empty;
            int count = 0;
            foreach (char c in hexString)
            {
                int v = Convert.ToInt32(c.ToString(), 16);
                int v2 = int.Parse(Convert.ToString(v, 2));
                result += string.Format("{0:d4}", v2);
                count++;
                if ((count % 4) == 0)
                {
                    list.Add(result);
                    result = string.Empty;
                }
            }
            // return list.ToArray();
            return result;
        }
        public string huoquSignalname(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf("_ ") + 1;
            int j = signal.IndexOf(" : ");
            signalName = signal.Substring(i + 1, j - i - 1);
            return signalName;
        }
        public string huoqusignalA(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf("(") + 1;
            int j = signal.IndexOf(",");
            signalA = signal.Substring(i, j - i);
            return signalA;
        }
        public string huoqusignalB(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf(",") + 1;
            int j = signal.IndexOf(")");
            signalB = signal.Substring(i, j - i);
            return signalB;
        }
        public string huoqusignalSaveMode(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf("@") + 1;
            //int j = signal.IndexOf("+");
            signalSaveMode = signal.Substring(i, 1);
            return signalSaveMode;
        }
        public string huoqusignalC(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf("[");
            int j = signal.IndexOf("]");
            string itoj = signal.Substring(i, j - i + 1);
            int m = itoj.IndexOf("|");
            int n = itoj.IndexOf("[");
            signalC = itoj.Substring(n + 1, m - n - 1);
            return signalC;
        }
        public string huoqusignalD(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf("[");
            int j = signal.IndexOf("]");
            string itoj = signal.Substring(i, j - i + 1);
            int m = itoj.IndexOf("|");
            int n = itoj.IndexOf("]");
            signalD = itoj.Substring(m + 1, n - m - 1);
            return signalD;
        }
        public string huoqusignalUnit(string signaltext)  //获取物理单位
        {
            string signal = signaltext;
            int i = signal.IndexOf("]");
            int j = signal.IndexOf("  ");
            string itoj = signal.Substring(i + 1, j - i + 1);
            int m = itoj.IndexOf(" \"") + 1;
            int n = itoj.IndexOf("  ");
            signalUnit = itoj.Substring(m + 1, n - m - 2);
            return signalUnit;
        }
        /*public string huoqumessageID(string messagetext)
        {
            string signal = messagetext;
            int i = signal.IndexOf("_ ") + 1;
            // int j = signal.IndexOf("  ");
            string itoj = signal.Substring(i + 1);
            int m = itoj.IndexOf(" ");
            // int n = itoj.IndexOf("  ");
            messageID = itoj.Substring(0, m);
            return messageID;
        } */
        /*public string huoqumessageDLC(string messagetext)
        {
            string signal = messagetext;
            int i = signal.IndexOf(":") + 1;
            string itoj = signal.Substring(i + 1);
            messageDLC = itoj.Substring(0, 1);
            return messageDLC;
        }*/
        public string huoqumessageName(string messagetext)
        {
            string signal = messagetext;
            int i = signal.IndexOf("_ ") + 1;
            // int j = signal.IndexOf("  ");
            string itoj = signal.Substring(i + 1);
            int m = itoj.IndexOf(" ");
            int n = itoj.IndexOf(":");
            messageName = itoj.Substring(m + 1, n - m - 1);
            return messageName;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            treevalue = treeView1.SelectedNode.Text;
            instrument ins = new instrument();
            ins.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Connectdatabase();
        }
        public void xianshiphy(string messageid)
        {
            MySqlConnection mconn = null; //声明连接对象                       
            //mconn = new MySqlConnection("server=172.26.214.228;user id=root;Password=123;database=cantool");
            mconn = new MySqlConnection("server=server=172.26.52.151;user id=root;Password=123;database=cantool");
            MySqlCommand mcmd = new MySqlCommand();//创建MySqlCommand对象
            mcmd.Connection = mconn;
            mcmd.CommandText = "SELECT SIGNALTEXT FROM can_signal WHERE messageid=" + messageid;
            try
            {

                mconn.Open();//数据库打开连接
                MySqlDataAdapter ada = new MySqlDataAdapter(mcmd);
                DataSet ds = new DataSet();
                ada.Fill(ds);
                int signalnum = ds.Tables[0].Rows.Count;
                string[] signaltextstr = new string[signalnum];
                int[] signalstartarray = new int[signalnum];
                int[] signalendarray = new int[signalnum];
                string[] x = new string[signalnum];
                for (int xi = 0; xi < signalnum; ++xi)
                { x[xi] = ""; }
                for (int i = 0; i < signalnum; ++i)
                {

                    signaltextstr[i] = ds.Tables[0].Rows[i][0].ToString();
                    signalstartarray[i] = Convert.ToInt32(huoqustartarray(signaltextstr[i]));
                    // signalendarray[i] = Convert.ToInt32(huoquendarray(signaltextstr[i]));
                    int locatex = signalstartarray[i] / 8;
                    int locatey = signalstartarray[i] % 8;
                    int shijiweizhiy = 7 - locatey;
                    int length = Convert.ToInt32(huoquendarray(signaltextstr[i]));
                    //x[i] = storedata[locatex, locatey];
                    x[i] = storedata[locatex, shijiweizhiy];
                    for (int j = 1; j < length; ++j)
                    {
                        if (locatex >= 0 && locatex < 8 && shijiweizhiy < 7)
                        {

                            shijiweizhiy++;
                            x[i] += storedata[locatex, shijiweizhiy];
                        }

                        else if (locatex >= 0 && locatex < 8 && shijiweizhiy >= 7)
                        {
                            locatex++;
                            locatey = 7;
                            shijiweizhiy = 7 - locatey;
                            x[i] += storedata[locatex, shijiweizhiy];
                        }

                    }
                    signalA = huoqusignalA(signaltextstr[i]);
                    signalB = huoqusignalB(signaltextstr[i]);
                    signalName = huoquSignalname(signaltextstr[i]);
                    phy = huoquphy(x[i]).ToString();
                    string context = "signalname : " + signalName + "   physical value -" + phy;
                    this.Invoke(new MethodInvoker(delegate { tn.Nodes.Add(context); }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { mconn.Close(); }
        }

        public string huoqustartarray(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf(":") + 1;
            int j = signal.IndexOf("|");
            startarray = signal.Substring(i + 1, j - i - 1);//从第几位开始放数据
            return startarray;

        }
        public string huoquendarray(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf("|");
            int j = signal.IndexOf("+") - 2;
            endarray = signal.Substring(i + 1, j - i - 1);//数据一共放几位
            return endarray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.portname == null)
            {
                MessageBox.Show("请先进入Setting->COM setting设置串口设置", "提示");
            }
            else
            {
                DateTime t = DateTime.Now;
                string zhilingsend = textBox2.Text.Trim();
                if (zhilingsend == "V\\r" || zhilingsend == "O1\\r" || zhilingsend == "Sn\\r" || zhilingsend == "C\\r")
                {
                    if (!sp.IsOpen)
                    {
                        sp.Open();
                    }
                    if (zhilingsend == "V\\r") { zhilingsend = "V\r"; }
                    if (zhilingsend == "O1\\r") { zhilingsend = "O1\r"; }
                    if (zhilingsend == "Sn\\r") { zhilingsend = "Sn\r"; }
                    if (zhilingsend == "C\\r") { zhilingsend = "C\r"; }
                    sp.WriteLine(zhilingsend);
                    this.Invoke(new MethodInvoker(delegate { listBox1.Items.Add("指令" + zhilingsend + "已发送"); }));
                }
                else
                {
                    listBox1.Items.Add("发送失败，无效的指令 " + System.DateTime.Now.ToString("hh:mm:ss") + "\n");
                }
            }
        }
    }
}
