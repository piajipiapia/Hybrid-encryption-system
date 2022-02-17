using System;
using System.Runtime.InteropServices;
using Hybrid_Cryptosystem;

namespace Hybrid_Cryptosystem
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            //RSA.RSARun();
            //AES.AESRun();
            byte[,] tempPhoto = PhotoProcess.InputPhoto("D:\\图片\\IMG_3090(20191219-220433).JPG");
            
            byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };//对称密钥
            long[] encode;
            byte[] decode;
            int atplength = PhotoProcess.Rlength;
            RSA Bres = new RSA();
            AES Aaes = new AES();
            AES Baes = new AES();
            Bres.GetKeys();
            Aaes.k = key;
            Console.WriteLine(BitConverter.ToString(Aaes.k));
            Aaes.AESRun(tempPhoto, atplength, true);
            encode = Bres.RSARun(Aaes.k);
            decode = Bres.RSADecode(encode);
            Baes.k = decode;
            Console.WriteLine(BitConverter.ToString(Baes.k));
            Baes.AESRun(Aaes.c, atplength, false);
        }

        
       




    }
}
