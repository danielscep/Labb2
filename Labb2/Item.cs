using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Item
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        private static List<Item> Inventory { get; set; }

        public static void InitShopInventory()
        {
            var inventoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Inventory.json");
            if(File.Exists(inventoryPath))
            {
                using (StreamReader srI = new StreamReader(inventoryPath))
                {
                    try
                    {
                        Inventory = JsonSerializer.Deserialize<List<Item>>(srI.ReadToEnd());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed to parse JSON, please resolve Json syntax or delete file to restore default Inventory \n");
                        throw;
                    }
                }
            }
            else
            {
                var inventory = new List<Item>();
                inventory.Add(new Item("apple", 46, 9.9f));
                inventory.Add(new Item("pear", 67, 11.5f));
                inventory.Add(new Item("milk", 33, 17.9f));
                inventory.Add(new Item("egg", 67, 33.9f));
                inventory.Add(new Item("butter", 28, 24.9f));
                inventory.Add(new Item("bread", 82, 20f));
                inventory.Add(new Item("ham", 47, 28.9f));

                Write(Serialize(inventory));
            }
        }

        private static void Write(string jsonStr)
        {
            var desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var jsonPath = Path.Combine(desktopDir, "Inventory.json");

            using (StreamWriter sw = new StreamWriter(jsonPath))
            {
                sw.WriteLine(jsonStr);
            }
        }

        public static string Serialize(List<Item> jsonStr)
        {
            return JsonSerializer.Serialize(jsonStr, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }

        public Item(string name, int quantity, float price)
        {
            Quantity = quantity;
            Name = name;
            Price = price;
        }
    }
}