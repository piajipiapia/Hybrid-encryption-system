using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using WindowsFormsApp1;

namespace Hybrid_Cryptosystem
{
    class RSA
    {
        public int p, q, d, e = 0, n, euler;
        public int[] ml = new int[16];//明文数字存放
        public long[] cl = new long[16];//密文数字存放
        public int[] dem = new int[16];//解密后明文数字存放
        public byte[] pbyte = new byte[16];//明文的字节数组
        public int P { get => p; set => p = value; }
        public int Q { get => q; set => q = value; }
        public int D { get => d; set => d = value; }//私钥，满足与n的欧拉函数互逆 
        public int E { get => e; set => e = value; }//公钥，e满足与n的欧拉函数互逆
        public int N { get => n; set => n = value; }//公钥，大素数的乘积
        public int Euler { get => euler; set => euler = value; }

        //RSA的构造函数
        public RSA() { }

        //获取公、私钥
        public void GetKeys()
        {
            var tupple = PrimeNumber();
            P = tupple.Item1;
            Q = tupple.Item2;
            N = P * Q;
            Euler = (P - 1) * (Q - 1);
            for (int i = 2; i < Euler; ++i)
            {        //获取e、d
                int gcd = Exgcd(i, Euler);
                if (gcd == 1 && D > 0)
                {
                    E = i;
                    break;
                }
            }
        }



        //RSA加密
        public  long[] RSARun(byte[] mw)
        {
            for(int i = 0; i < 16; ++i)
            {
                ml[i] = mw[i];
            }
            
            for (int i = 0; i < 16; ++i)
            {
                int temp = ml[i];
                cl[i] = FastExp(temp, E, N);
            }
            return cl;
          
        }

        //RSA解密
        public byte[] RSADecode(long[] cw)
        {
            for (int i = 0; i < 16; ++i)
            {
                dem[i] = (int)FastExp((int)cw[i], D, N);
            }
            for (int i = 0; i < 16; ++i)
            {
                pbyte[i] = (byte)dem[i];
            }
            return pbyte;
        }

        //二进制转换,返回二进制位数
        public  int BianaryTransform(int num, int[] bin_num)
        {
            int i = 0, mod = 0;
            while (num != 0)
            {
                mod = num % 2;
                bin_num[i] = mod;
                num /= 2;
                i++;
            }
            return i;
        }
        //素性鉴定
        public  bool IsPrime(int num)
        {
            for (int i = 2; i < System.Math.Sqrt(num); ++i)
            {
                if (num % i == 0) return false;
            }
            return true;
        }
        //生成10000~20000之间的素数
        public  Tuple<int, int> PrimeNumber()
        {
            Random random = new Random();
            int p, q;
            while (true)
            {
                p = random.Next(10000, 20000);
                if (IsPrime(p))
                    break;
            }
            while (true)
            {
                q = random.Next(10000, 20000);
                if (IsPrime(q) && p != q)
                    break;
            }
            Tuple<int, int> tuple = new Tuple<int, int>(p, q);
            return tuple;
        }

        //扩展欧几里得
        public  int Exgcd(int m, int n)
        {
            int x1, y1, x0, y0, y;
            x0 = 1;
            y0 = 0;
            x1 = 0;
            y1 = 1;
            D = 0;
            y = 1;
            int r = m % n;
            int q = (m - r) / n;
            while (r > 0)
            {
               D = x0 - q * x1;
                y = y0 - q * y1;
                x0 = x1;
                y0 = y1;
                x1 = D;
                y1 = y;
                m = n;
                n = r;
                r = m % n;
                q = (m - r) / n;
            }
            return n;
        }
        //快速指数算法
        public  long FastExp(int a, int b, int n)
        {
            int[] bin_num = new int[50];
            long d = 1;
            int k = BianaryTransform(b, bin_num) - 1;

            for (int i = k; i >= 0; i--)
            {
                d = (d * d) % n;
                if (bin_num[i] == 1)
                {
                    d = (d * a) % n;
                }
            }
            return d;
        }
    }
}
