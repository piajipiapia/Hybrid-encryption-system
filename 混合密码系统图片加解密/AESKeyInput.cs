using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid_Cryptosystem
{
    class AESKeyInput
    {

        public string GetRandomString(int length, bool useNum, bool useUpp, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }



        public bool Is_Legal(string str,int[] error)
        {
            bool flag = true;
            char[] ch = str.ToCharArray();
            if (ch.Length != 16) flag = false;
            if (flag)
            {
                for(int i = 0; i < ch.Length; ++i)
                {
                    if(ch[i]>='0'&&ch[i]<='9' || ch[i] >= 'A' && ch[i] <= 'Z')
                    {
                        continue;
                    }
                    else
                    {
                        flag = false;
                        error[0] = 2;
                        break;
                    }
                }
            }
            else
            {
                error[0] = 1;
            }

            return flag;
        }

        public byte[] KeyInput(string str)
        {
            byte[] strbyte;
            strbyte = System.Text.Encoding.ASCII.GetBytes(str);
            return strbyte;
        }

        public string ErrorProcess(int[] error)
        {
            string str;
            if (error[0] == 1)
            {
                str = "输入长度不等于16！";
            }
            else
            {
                str = "输入内容不合法！";
            }
            return str;
        }
    }
}
