Console.WriteLine("Welcome to the Password Manager");
Console.WriteLine("Select an option:");
Console.WriteLine("1. Generate a password");
Console.WriteLine("2. Show saved passwords");

if (int.TryParse(Console.ReadLine(), out int option))
{
    switch (option)
    {
        case 1:
            GeneratePasswordOption();
            break;
        case 2:
            ShowSavedPasswordsOption();
            break;
        default:
            Console.WriteLine("Invalid option. Please select 1 or 2.");
            break;
    }
}
else
{
    Console.WriteLine("Invalid input. Please enter a number.");
}

void GeneratePasswordOption()
{
    string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    Console.WriteLine("Enter password length:");
    int length = int.Parse(Console.ReadLine());

    string password = GeneratePassword(length, characters);
    Console.WriteLine($"Generated password: {password}");

    static string GeneratePassword(int length, string characters)
    {
        Random random = new Random();
        string password = "";

        for (int i = 0; i < length; i++)
        {
            int randomIndex = random.Next(characters.Length);
            password += characters[randomIndex];
        }

        return password;
        
    }
    Console.WriteLine("Do you want to save this password? (y/n)");
    string saveOption = Console.ReadLine();
    if (saveOption.ToLower() == "y")
    {
        Console.WriteLine("Enter a site for this password:");
        string site = Console.ReadLine();
        string data = $"{site}: {password}";
        File.AppendAllText("passwords.txt", data + "\n");
        Console.WriteLine("Password saved successfully.");
    }

else
    {
        Console.WriteLine("Password not saved.");
    }
    Console.WriteLine("Press any key to return to the main menu...");
    Console.ReadKey();
}

void ShowSavedPasswordsOption()
{
    Console.WriteLine("Saved passwords:");
    if (File.Exists("passwords.txt"))
    {
        string[] passwords = File.ReadAllLines("passwords.txt");
        foreach (string password in passwords)
        {
            Console.WriteLine(password);
        }
    }
    else    {
    Console.WriteLine("No saved passwords found.");
    }
    Console.WriteLine("Press any key to return to the main menu...");
    Console.ReadKey();
}