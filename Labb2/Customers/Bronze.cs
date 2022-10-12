using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Bronze : Customer
    {
        public override string Type { get; } = nameof(Bronze);
        public override float Discount { get; } = 0.95f;


        public Bronze(string user, string pass) : base(user, pass)
        {
            
        }
    }
}
