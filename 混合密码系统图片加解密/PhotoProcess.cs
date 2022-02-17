using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using Hybrid_Cryptosystem;
using WindowsFormsApp1;
namespace Hybrid_Cryptosystem
{
     class PhotoProcess
    {

       
        public int count = 0;
        private static byte[] byteData;//存放转换后的图片二进制流数据
        private static byte[] afterProcessPhoto;//存放合并后的字节数组
        private static byte[,] splitData;//存放分割处理后的二进制流数据
        private static int length,apLength,rlength;//length:原始图片位数组长度; apLength:存放合并处理后数据的总长度（大小）; rlength:存放分割处理后数组的行数
       
        public static int Length { get => length; set => length = value; }
        public static int ApLength { get => apLength; set => apLength = value; }
        public static int Rlength { get => rlength; set => rlength = value; }

        //图片输入处理（分组和补0）
        public static  byte[,] InputPhoto(string fileName)//输入图片，转为二进制流，分割处理后输出
        {
            
            byteData = GetPictureData(fileName);
            BitArray myba = new BitArray(byteData);//将二进制流数组转换成位数组
            Length = myba.Length;//使用位数组查看图片总长度（大小）
            Console.WriteLine(Length / 8);//（length/8）为字节数组的长度
            splitData = Split(byteData, Length / 8);//分割图片，返回二维字节数组
            return splitData;
            
        }

        //图片输出处理（合并分组）
        public static  void OutputPhoto(byte[,] splitData, string savePath,string nowTime)
        {
            afterProcessPhoto = Merger(splitData, Rlength);
            PushPicture(afterProcessPhoto,savePath,nowTime);

        }

        //图片分割
        public static  byte[,] Split(byte[] photoData, int dataLength)
        {
            if(dataLength % 16 != 0)
            {
                Rlength = (dataLength/16) +1;
            }
            else
            {
                Rlength = dataLength / 16;
            }
            
            byte[,] ps = new byte[Rlength,16];
            for(int i = 0; i < Rlength; ++i)
            {
                for(int j = 0; j < 16; ++j)
                {
                    if (i*16+j<dataLength)
                        ps[i, j] = photoData[i * 16 + j];
                    else
                        ps[i, j] = 0x00;
                }
            }
            return ps;
        }

        //二进制图片流合并
        public static  byte[] Merger(byte[,] splitPhoto,int rowLength)
        {
            ApLength = rowLength * 16;
            byte[] photo = new byte[ApLength];
            for(int i = 0; i < rowLength; ++i)
            {
                for(int j = 0; j < 16; ++j)
                {
                    photo[i * 16 + j] = splitPhoto[i,j];
                }
            }

            return photo;
        }

        //图片输入(转为字节流)
        public static byte[] GetPictureData(string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.OpenOrCreate);
            byte[] byteData = new byte[fs.Length];
            fs.Read(byteData, 0, byteData.Length);
            fs.Close();
            return byteData;
        }

        //图片输出
        public static void PushPicture(byte[] pictureByteData,string savePath,string nowTime)
        {
            //传Image下图片的路径
            //Form1 f = new Form1();
            string name = savePath+"\\Picture"+nowTime +".jpg";
            string imgPath = name;
                //string imgPath = "G:\\密码学\\WindowsFormsApp1\\WindowsFormsApp1\\bin\\Debug\\Images" + imgName + ".jpg";
            MemoryStream ms = new MemoryStream(pictureByteData);
            Bitmap bmp = new Bitmap(ms);
            bmp.Save(imgPath, ImageFormat.Bmp);
            bmp.Dispose();
            ms.Close();
        }
    }
}
