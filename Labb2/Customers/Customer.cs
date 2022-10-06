using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Customer
    {
        public virtual string Type { get; } = nameof(Customer);
        public string Name { get; }
        public string Password { get; }
        public List<Item> Cart { get; }

        public Customer(string name, string password)
        {
            Cart = new List<Item>();
            Name = name;
            Password = password;
        }
    }
}
