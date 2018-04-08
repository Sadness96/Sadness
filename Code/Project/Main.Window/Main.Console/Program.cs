using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Utils.Helper.Encryption;
using System.Drawing.Imaging;

namespace Main.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string strBase64Encrypt = Base64Helper.Base64Encrypt("test测试123");
            string strBase64Decrypt = Base64Helper.Base64Decrypt(strBase64Encrypt);

            string strFileBase64_1jpg = Base64Helper.ImageBase64Encrypt(@"C:\Users\XiaoHua\Desktop\1.jpg", ImageFormat.Jpeg);
            string strFileBase64_2png = Base64Helper.ImageBase64Encrypt(@"C:\Users\XiaoHua\Desktop\2.png", ImageFormat.Png);
            string strFileBase64_3gif = Base64Helper.ImageBase64Encrypt(@"C:\Users\XiaoHua\Desktop\3.gif", ImageFormat.Gif);

            bool bFileBase64_11jpg = Base64Helper.ImageBase64Decrypt(strFileBase64_1jpg, @"C:\Users\XiaoHua\Desktop\11.jpg", ImageFormat.Jpeg);
            bool bFileBase64_22png = Base64Helper.ImageBase64Decrypt(strFileBase64_2png, @"C:\Users\XiaoHua\Desktop\22.png", ImageFormat.Png);
            bool bFileBase64_33gif = Base64Helper.ImageBase64Decrypt(strFileBase64_3gif, @"C:\Users\XiaoHua\Desktop\33.gif", ImageFormat.Gif);
        }
    }
}
