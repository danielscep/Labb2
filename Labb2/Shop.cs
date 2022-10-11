using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;
using System.Xml.Linq;

namespace Labb2
{
    internal class Shop
    {
        public static List<Customer> GetDefaultCustomers()
        {
            List<Customer> customerBase = new();

            Customer c1 = new("Knatte", "123");
            Gold c2 = new("Fnatte", "321");
            Silver c3 = new("Tjatte", "213");
            Bronze c4 = new("Daniel", "qwerty");

            customerBase.Add(c1);
            customerBase.Add(c2);
            customerBase.Add(c3);
            customerBase.Add(c4);

            c4.Cart.Add(new Item("apple", 10, 9.9f));
            c4.Cart.Add(new Item("potato", 8, 12.7f));


            c2.Cart.Add(new Item("apple", 10, 9.9f));
            c2.Cart.Add(new Item("potato", 8, 12.7f));

            return customerBase;
        }
        public static List<Item> GetDefaultInventory()
        {
            var inventory = new List<Item>();
            inventory.Add(new Item("apple", 46, 9.9f));
            inventory.Add(new Item("pear", 67, 11.5f));
            inventory.Add(new Item("milk", 33, 17.9f));
            inventory.Add(new Item("egg", 67, 33.9f));
            inventory.Add(new Item("butter", 28, 24.9f));
            inventory.Add(new Item("bread", 82, 20f));
            inventory.Add(new Item("ham", 47, 28.9f));
            return inventory;
        }

        public static void WriteCustomers(string jsonStr)
        {
            var desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var jsonPath = Path.Combine(desktopDir, "Customers.json");

            using (StreamWriter sw = new StreamWriter(jsonPath))
            {
                sw.WriteLine(jsonStr);
            }
        }
        public static void WriteInventory(string jsonStr)
        {
            var desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var jsonPath = Path.Combine(desktopDir, "Inventory.json");

            using (StreamWriter sw = new StreamWriter(jsonPath))
            {
                sw.WriteLine(jsonStr);
            }
        }

        public static string SerializeFancy<T>(List<T> jsonStr)
        {
            return JsonSerializer.Serialize(jsonStr, new JsonSerializerOptions() {
                WriteIndented = true
            });
        }

        public static List<Customer> DeserializeCustomer(string json)
        {
            var customerBase = new List<Customer>();
            using (var jsonDoc = JsonDocument.Parse(json))
            {
                if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                    {
                        Customer a;
                        switch (jsonElement.GetProperty("Type").GetString())
                        {
                            case nameof(Customer):
                                a = jsonElement.Deserialize<Customer>();
                                customerBase.Add(a);
                                break;
                            case nameof(Bronze):
                                a = jsonElement.Deserialize<Bronze>();
                                customerBase.Add(a);
                                break;
                            case nameof(Silver):
                                a = jsonElement.Deserialize<Silver>();
                                customerBase.Add(a);
                                break;
                            case nameof(Gold):
                                a = jsonElement.Deserialize<Gold>();
                                customerBase.Add(a);
                                break;
                        }
                    }
                }
            }

            return customerBase;
        }


        public static void InitShopCustomers()
        {
            WriteCustomers(SerializeFancy(GetDefaultCustomers()));
        }
        public static void InitShopInventory()
        {
            WriteInventory(SerializeFancy(GetDefaultInventory()));
        }
        
    }
}