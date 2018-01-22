using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Utils.Helper.Encryption;
using FileIO.Helper.BinaryFile;
using Sadness.SQLiteDB.Connect;
using Sadness.SQLiteDB.Utils;
using Sadness.SQLiteDB.Models;

namespace Main.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //OperationByte.SaveByteArray("ice_system_plugin_toolbar", "ice_image", @"C:\Users\XiaoHua\Desktop\新建文件夹\1.png", "ice_id = '1'");
            BinaryFileHelper.GetFilePathFromBinaryData(OperationByte.GetByteArray("ice_system_plugin_menu", "ice_image_small", "ice_id = 1"), @"C:\Users\XiaoHua\Desktop\新建文件夹\2.png");
            BinaryFileHelper.GetFilePathFromBinaryData(OperationByte.GetByteArray("ice_system_plugin_menu", "ice_image_large", "ice_id = 1"), @"C:\Users\XiaoHua\Desktop\新建文件夹\3.png");
        }
    }
}
