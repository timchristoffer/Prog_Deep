using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rep_Uppgift_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Skapar en slumpgenerator.
            Random random = new Random();

            // Simulerar tärningskast.
            int dice1 = random.Next(1, 7);
            int dice2 = random.Next(1, 7);
            int dice3 = random.Next(1, 7);

            // Kontrollerar resultatet och skriver ut det. 
            if (dice1 == dice2 && dice2 == dice3 )
            {
                Console.WriteLine("Två " + dice1 + "or");
            }
            else if (dice1 != dice2)
            {
                Console.WriteLine("En " + dice1 + "a och en " + dice2 + "a");
            }

            Console.ReadLine();
        }
    }
}
