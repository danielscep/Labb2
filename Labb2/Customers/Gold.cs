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
        public override float Discount { get; } = 0.85f;


        public Gold(string user, string pass) : base(user, pass)
        {

        }
    }
}
