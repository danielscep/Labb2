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
        public virtual float Discount { get; } = 1;
        public string User { get; private set; }
        public string Pass { get; private set; }
        public List<Item> Cart { get; set; }
        public static List<Customer> ?Customers { get; set; }
        public int Currency { get; set; }

        public Object[,] Currencies;

        public Customer(string user, string pass)
        {
            Cart = new List<Item>();
            User = user;
            Pass = pass;
            Currency = 0;
            Currencies = new Object[4,2]
            {
                { "SEK", 1 },
                { "EUR", 0.091f },
                { "NOK", 0.95f },
                { "USD", 0.088f }
            };

        }

        public static void Buy(Customer customer, string itemName, int qty)
        {
            for (int i = 0; i < Item.Inventory.Count; i++)
            {
                if (Item.Inventory[i].Name.ToLower() == itemName.ToLower())
                {
                    Item invItem = Item.Inventory[i];

                    if (invItem.Quantity - qty < 0)
                    {
                        qty = invItem.Quantity;
                        invItem.Quantity = 0;
                    }
                    else
                    {
                        invItem.Quantity -= qty;
                    }

                    Item newItem = new Item(invItem.Name, qty, invItem.Price);
                    customer.Cart.Add(newItem);

                    Write(Serialize(Customers));
                    Item.Save();
                }
            }
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
                        Customers = Deserialize();
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
        }

        private static void Write(string jsonStr)
        {
            var customerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Customers.json");

            using (StreamWriter sw = new StreamWriter(customerPath))
            {
                sw.WriteLine(jsonStr);
            }

        }

        public static void Save()
        {
            Write(Serialize(Customers));
        }

        private static string Serialize(List<Customer> jsonStr)
        {
            return JsonSerializer.Serialize(jsonStr, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }

        private static List<Customer> Deserialize()
        {
            string json;
            var customerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Customers.json");

            using (StreamReader srC = new StreamReader(customerPath))
            {
                json = srC.ReadToEnd();
            }

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
