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
            Customer.InitShopCustomers();
            Item.InitShopInventory();

            ShopMenu.StartMenu();

        }
    }
}