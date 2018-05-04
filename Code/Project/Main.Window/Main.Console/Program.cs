using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Utils.Helper.Encryption;
using System.Drawing.Imaging;
using Sadness.SQLiteDB.Connect;

namespace Main.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainImage.SaveImageByteArray("QRCodeLogo", @"C:\Users\XiaoHua\Desktop\logo.png");
        }
    }
}
