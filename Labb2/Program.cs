using System.Text.Json;

namespace Labb2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Item> inventory = new();
            List<Customer> customerBase = new();

            Customer c1 = new("Daniel","123");
            Customer c2 = new("Goran", "qwerty");
            Customer c3 = new("Dragan", "abcdefg");
            Customer c4 = new("Mirodrag", "Sljiva67");

            customerBase.Add(c1);
            customerBase.Add(c2);
            customerBase.Add(c3);
            customerBase.Add(c4);

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.IncludeFields = true;
            options.MaxDepth = Int32.MaxValue;
            options.IncludeFields = true;
            options.AllowTrailingCommas = true;
            string customerBaseJson = JsonSerializer.Serialize(customerBase, options);

            Item apple = new("apple", 440.5f);
            c4.Cart.Add(apple);

            Console.WriteLine(customerBaseJson);
            customerBaseJson = JsonSerializer.Serialize(c4.Cart, options);
            Console.WriteLine(customerBaseJson);


        }
    }
}