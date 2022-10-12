using System.Windows.Input;
namespace Labb2;

public class ShopMenu
{
    static private ConsoleKeyInfo userInput;
    private static Customer currentCustomer;


    public static void StartMenu()
    {
        Console.Clear();
        currentCustomer = null;
        string menu = "1 Sign In \n" +
                      "2 Register";

        Console.WriteLine(menu);

        userInput = Console.ReadKey(true);

        switch (userInput.Key)
        {
            case ConsoleKey.D1:
                SignIn();
                break;
            case ConsoleKey.D2:
                Register();
                break;
            default: 
                StartMenu();
                break;
        }
    }

    private static void SignIn()
    {
        Console.Clear();
        Console.Write("Username: ");
        string user = Console.ReadLine();
        Console.Write("Password: ");
        string pass = Console.ReadLine();
        currentCustomer = Customer.SignIn(user, pass);

        if (currentCustomer == null)
        {
            SignIn();
        }

        Shop();
    }

    private static void Register()
    {
        Console.Clear();
        Console.Write("Username: ");
        string user = Console.ReadLine();
        Console.Write("Password: ");
        string pass = Console.ReadLine();
        Console.Write("Verify password: ");
        string rPass = Console.ReadLine();

        while (pass != rPass)
        {
            Console.Write("Repeat password: ");
            rPass = Console.ReadLine();
        }

        try
        {
            currentCustomer = Customer.AddCustomer(user, pass);
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine("User already exists");
            Console.ReadKey(true);
            Register();
        }

        Shop();
    }


    private static void Shop(string message = "")
    {
        float convertCurrency = Convert.ToSingle(currentCustomer.Currencies[currentCustomer.Currency, 1]);
        Console.Clear();
        Console.WriteLine($"Signed in as {currentCustomer.User}\n");
        var itemIndex = 1;
        foreach (var item in Item.Inventory)
        {
            Console.WriteLine($"{itemIndex} | {char.ToUpper(item.Name[0]) + item.Name.Substring(1)} | Qty: {item.Quantity} | Price: {item.Price * convertCurrency} {currentCustomer.Currencies[currentCustomer.Currency, 0]} |");
            itemIndex++;
        }

        Console.WriteLine("\nQ - Quit");
        Console.WriteLine("S - Sign Out");
        Console.WriteLine("V - View Cart");
        Console.WriteLine("C - Checkout");
        Console.WriteLine($"Z - Currency: {currentCustomer.Currencies[currentCustomer.Currency, 0] }\n");
        Console.WriteLine(message);


        string input = Console.ReadLine().ToLower();

        switch (input)
        {
            case "q":
                Environment.Exit(0);
                break;
            case "s":
                StartMenu();
                break;
            case "v":
                ViewCart(convertCurrency);
                break;
            case "c":
                CheckOut();
                break;
            case "z":
                currentCustomer.Currency++;
                if (currentCustomer.Currency > 3)
                    currentCustomer.Currency = 0;
                break;
        }

        try
        {
            var ItemArray = input.Split(" ");
            var itemName = ItemArray[0];
            int itemQty;

            if (!int.TryParse(ItemArray[1], out itemQty))
            {
                Shop("Please enter a correct quantity");
            }
            Customer.Buy(currentCustomer,itemName,itemQty);


        }
        catch(IndexOutOfRangeException)
        {
            Shop("Please enter item quantity");
        }

        Shop();
    }

    private static void CheckOut()
    {
        currentCustomer.Cart = new List<Item>();
        Customer.Save();
    }

    private static void ViewCart(float convertCurrency)
    {
        string discount = String.Empty;
        Console.Clear();
        Console.WriteLine("Your cart\n");

        float totalPrice = 0;

        foreach (var item in currentCustomer.Cart)
        {
            Console.WriteLine($"| {char.ToUpper(item.Name[0]) + item.Name.Substring(1)} | Qty: {item.Quantity} | Price: {item.Price * convertCurrency} {currentCustomer.Currencies[currentCustomer.Currency, 0]} | Total price: {item.Price * item.Quantity * convertCurrency}");
            totalPrice += item.Price * item.Quantity * currentCustomer.Discount * convertCurrency;
        }

        if (currentCustomer.Discount < 1)
        {
            discount = $", your current discount is {currentCustomer.Discount.ToString("p")}";
        }

        Console.WriteLine($"\nTotal price: {totalPrice}{discount}");
        Console.WriteLine("Press any key to return.");
        Console.ReadKey();
        Shop();
    }
}