using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Silver : Customer
    {
        public override string Type { get; } = nameof(Silver);

        public Silver(string name, string password) : base(name, password)
        {

        }
    }
}
