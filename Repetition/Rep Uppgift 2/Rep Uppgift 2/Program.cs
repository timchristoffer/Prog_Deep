using System;

class Program
{
    static void Main(string[] args)
    {
        // Skapar en slumpgenerator.
        Random random = new Random();

        // Simulera tärningskast.
        int dice1 = random.Next(1, 7); // Slumpar ett heltal mellan 1 och 6
        int dice2 = random.Next(1, 7);
        int dice3 = random.Next(1, 7);

        // Kontrollera resultaten och skriver ut det.
        if (dice1 == dice2 && dice2 == dice3)
        {
            Console.WriteLine("Tre " + dice1 + "or");
        }
        else if (dice1 == dice2 && dice2 != dice3)
        {
            Console.WriteLine("Två " + dice1 + "or och en " + dice3 + "a");
        }
        else if (dice1 == dice3 && dice1 != dice2)
        {
            Console.WriteLine("Två " + dice1 + "or och en " + dice2 + "a");
        }
        else if (dice2 == dice3 && dice1 != dice2)
        {
            Console.WriteLine("Två " + dice2 + "or och en " + dice1 + "a");
        }
        else
        {
            Console.WriteLine("En " + dice1 + "a, en " + dice2 + "a och en " + dice3 + "a");
        }

        Console.ReadLine();
    }
}

