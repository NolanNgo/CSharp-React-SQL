using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI_51702017.Model;

namespace WebAPI_51702017.Model
{
    public class messageSend
    {
        public int codeRes { get; set; }
        public bool statusRes { get; set; }
        public string message { get; set; }
        public Object objAcc { get; set; }
        public string jwtoken { get; set; }
        public string showMess()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
