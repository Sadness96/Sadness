using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using IceElves.SQLiteDB.Connect;

namespace Main.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainImage.SaveImageByteArray("AppSmallIcon", @"C:\Users\XiaoHua\Desktop\ms_office_16x16.png");
            MainImage.SaveImageByteArray("AppLargeIcon", @"C:\Users\XiaoHua\Desktop\ms_office_32x32.png");
        }
    }
}
