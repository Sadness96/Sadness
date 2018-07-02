using OfficeAutomationEF;
using Sadness.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sadness.WebApi.Controllers
{
    public class UserLoginController : ApiController
    {
        [HttpGet]
        public UserLoginModels GetUserLogin(string UserName, string Password, DateTime CurrentTime)
        {
            UserLoginModels user = new UserLoginModels();
            user.UserName = UserName;
            user.LoginTime = CurrentTime;
            officeautomationEntities db = new officeautomationEntities();
            var vQuery = db.user_info.Where(o => o.UserName.Equals(UserName) && o.Password.Equals(Password)).ToList();
            if (vQuery != null && vQuery.Count >= 1)
            {
                user.IsLogin = true;
                user.Id = vQuery.FirstOrDefault().Id;
                user.RealName = vQuery.FirstOrDefault().RealName;
                user.Sex = vQuery.FirstOrDefault().Sex;
                user.IdNumber = vQuery.FirstOrDefault().IdNumber;
                user.PhoneNumber1 = vQuery.FirstOrDefault().PhoneNumber1;
                user.PhoneNumber2 = vQuery.FirstOrDefault().PhoneNumber2;
                user.QQNumber = vQuery.FirstOrDefault().QQNumber;
                user.EMailBox = vQuery.FirstOrDefault().EMailBox;
                user.Address = vQuery.FirstOrDefault().Address;
                user.State = vQuery.FirstOrDefault().State;
            }
            else
            {
                user.IsLogin = false;
            }
            return user;
        }
    }
}
