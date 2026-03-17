while (true)
{
    // Display menu
    Console.Clear();
    Console.WriteLine("Welcome to the Password Manager");
    Console.WriteLine("Select an option:");
    Console.WriteLine("1. Generate a password");
    Console.WriteLine("2. Show saved passwords");
    Console.WriteLine("3. Exit");
    
    // Read user input
    string? optionInput = Console.ReadLine();
    if (!int.TryParse(optionInput, out int option))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        continue;
    }

    switch (option)
    {
        case 1:
            GeneratePasswordOption();
            break;
        case 2:
            ShowSavedPasswordsOptionWithSearch();
            break;
        case 3:
            Console.WriteLine("Goodbye! Press any key to exit...");
            Console.ReadKey();
            return;
        default:
            Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            break;
    }
}

// Function to generate a password
void GeneratePasswordOption()
{
    // Define the characters to be used in the password
    string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";

    // Read password length from user
    Console.WriteLine("Enter password length:");
    string? input = Console.ReadLine();
    if (!int.TryParse(input, out int length) || length <= 0)
    {
        Console.WriteLine("Invalid input. Please enter a positive number.");
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
        return;
    }

    // Generate the password
    string password = GeneratePassword(length, characters);
    Console.WriteLine($"Generated password: {password}");
    
    static string GeneratePassword(int length, string characters)
    {
        Random random = new Random();
        string password = string.Empty;

        for (int i = 0; i < length; i++)
        {
            int randomIndex = random.Next(characters.Length);
            password += characters[randomIndex];
        }

        return password;
    }

    // Ask user if they want to save the password
    Console.WriteLine("Do you want to save this password? (y/n)");
    string? saveOption = Console.ReadLine();
    if ((saveOption ?? string.Empty).Equals("y", StringComparison.CurrentCultureIgnoreCase))
    {
        Console.WriteLine("Enter a site for this password:");
        string site = Console.ReadLine() ?? string.Empty;
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

// Function to show saved passwords with search functionality
void ShowSavedPasswordsOptionWithSearch()
{
    // Search functionality
    Console.WriteLine("Do you want to search for a specific password? (y/n)");
    string? searchOption = Console.ReadLine();
    if ((searchOption ?? string.Empty).Equals("y", StringComparison.CurrentCultureIgnoreCase))
    {
        Console.WriteLine("Enter the site name to search for:");
        string searchTerm = Console.ReadLine() ?? string.Empty;
        // Check if the passwords file exists
        if (!File.Exists("passwords.txt"))
        {
            Console.WriteLine("No saved passwords found.");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            return;
        }
        // Read all passwords and filter by the search term
        string[] savedPasswords = File.ReadAllLines("passwords.txt");
        var matchingPasswords = savedPasswords.Where(p => p.StartsWith(searchTerm + ":")).ToArray();
        // Display matching passwords
        if (matchingPasswords.Length == 0)
        {
            Console.WriteLine("No passwords found for the specified site.");
        }
        else
        {
            Console.WriteLine("Matching Passwords:");
            foreach (string password in matchingPasswords)
            {
                Console.WriteLine(password);
            }
        }
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
        return;
    }
}