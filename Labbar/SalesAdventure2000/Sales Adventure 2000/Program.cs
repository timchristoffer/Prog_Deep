using System;

class Program
{
    static void Main(string[] args)
    {
        // Instansiera spelet
        Game game = new Game();

        // Visa startmeny och vänta på spelarens val
        game.StartMenu();

        // Initiera spelet efter att spelaren har valt att starta det
        game.Initialize();

        // Starta spelet
        game.Start();
    }
}
