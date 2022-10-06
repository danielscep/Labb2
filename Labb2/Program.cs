using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace Labb2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customerBase = new List<Customer>();
            var inventory = new List<Item>();

            var customerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Customers.json");
            var inventoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Inventory.json");

            if (!File.Exists(customerPath))
                Misc.InitShopCustomers();

            if (!File.Exists(inventoryPath))
                Misc.InitShopInventory();

            using StreamReader srC = new StreamReader(customerPath);
            {
                try
                {
                    customerBase = Misc.DeserializeCustomer(srC.ReadToEnd());
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
                    inventory = JsonSerializer.Deserialize<List<Item>>(srI.ReadToEnd());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to parse JSON, please resolve Json syntax or delete file to restore default Inventory \n");
                    throw;
                }
            }
        }
    }
}