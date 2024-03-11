using System;

class Program
{
    static void Main(string[] args)
    {
        string password = "Exempel123!"; // Byt ut detta mot det lösenord som du vill validera

        bool isValid = PasswordValidator.ValidatePassword(password);
        Console.WriteLine($"Lösenordet \"{password}\" är giltigt: {isValid}");
    }
}

