using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Hybrid_Cryptosystem;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            

           
            //显示对称密钥key：
            /* BitConverter.ToString(Aaes.k);
             Aaes.AESRun(tempPhoto, atplength, true);
             encode = Bres.RSARun(Aaes.k);
             decode = Bres.RSADecode(encode);
             Baes.k = decode;
             Console.WriteLine(BitConverter.ToString(Baes.k));
             Baes.AESRun(Aaes.c, atplength, false);*/
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}





