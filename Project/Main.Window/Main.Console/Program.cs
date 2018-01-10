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
            bool b1 = AESHelper.FileAESEncrypt(@"C:\Users\XiaoHua\Desktop\Navicat_PDF_Win.pdf", @"C:\Users\XiaoHua\Desktop\Navicat_PDF_Win(1).pdf", "123321");
            bool b2 = AESHelper.FileAESDecrypt(@"C:\Users\XiaoHua\Desktop\Navicat_PDF_Win(1).pdf", @"C:\Users\XiaoHua\Desktop\Navicat_PDF_Win(2).pdf", "123321");
            bool b3 = AESHelper.FileAESDecrypt(@"C:\Users\XiaoHua\Desktop\Navicat_PDF_Win(1).pdf", @"C:\Users\XiaoHua\Desktop\Navicat_PDF_Win(3).pdf", "124321");
        }
    }
}
