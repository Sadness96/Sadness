using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sadness.WebApi.Models
{
    public class UserLoginModels
    {
        public string UserName { get; set; }
        public bool IsLogin { get; set; }
        public DateTime LoginTime { get; set; }
        public int Id { get; set; }
        public string RealName { get; set; }
        public string Sex { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string QQNumber { get; set; }
        public string EMailBox { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
    }
}