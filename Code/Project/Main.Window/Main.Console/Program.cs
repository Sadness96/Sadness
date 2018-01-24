using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Utils.Helper.Encryption;
using FileIO.Helper.BinaryFile;
using FileIO.Helper.ZIP;
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
            //BinaryFileHelper.GetFilePathFromBinaryData(OperationByte.GetByteArray("ice_system_plugin_menu", "ice_image_small", "ice_id = 1"), @"C:\Users\XiaoHua\Desktop\新建文件夹\2.png");
            //BinaryFileHelper.GetFilePathFromBinaryData(OperationByte.GetByteArray("ice_system_plugin_menu", "ice_image_large", "ice_id = 1"), @"C:\Users\XiaoHua\Desktop\新建文件夹\3.png");
            List<string> listFolderOrFilePath = new List<string>();
            listFolderOrFilePath.Add(@"E:\压缩文件测试目录\福建测试201801151547_上传_20180123111202_2787");
            ZIPHelper.CompressionZip(@"E:\压缩文件测试目录\temp1.zip", listFolderOrFilePath);
            ZIPHelper.CompressionZip(@"E:\压缩文件测试目录\temp2.zip", listFolderOrFilePath, "testtest");

            ZIPHelper.DeCompressionZip(@"E:\压缩文件测试目录\temp1.zip", @"E:\压缩文件测试目录\temp1\");
            ZIPHelper.DeCompressionZip(@"E:\压缩文件测试目录\temp2.zip", @"E:\压缩文件测试目录\temp2\", "testtest");

            //string strPath = @"E:\数据\其他项目\福建一类\2018年1月23日测试数据\福建测试201801151547_上传_20180123111202_2787_20180123111204.zip";
            //ZIPHelper.DeCompressionZip(strPath, Path.GetDirectoryName(strPath), "MAPZONE_2018_FJ_9");
            ZIP7Helper.Compression7ZipDirectory(@"E:\压缩文件测试目录\temp3.zip", @"E:\压缩文件测试目录\福建测试201801151547_上传_20180123111202_2787");
            ZIP7Helper.Compression7ZipDirectory(@"E:\压缩文件测试目录\temp4.zip", "test", @"E:\压缩文件测试目录\福建测试201801151547_上传_20180123111202_2787");

            ZIP7Helper.DeCompression7Zip(@"E:\压缩文件测试目录\temp3.7z", @"E:\压缩文件测试目录\temp3\");
            ZIP7Helper.DeCompression7Zip(@"E:\压缩文件测试目录\temp4.7z", @"E:\压缩文件测试目录\temp4\", "test");
        }
    }
}
