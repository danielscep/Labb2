using System.IO;
using System.Text.Json;
using System.Transactions;
using System.Xml.Linq;

namespace Labb2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Customers.json");
            var inventoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Inventory.json");

            if (!File.Exists(customerPath))
                Shop.InitShopCustomers();

            if (!File.Exists(inventoryPath))
                Shop.InitShopInventory();

            using StreamReader srC = new StreamReader(customerPath);
            {
                try
                {
                    List<Customer> customerBase = Shop.DeserializeCustomer(srC.ReadToEnd());
                    Customer.SetCustomers(customerBase);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to parse JSON, please resolve Json syntax or delete file to restore default customers \n");
                    throw;
                }
            }

            using StreamReader srI = new StreamReader(inventoryPath);
            {
                try
                {
                    List<Item> inventory = JsonSerializer.Deserialize<List<Item>>(srI.ReadToEnd());
                    Item.SetInventory(inventory);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to parse JSON, please resolve Json syntax or delete file to restore default Inventory \n");
                    throw;
                }
            }

            ShopMenu.StartMenu();

        }
    }
}