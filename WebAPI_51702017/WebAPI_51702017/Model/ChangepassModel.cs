﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_51702017.Model
{
    public class ChangepassModel
    {
        public int accountId { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
