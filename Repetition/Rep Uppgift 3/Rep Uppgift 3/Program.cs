using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rep_Uppgift_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mata in Regplåt: ");
            Console.WriteLine(IsValidRegistrationNumber(Console.ReadLine()));
        }

        static bool IsValidRegistrationNumber(string input)
        {
            // Regular expression för att matcha kraven på registreringsnummer
            Regex regex = new Regex(@"^[A-Z]{3}\d{3}$|^[A-Z]{3}\d{2}[A-Z]$");

            // Returnera true om strängen matchar mönstret, annars false
            return regex.IsMatch(input);
        }
    }
}
