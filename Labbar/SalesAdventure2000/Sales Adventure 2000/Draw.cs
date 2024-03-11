using System;

public class Draw
{
    public static void DrawStartMenu()
    {
        Console.Clear();
        Console.WriteLine("  __        __   _                             ");
        Console.WriteLine("  \\ \\      / /__| | ___ ___  _ __ ___   ___  ");
        Console.WriteLine("   \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ ");
        Console.WriteLine("    \\ V  V /  __/ | (_| (_) | | | | | |  __/  ");
        Console.WriteLine("     \\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|");
        Console.WriteLine();
    }


    public static void DrawGameOver()
    {
        Console.Clear();
        Console.WriteLine("   _____                         ____                 _ ");
        Console.WriteLine("  / ____|                       / __ \\               | |");
        Console.WriteLine(" | |  __  __ _ _ __ ___   ___  | |  | |_   _____ _ __| |");
        Console.WriteLine(" | | |_ |/ _` | '_ ` _ \\ / _ \\ | |  | \\ \\ / / _ \\ '__| |");
        Console.WriteLine(" | |__| | (_| | | | | | |  __/ | |__| |\\ V /  __/ |  |_|");
        Console.WriteLine("  \\_____|\\__,_|_| |_| |_|\\___|  \\____/  \\_/ \\___|_|  (_)");
        Console.WriteLine("\n\nPress any key to continue...");
        Console.ReadKey();
    }

    public static void DrawYouWon()
    {
        Console.Clear();
        Console.WriteLine("  __   __            _    _               _ ");
        Console.WriteLine("  \\ \\ / /           | |  | |             | |");
        Console.WriteLine("   \\ V /___  _   _  | |  | | ___  _ __ __| |");
        Console.WriteLine("    \\ // _ \\| | | | | |/\\| |/ _ \\| '__/ _` |");
        Console.WriteLine("    | | (_) | |_| | \\  /\\  / (_) | | | (_| |");
        Console.WriteLine("    \\_/\\___/ \\__,_|  \\/  \\/ \\___/|_|  \\__,_|");
        Console.WriteLine("\n\nPress any key to continue...");
        Console.ReadKey();
    }
}