using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_51702017.Model
{
    public class AccountModel
    {
        public int accountId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int cost { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public int countOrder { get; set; }
        public int role { get; set;  }


    }
}
