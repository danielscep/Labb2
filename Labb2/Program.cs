using System.Text.Json;
using System.Xml.Linq;

namespace Labb2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Item> inventory = new();
            List<Customer> customerBase = new();

            Customer c1 = new("Daniel","123");
            Gold c2 = new("Goran", "qwerty");
            Silver c3 = new("Dragan", "abcdefg");
            Bronze c4 = new("Miodrag", "Sljiva67");

            customerBase.Add(c1);
            customerBase.Add(c2);
            customerBase.Add(c3);
            customerBase.Add(c4);

            Item apple = new("apple", 440.5f);
            c4.Cart.Add(apple);


            
            string customerBaseJson = JsonSerializer.Serialize(customerBase, new JsonSerializerOptions()
                {WriteIndented = true});

            Console.WriteLine(customerBaseJson);
            Save(customerBaseJson);
        }
        
        static void Save(string jsonStr)
        {
            var desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var jsonPath = Path.Combine(desktopDir, "save.json");

            using (StreamWriter sw = new StreamWriter(jsonPath))
            {
                sw.WriteLine(jsonStr);
            }
        }
    }
}