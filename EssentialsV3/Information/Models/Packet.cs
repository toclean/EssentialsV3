﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information.Models
{
    public class Packet
    {
        public string Type { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
    }
}
