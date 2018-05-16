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

namespace Main.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("f1");
            dt.Columns.Add("f2");
            dt.Columns.Add("f3");
            for (int i = 0; i < 25; i++)
            {
                DataRow dr = dt.NewRow();
                dr["f1"] = i;
                dr["f2"] = i;
                dr["f3"] = i;
                dt.Rows.Add(dr);
            }
            DataSet dsTest = DataProcessing.PagingQuery(dt, 99);
            DataTable dtTest = DataProcessing.PagingQuery(dt, 2, 99);
        }
    }
}
