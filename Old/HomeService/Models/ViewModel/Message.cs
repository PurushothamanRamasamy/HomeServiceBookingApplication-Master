﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Models.ViewModel
{
    public class Message
    {
        public string DispalyMessage { get; set; }
        public string ToAction { get; set; }
        public string ToControl { get; set; }
        public bool IsInput { get; set; }
        public string Id { get; set; }
        public string Inputdata { get; set; }
    }
}
