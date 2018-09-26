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
using ADO.Helper.DatabaseConversion;
using FileIO.Helper.ZIP;
using Utils.Helper.Redis;
using Utils.Helper.CheckCorrectness;
using Newtonsoft.Json;
using FileIO.Helper.TXT;

namespace Main.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            #region GZIP压缩测试方法
            //string strJsonData = TXTHelper.GetFileString(@"\\192.168.2.64\共享文件\test.txt");
            //string Compression = GZIPHelper.CompressionStringGZIP(strJsonData);
            //string DeCompression = GZIPHelper.DeCompressionStringGZIP(Compression);
            #endregion

            #region IP地址效验测试方法
            //bool b1 = CheckCorrectnessHelper.CheckInternetProtocol("36.102.228.108");
            //bool b2 = CheckCorrectnessHelper.CheckInternetProtocol("192.168.2.123");
            //bool b3 = CheckCorrectnessHelper.CheckInternetProtocol("255.255.255.255");
            //bool b4 = CheckCorrectnessHelper.CheckInternetProtocol("300.255.255.255");
            //bool b5 = CheckCorrectnessHelper.CheckInternetProtocol("256.255.255.0");
            //bool b6 = CheckCorrectnessHelper.CheckInternetProtocol("0.0.134.255");
            #endregion

            #region Redis测试方法
            //RedisHelper redis = new RedisHelper();
            ////增
            //List<test> list0 = new List<test>();
            //list0.Add(new test { index = 0, bIsChoice = true, strUserNmae = "111", strPassword = "222" });
            //list0.Add(new test { index = 0, bIsChoice = true, strUserNmae = "333", strPassword = "444" });
            //list0.Add(new test { index = 0, bIsChoice = true, strUserNmae = "555", strPassword = "666" });
            //redis.StringSet("test", JsonConvert.SerializeObject(list0));
            //redis.StringSet("user:test", JsonConvert.SerializeObject(list0));
            ////改
            //List<test> list1 = new List<test>();
            //list1.Add(new test { index = 0, bIsChoice = true, strUserNmae = "aaa", strPassword = "bbb" });
            //list1.Add(new test { index = 0, bIsChoice = true, strUserNmae = "ccc", strPassword = "ddd" });
            //redis.StringSet("test", JsonConvert.SerializeObject(list1));
            //redis.StringSet("user:test", JsonConvert.SerializeObject(list1));
            ////查
            //if (redis.KeyExists("test"))
            //{
            //    List<test> list2 = JsonConvert.DeserializeObject<List<test>>(redis.StringGet("test"));
            //}
            //if (redis.KeyExists("user:test"))
            //{
            //    List<test> list3 = JsonConvert.DeserializeObject<List<test>>(redis.StringGet("user:test"));
            //}
            ////删
            //redis.KeyDelete("test");
            //redis.KeyDelete("user:test");
            #endregion
        }
    }

    public class test
    {
        public int index { get; set; }
        public bool bIsChoice { get; set; }
        public string strUserNmae { get; set; }
        public string strPassword { get; set; }
    }
}
