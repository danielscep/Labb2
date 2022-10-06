using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Gold : Customer
    {
        public override string Type { get; } = nameof(Gold);

        public Gold(string name, string password) : base(name, password)
        {

        }
    }
}
