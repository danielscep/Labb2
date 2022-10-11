using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Customer
    {
        public virtual string Type { get; } = nameof(Customer);
        public string User { get; private set; }
        public string Pass { get; private set; }
        public List<Item> Cart { get; private set; }
        public static List<Customer> ?Customers { get; private set; }

        public Customer(string user, string pass)
        {
            Cart = new List<Item>();
            User = user;
            Pass = pass;
        }

        public static Customer AddCustomer(string user, string pass)
        {
            foreach (var customer in Customers)
            {
                if (customer.User == user)
                    throw new NullReferenceException();

            }

            Customer newCustomer = new Customer(user, pass);

            Customers.Add(newCustomer);

            Write(Serialize(Customers));
            return newCustomer;
        }

        public static Customer SignIn(string user, string pass)
        {
            foreach (var customer in Customers)
            {
                if (customer.User == user && customer.Pass == pass)
                    return customer;
            }
            return null;
        }

        public static void InitShopCustomers()
        {
            var customerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Customers.json");

            if (File.Exists(customerPath))
            {
                using (StreamReader srC = new StreamReader(customerPath))
                {
                    try
                    {
                        Customers = Deserialize(srC.ReadToEnd());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed to parse JSON, please resolve Json syntax or delete file to restore default customers \n");
                        throw;
                    }
                }
            }
            else
            {
                List<Customer> customers = new();

                Customer c1 = new("Knatte", "123");
                Gold c2 = new("Fnatte", "321");
                Silver c3 = new("Tjatte", "213");
                Bronze c4 = new("Daniel", "qwerty");

                customers.Add(c1);
                customers.Add(c2);
                customers.Add(c3);
                customers.Add(c4);

                c4.Cart.Add(new Item("apple", 10, 9.9f));
                c4.Cart.Add(new Item("potato", 8, 12.7f));


                c2.Cart.Add(new Item("apple", 10, 9.9f));
                c2.Cart.Add(new Item("potato", 8, 12.7f));
                Customers = customers;
                Write(Serialize(Customers));
            }

            Console.WriteLine(Serialize(Customers));
        }

        private static void Write(string jsonStr)
        {
            var customerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Customers.json");

            using (StreamWriter sw = new StreamWriter(customerPath))
            {
                sw.WriteLine(jsonStr);
            }

        }

        public static string Serialize(List<Customer> jsonStr)
        {
            return JsonSerializer.Serialize(jsonStr, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }

        public static List<Customer> Deserialize(string json)
        {
            var customerBase = new List<Customer>();
            using (var jsonDoc = JsonDocument.Parse(json))
            {
                if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                    {
                        Customer customer;
                        List<Item> cart = JsonSerializer.Deserialize<List<Item>>(jsonElement.GetProperty("Cart").GetRawText());

                        switch (jsonElement.GetProperty("Type").GetString())
                        {
                            case nameof(Customer):
                                customer = jsonElement.Deserialize<Customer>();
                                customer.Cart = cart;
                                customerBase.Add(customer);
                                break;
                            case nameof(Bronze):
                                customer = jsonElement.Deserialize<Bronze>();
                                customer.Cart = cart;
                                customerBase.Add(customer);
                                break;
                            case nameof(Silver):
                                customer = jsonElement.Deserialize<Silver>();
                                customer.Cart = cart;
                                customerBase.Add(customer);
                                break;
                            case nameof(Gold):
                                customer = jsonElement.Deserialize<Gold>();
                                customer.Cart = cart;
                                customerBase.Add(customer);
                                break;
                        }
                    }
                }
            }

            return customerBase;
        }
    }

}
