﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Bronze : Customer
    {
        public override string Type { get; } = nameof(Bronze);

        public Bronze(string name, string password) : base(name, password)
        {

        }
    }
}
