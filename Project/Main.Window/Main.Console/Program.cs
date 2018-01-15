using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Utils.Helper.Encryption;
using IceElves.SQLiteDB.Connect;
using IceElves.SQLiteDB.Utils;
using IceElves.SQLiteDB.Models;

namespace Main.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            OperationByte.SaveByteArray("ice_system_plugin_menu", "ice_image_small", @"C:\Users\XiaoHua\Desktop\16.png", "ice_id = '1'");
            OperationByte.SaveByteArray("ice_system_plugin_menu", "ice_image_large", @"C:\Users\XiaoHua\Desktop\32.png", "ice_id = '1'");
        }
    }
}
