﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCS.Client
{
    public class Error
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return Status + ": " + Message;
        }
    }
}
