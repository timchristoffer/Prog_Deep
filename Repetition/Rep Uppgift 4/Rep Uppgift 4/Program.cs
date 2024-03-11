using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rep_Uppgift_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ange nytt lösenord: ");
            Console.WriteLine(IsValidPassword(Console.ReadLine()));

        }
        static bool IsValidPassword(string password)
        {
            // Validera längd
            if (password.Length < 8)
                return false;

            // Validera minst 2 stora bokstäver, 2 små bokstäver och ett specialtecken
            if (!Regex.IsMatch(password, "(.*[A-Z]){2,}") || !Regex.IsMatch(password, "(.*[a-z]){2,}") || !Regex.IsMatch(password, "(.*[^A-Za-z0-9]){1,}"))
                return false;

            // Undvik att lösenordet innehåller förbjudna delar (case insensitive)
            string[] forbiddenParts = { "hej", "lösen", "hopp" };
            foreach (var part in forbiddenParts)
            {
                if (Regex.IsMatch(password, part, RegexOptions.IgnoreCase))
                    return false;
            }

            // Alla valideringar passerade, lösenordet är giltigt
            return true;
        }

    }
}
