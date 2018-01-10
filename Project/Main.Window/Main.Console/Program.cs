using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using FileIO.Helper.ZIP;
using IceElves.SQLiteDB.Connect;
using IceElves.SQLiteDB.Utils;
using IceElves.SQLiteDB.Models;

namespace Main.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<string> list = new List<string>();
            //list.Add(@"E:\读取文件路径测试\1\2\3\4.txt");
            //list.Add(@"E:\读取文件路径测试\1.txt");
            //ZIP7Helper.Compression7Zip(@"C:\Users\XiaoHua\Desktop\zipTest\test1.7z", list.ToArray());
            //ZIP7Helper.Compression7Zip(@"C:\Users\XiaoHua\Desktop\zipTest\test2.7z", "123", list.ToArray());

            //ZIP7Helper.Compression7ZipDirectory(@"C:\Users\XiaoHua\Desktop\zipTest\test11.7z", @"E:\读取文件路径测试");
            //ZIP7Helper.Compression7ZipDirectory(@"C:\Users\XiaoHua\Desktop\zipTest\test22.7z", "123", @"E:\读取文件路径测试");

            ZIP7Helper.DeCompression7Zip(@"C:\Users\XiaoHua\Desktop\zipTest\test1.7z", @"C:\Users\XiaoHua\Desktop\zipTest\test1");
            ZIP7Helper.DeCompression7Zip(@"C:\Users\XiaoHua\Desktop\zipTest\test2.7z", @"C:\Users\XiaoHua\Desktop\zipTest\test2", "123");
            ZIP7Helper.DeCompression7Zip(@"C:\Users\XiaoHua\Desktop\zipTest\test11.7z", @"C:\Users\XiaoHua\Desktop\zipTest\test11");
            ZIP7Helper.DeCompression7Zip(@"C:\Users\XiaoHua\Desktop\zipTest\test22.7z", @"C:\Users\XiaoHua\Desktop\zipTest\test22", "123");
        }
    }
}
