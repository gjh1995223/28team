using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class 数据库测试 : Form
    {
        public 数据库测试()
        {
            InitializeComponent();
        }
        //想调用method.cs内的方法。
        //public method m1;

        //模拟字符串16进制
        public static string shuru = "t3588CD3CFC7F1818F000";//匹配到can_message的第一行数据的模拟数据 BO_ 856 CDU_1: 8 CDU

        public static string source;
        public static string IDstr;
        public static string DLCstr;
        public static string[] data;
        public static string[,] storedata = new string[8, 8];//8*8存储数据

        public string messageID = "";
        public string messageDLC = "";
        public string messageName = "";

        public string signalx = "";
        public string startarray = "";
        public string endarray = "";

        public string signalName = "";
        public string signalA = "";
        public string signalB = "";

        public string signalSaveMode = "";//数据存储方式msb or lsb.
        public string signalC = "";
        public string signalD = "";
        public string signalUnit = "";//物理单位

       
        public int signalnum ;
        public double phy ;
        //private string signalNodeName="";



        public double huoquphy(string signalx)
        {
          //  byte x1 = Convert.ToByte(signalx);
           double phy=0;
           int x = Convert.ToInt32(signalx,2);
           phy =Convert.ToInt32( signalA) * x + Convert.ToInt32(signalB);
           return phy;
        }
        public double xianshiphy(string messageid) 
        {
            MySqlConnection mconn = null; //声明连接对象                       
            mconn = new MySqlConnection("server=127.0.0.1;user id=admin;Password=123456;database=cantool");
            MySqlCommand mcmd = new MySqlCommand();//创建MySqlCommand对象
            mcmd.Connection = mconn;
            mcmd.CommandText = "SELECT SIGNALTEXT FROM can_signal WHERE messageid="+messageid;
            try
            {

                mconn.Open();//数据库打开连接
                MySqlDataAdapter ada = new MySqlDataAdapter(mcmd);
                DataSet ds = new DataSet();
                ada.Fill(ds);
                signalnum = ds.Tables[0].Rows.Count;
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
                    x[i] += storedata[locatex, shijiweizhiy];
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
                    phy = huoquphy(x[i]);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { mconn.Close(); }

                return phy;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // signalUnit
            textBox2.Text = huoquendarray(textBox1.Text.Trim());
            //huoqustartarray(textBox1.Text.Trim());

        }





        private void button1_Click(object sender, EventArgs e)
        {
           // int count = 0 ;
            for (int o = 0; o < 8; o++)
            {
                for (int p = 0; p < 8; p++)
                {
                   // ++count;
                    storedata[o, p] = "1";
                }
            }

            MySqlConnection mconn = null; //声明连接对象

           // mconn = new MySqlConnection("server=172.26.39.8;user id=root;Password=123;database=cantool");//创建对象
            mconn = new MySqlConnection("server=127.0.0.1;user id=admin;Password=123456;database=cantool");
            MySqlCommand mcmd = new MySqlCommand();//创建MySqlCommand对象

            mcmd.Connection = mconn;

            //  mcmd.CommandText = "SELECT * FROM can_message;";//查询命令
            // mcmd.CommandText = "SELECT signaltext  FROM can_signal WHERE messageid=1";//signal_message
            mcmd.CommandText = "SELECT SIGNALTEXT FROM can_signal WHERE messageid=13";
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
                     int  shijiweizhiy = 7 - locatey;
                    int length = Convert.ToInt32(huoquendarray(signaltextstr[i]));
                    //x[i] = storedata[locatex, locatey];
                    x[i] += storedata[locatex, shijiweizhiy];
                    for (int j = 1; j < length; ++j)
                    {
                        if (locatex >= 0 && locatex < 8 && shijiweizhiy  < 7)
                        {
                          
                            shijiweizhiy++;
                            x[i] += storedata[locatex, shijiweizhiy];
                        }

                        else if (locatex >= 0 && locatex < 8 &&  shijiweizhiy >= 7)
                        {
                            locatex++;
                            locatey = 7;
                            shijiweizhiy = 7 - locatey;
                            x[i] += storedata[locatex, shijiweizhiy];
                        }
                    
                    }
                    signalA = huoqusignalA(signaltextstr[i]);
                    signalB = huoqusignalB(signaltextstr[i]);
                    phy = huoquphy(x[i]);

                    textBox1.Text += x[i].ToString()+"\r\n\r"+i.ToString()+"!!!\r\n";
                    textBox2.Text += phy+"\r\n";

                }
                dataGridView1.DataSource = ds.Tables[0];

            }

            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            finally
            {
                mconn.Close();//关闭数据库连接
            }
        }
        /// <summary>   
        /// 16进制字符串转为16进制字符数组   
        /// </summary>   
        /// <param name="newString"></param>   
        /// <returns></returns>   
        //public static byte[] Hex2ByteArr(string newString)
        //{
        //    int len = newString.Length / 2;
        //    byte[] arr = new byte[len];
        //    for (int i = 0; i < len; i++)
        //    {
        //        arr[i] = Convert.ToByte(newString.Substring(i * 2, 2), 16);
        //    }
        //    return arr;
        //}



        //分离接收的数据，将其存到输入中
        private void button2_Click(object sender, EventArgs e)
        {
            string IDdtr = shuru.Substring(1, 3);//截取id，给高金贺做！
            string DLCstr = shuru.Substring(4, 1);  //截取dlc
            int DLC = Convert.ToInt32(DLCstr);//格式转换

            //截取datastr
            data = new string[DLC];//定义data
            string[] data2 = new string[DLC];
            string DATAstr = shuru.Substring(5);
            string[,] storedata = new string[8, 8];
            // string[,] chulidata2 = new string[DLC, 8];//定义一个二维字符数组，将data数据存储入。
            for (int i = 0; i < DLC; i++)
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


            //for (int j = 0; j < 8; ++j)
            //{
            //    //每两位算一次
            //    string[] data2 = new string[DLC];
            //    for (int i = 0; i < DLC; ++i)
            //    {
            //        int m = 5 + 2 * i;
            //        data[i] = shuru.Substring(m, 2);//每次截两位算，两位一换行

            //        data2[i] = HexString2BinString(data[i]);
            //        chulidata2[j, i] = data2[i];
            //    }

            //    textBox1.Text += "\r\n" + "***" + data2[j] + "@@" + j + "\r\n";
            //    for (int p = 0; p < 8; ++p)
            //        textBox1.Text += "\r\n" + chulidata2[j, p];

            //}
        }

        //将十六进制字符串转化为2进制字符串
        static string HexString2BinString(string hexString)
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
        public string huoqusignalUnit(string signaltext)
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
        public string huoqumessageID(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf("_ ") + 1;
            // int j = signal.IndexOf("  ");
            string itoj = signal.Substring(i + 1);
            int m = itoj.IndexOf(" ");
            // int n = itoj.IndexOf("  ");
            messageID = itoj.Substring(0, m);
            return messageID;
        }
        public string huoqumessageDLC(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf(":") + 1;
            string itoj = signal.Substring(i + 1);
            messageDLC = itoj.Substring(0, 1);
            return messageDLC;
        }
        public string huoqumessageName(string signaltext)
        {
            string signal = signaltext;
            int i = signal.IndexOf("_ ") + 1;
            // int j = signal.IndexOf("  ");
            string itoj = signal.Substring(i + 1);
            int m = itoj.IndexOf(" ");
            int n = itoj.IndexOf(":");
            messageName = itoj.Substring(m + 1, n - m - 1);
            return messageName;
        }

        public void datahandle(string shuru)   //数据切分
        {
            IDstr = shuru.Substring(1, 3);
            DLCstr = shuru.Substring(4, 1);
            int DLC = Convert.ToInt32(DLCstr);
            data = new string[DLC];
            string[] data2 = new string[DLC];
            string DATAstr = shuru.Substring(5);
            string[,] storedata = new string[8, 8];
            for (int i = 0; i < 8; i++)  //存储数组初始化  /0 t1234
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

        private void 数据库测试_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = xianshiphy("13").ToString();
        }






    }
}




