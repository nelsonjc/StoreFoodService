﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFood.Domain.DTOs.Requests
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
