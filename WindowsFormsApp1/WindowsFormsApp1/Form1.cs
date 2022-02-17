using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hybrid_Cryptosystem;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string savePath;
        public string nowTime;
        int count = 0;
        int bpk1, bsk, bpk2;
        int atplength;
        int apk1, apk2;
        string bpk;
        string apk;
        byte[,] tempPhoto;
        AESKeyInput A = new AESKeyInput();
        byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };//对称密钥
        RSA Arsa = new RSA();
        RSA Brsa = new RSA();

        AES Aaes = new AES();
        AES Baes = new AES();
        StringBuilder s = new StringBuilder();
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {

            //bpk 

            Brsa.GetKeys();
            Baes.k = key;
            bpk2 = Brsa.N;
            bpk1 = Brsa.E;
            //b公钥
            bpk = string.Concat("(" + bpk1.ToString() + "," + bpk2.ToString() + ")");
           
        }

        public void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = bpk;
            string a = "B的公私钥生成完成！\n";
            richTextBox1.AppendText(a);
        }

        public void button5_Click(object sender, EventArgs e)
        {


           Baes.k = Brsa.RSADecode(Brsa.RSARun(Aaes.k));
           textBox4.Text=  BitConverter.ToString(Baes.k);
            string a = "B已得到会话密钥！(字节显示)\n";
            richTextBox1.AppendText(a);


        }

        public void button6_Click(object sender, EventArgs e)
        {
            nowTime = DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒");
            //加密
            tempPhoto = PhotoProcess.InputPhoto(textBox5.Text);
           atplength = PhotoProcess.Rlength;
           string a1 = "请稍等。。。\n";
            richTextBox1.AppendText(a1);
           Aaes.AESRun(tempPhoto, atplength,true,savePath,nowTime);
          
           string a = "加密成功\n";
            richTextBox1.AppendText(a);
          

        }

        public void button7_Click(object sender, EventArgs e)
        {
            //解密
            //tempPhoto = PhotoProcess.InputPhoto(textBox5.Text);
            //Aaes.c = tempPhoto;
            string a1 = "请稍等。。。\n";
            richTextBox1.AppendText(a1);
            atplength = PhotoProcess.Rlength;
            Aaes.AESRun(Aaes.c, atplength, false,savePath,nowTime);
          
            string a = "解密成功\n";
            richTextBox1.AppendText(a);
        }

        public void button8_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(nowTime+"\n");
            string dirPath = savePath + "\\加密文本";
            if (!(System.IO.Directory.Exists(dirPath)))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            FileStream ofs = new FileStream(savePath + "\\加密文本\\EncodeText" + nowTime + ".jpg", FileMode.OpenOrCreate);
            byte[,] ectext = Aaes.c;
            byte[] encodetext = PhotoProcess.Merger(ectext, PhotoProcess.Rlength);
            ofs.Write(encodetext, 0, encodetext.Length);
            ofs.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "请选择保存文件夹";
            if(folderBrowserDialog1.ShowDialog()==DialogResult.OK || folderBrowserDialog1.ShowDialog() == DialogResult.Yes)
            {
                textBox6.Text = folderBrowserDialog1.SelectedPath;
                savePath = folderBrowserDialog1.SelectedPath;
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };//对称密钥
            RSA Ares = new RSA();
            AES Aaes = new AES();
            //apk
            Ares.GetKeys();
            Aaes.k = key;
            apk2 = Ares.N;
            apk1 = Ares.E;
            //a公钥
            apk = string.Concat("(" + apk1.ToString() + "," + apk2.ToString() + ")");
            textBox2.Text = apk;
            string a = "A的公私钥生成完成！\n";
            richTextBox1.AppendText(a);

        }

        public void button1_Click(object sender, EventArgs e)
        {
            string a = "输入成功！\n";
            string b= "输入错误！\n";
           
            int[] t = new int[1];
            AESKeyInput aeski = new AESKeyInput();
            //随机生成对称密钥
            string s = aeski.GetRandomString(16, true, true, "");
           // string strtxt = textBox1.Text.ToString();//获取textbox1输入的值
            if (A.Is_Legal(s, t))
            {
                Aaes.k = A.KeyInput(s);
                label6.Visible = false;
                textBox1.Text = s;
                richTextBox1.AppendText(a);
            }
            else {
                string err = A.ErrorProcess(t);
                label6.Text = err;
                label6.Visible=true;
                richTextBox1.AppendText(b);

            }
           
             
        }


        public void button3_Click(object sender, EventArgs e)
        {



            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string file = textBox5.Text = ofd.FileName;
            }
            


            string a = "图片上传成功！\n";
            richTextBox1.AppendText(a);
        }

       
    }
}
