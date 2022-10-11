using System.Windows.Input;
namespace Labb2;

public class ShopMenu
{
    static private ConsoleKeyInfo userInput;
    private static Customer currentCustomer;
    public static void StartMenu()
    {
        Console.Clear();
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

    public static void SignIn()
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
    }

    public static void Register()
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
            Customer.AddCustomer(user, pass);
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine("User already exists");
            Console.ReadKey(true);
            Register();
        }

    }


    public static void Shop()
    {

    }
}