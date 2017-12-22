using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using IceElves.SQLiteDB.Connect;
using IceElves.SQLiteDB.Utils;
using IceElves.SQLiteDB.Models;

namespace Main.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ice_system_images> listImage1 = OperationEntityList.GetEntityList<ice_system_images>("ice_system_images", null);
            List<ice_system_images> listImage2 = OperationEntityList.GetEntityList<ice_system_images>("ice_system_images", string.Format("ice_sys_key = 'AppSmallIcon'"));
        }
    }
}
