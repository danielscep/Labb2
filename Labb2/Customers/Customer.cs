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

        private static List<Customer> Customers { get; set; }

        public Customer(string name, string password)
        {
            Cart = new List<Item>();
            Name = name;
            Password = password;
        }

        public static void SetCustomers(List<Customer> customers)
        {
            Customers = customers;
        }

        public static void AddCustomer(List<Customer> customers)
        {
            Customers = customers;
        }

        public static Customer SignIn(string user, string pass)
        {
            foreach (var customer in Customers)
            {
                if (customer.Name == user && customer.Password == pass)
                    return customer;
            }

            return null;
        }
    }
}
