using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_51702017.Model
{
    public class OrderModel
    {
        /*dung de khoi tao 1 order voi ID*/
        public string orderId { get; set; }
        public int accountId { get; set; }
        public string productId { get; set; }
        public int countProduct { get; set; }
        /*chi dung de hien thi ra */
        public string userName { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int cost { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string NameProduct { get; set; }
        public int Prices { get; set; }
        public int Storage { get; set; }
        public string Image { get; set; }
        public int ProductTypeID { get; set; }


    }
}
