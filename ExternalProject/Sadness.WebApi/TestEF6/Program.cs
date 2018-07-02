using OfficeAutomationEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEF6
{
    class Program
    {
        static void Main(string[] args)
        {
            //新增
            officeautomationEntities ef = new officeautomationEntities();
            user_info user = new user_info();
            user.UserName = "00006";
            user.Password = "123456";
            ef.user_info.Add(user);
            ef.SaveChanges();
            //修改
            var query = ef.user_info.Where(o => o.UserName.Equals("00006")).FirstOrDefault();
            query.Password = "mq1i1JC92zal7nnbFZjtPQ==";
            ef.SaveChanges();
            //删除
            ef.user_info.Remove(query);
            ef.SaveChanges();
            //查询
            var v = ef.user_info.Where(o => o.UserName.Equals("00003")).ToList();
        }
    }
}
