﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ResponseModels
{
    public class LoginResponse
    {
        public required string Token { get; set; }
    }
}