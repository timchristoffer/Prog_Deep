using System;
using System.Collections.Generic;

class Game
{
    private string[,] startGrid = new string[10, 10];
    private Random random = new Random();
    private int[] playerPosition = new int[2];
    private Player player;
    private List<Monster> monsters = new List<Monster>();
    private bool playerTurn = true;
    private string playerName;
    private List<Item> items = new List<Item>();
    private bool showInventory = false;
    private int monsterKillCount = 0; // Monster kill counter

    private void FillStartGridWithDefault()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                startGrid[i, j] = ".";
            }
        }
    }

    public void Initialize()
    {
        FillStartGridWithDefault();
        GenerateMonstersAndItems();
        playerPosition[0] = random.Next(0, 10);
        playerPosition[1] = random.Next(0, 10);
        startGrid[playerPosition[0], playerPosition[1]] = "P";
        player = new Player(playerName, 100, 10, 10, 'P');
        Console.CursorVisible = false;
    }

    private void GenerateMonstersAndItems()
    {
        // Generate monsters
        for (int i = 0; i < 5; i++)
        {
            int x = random.Next(0, 10);
            int y = random.Next(0, 10);
            monsters.Add(new Monster(random.Next(50, 150), random.Next(8, 15), random.Next(10, 15)) { PositionX = x, PositionY = y });
            startGrid[x, y] = "M";
        }

        // Generate items
        List<Item> generatedItems = Item.GenerateItems(8, random);

        foreach (var item in generatedItems)
        {
            items.Add(item);
            startGrid[item.X, item.Y] = item.Symbol.ToString();
        }
    }

    public void StartMenu()
    {
        Draw.DrawStartMenu();
        Console.Write("Enter your name:");
        playerName = Console.ReadLine();
        Console.WriteLine($"[Hello, {playerName}!]");
        Console.WriteLine("____________________________");
        Console.WriteLine("[1.] Start Game");
        Console.WriteLine("[2.] Quit Game");
        Console.WriteLine("____________________________");

        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(true);
        } while (keyInfo.KeyChar != '1' && keyInfo.KeyChar != '2');

        if (keyInfo.KeyChar == '1')
        {
            Console.Clear();
            Initialize();
            Start();
        }
        else if (keyInfo.KeyChar == '2')
        {
            Environment.Exit(0);
        }
    }

    public void Start()
    {
        while (true)
        {
            DrawWorld();
            if (playerTurn)
                HandlePlayerInput();
            else
                HandleMonsterTurn();
        }
    }

    private void DrawWorld()
    {
        Console.Clear();
        Console.WriteLine("  0 1 2 3 4 5 6 7 8 9");
        for (int i = 0; i < 10; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < 10; j++)
            {
                if (startGrid[i, j] == "M")
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Sätt färgen till röd för monster
                    Console.Write(startGrid[i, j] + " ");
                    Console.ResetColor();
                }
                else if (startGrid[i, j] == "P")
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Sätt färgen till grön för spelare
                    Console.Write(startGrid[i, j] + " ");
                    Console.ResetColor();
                }
                else if (startGrid[i, j] == "¤")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Sätt färgen till gul för föremål
                    Console.Write(startGrid[i, j] + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(startGrid[i, j] + " ");
                }
            }
            if (i == 0)
            {
                Console.Write($"  [Player Name: {player.Name}]");
            }
            else if (i == 1)
            {
                Console.Write($"  [ Health: {player.Health} ]");
            }
            else if (i == 2)
            {
                Console.Write($"  [ Strength: {player.Strength} ]");
            }
            else if (i == 3)
            {
                Console.Write($"  [ Endurance: {player.Endurance} ]");
            }
            else if (i == 4)
            {
                Console.Write($"  [ Player Kills: {monsterKillCount}/10 ]"); // Visa Monster Counter
            }
            else if (i == 5)
            {
                if (showInventory)
                {
                    Console.Write("  [INVENTORY] ");
                    int itemIndex = 1;
                    foreach (var item in player.Inventory)
                    {
                        Console.Write($"{itemIndex++}. {item.Name}, ");
                    }
                }
            }
            Console.WriteLine();
        }
    }

    private void HandlePlayerInput()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        int newX = playerPosition[0];
        int newY = playerPosition[1];

        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                newX--;
                break;
            case ConsoleKey.DownArrow:
                newX++;
                break;
            case ConsoleKey.LeftArrow:
                newY--;
                break;
            case ConsoleKey.RightArrow:
                newY++;
                break;
            case ConsoleKey.I:
                OpenInventory();
                break;
            default:
                break;
        }

        if (newX >= 0 && newX < 10 && newY >= 0 && newY < 10)
        {
            if (startGrid[newX, newY] == "¤")
            {
                ShowItemMenu(newX, newY);
            }
            else if (startGrid[newX, newY] == "M")
            {
                Monster monster = FindMonsterAtPosition(newX, newY);
                if (monster != null)
                {
                    HandleMonsterEncounter(newX, newY, monster);
                }
            }
            else
            {
                MovePlayer(newX, newY);
            }
        }
    }

    private void MovePlayer(int x, int y)
    {
        startGrid[playerPosition[0], playerPosition[1]] = ".";
        playerPosition[0] = x;
        playerPosition[1] = y;
        startGrid[playerPosition[0], playerPosition[1]] = "P";
    }

    private void ShowItemMenu(int x, int y)
    {
        Item item = items.Find(i => i.PositionX == x && i.PositionY == y);

        Console.WriteLine($"[You found an item: {item.Name}]");
        Console.WriteLine("_________________________________");
        Console.WriteLine("[1.] Use item");
        Console.WriteLine("[2.] Add item to inventory");
        Console.WriteLine("_________________________________");

        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(true);
        } while (keyInfo.KeyChar != '1' && keyInfo.KeyChar != '2');

        if (keyInfo.KeyChar == '1')
        {
            player.UseItem(item);
            startGrid[x, y] = ".";
        }
        else if (keyInfo.KeyChar == '2')
        {
            player.PickUpItem(item);
            Console.WriteLine($"[Item {item.Name} added to inventory]");
            startGrid[x, y] = ".";
        }
    }

    private void OpenInventory()
    {
        DrawWorld();
        Console.WriteLine("[ INVENTORY ]");
        for (int i = 0; i < player.Inventory.Count; i++)
        {
            Console.WriteLine($"[{i + 1}.] {player.Inventory[i].Name}");
        }
        Console.WriteLine("Select an item to use (enter item number) or press any key to close inventory...");

        if (int.TryParse(Console.ReadLine(), out int selectedItemIndex))
        {
            if (selectedItemIndex > 0 && selectedItemIndex <= player.Inventory.Count)
            {
                Item selectedItem = player.Inventory[selectedItemIndex - 1];
                Console.WriteLine($"You selected {selectedItem.Name}.");
                selectedItem.Use(player);
                Console.WriteLine($"You used {selectedItem.Name}");

                // Kontrollera om föremålet fortfarande finns kvar i inventoryn
                if (player.Inventory.Contains(selectedItem))
                {
                    player.Inventory.Remove(selectedItem);
                }
                else
                {
                    Console.WriteLine("The selected item is no longer in your inventory.");
                }
            }
            else
            {
                Console.WriteLine("Invalid item number. Please try again.");
            }
        }
    }


    private void HandleMonsterEncounter(int x, int y, Monster monster)
    {
        Console.WriteLine("You encountered a monster!");
        Console.WriteLine($"Monster Health: {monster.Health}");

        bool encounterOver = false;

        Console.WriteLine("Do you want to fight or run?");
        Console.WriteLine("1. Fight");
        Console.WriteLine("2. Run");

        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(true);
        } while (keyInfo.KeyChar != '1' && keyInfo.KeyChar != '2');

        switch (keyInfo.KeyChar)
        {
            case '1':
                while (!encounterOver)
                {
                    Console.WriteLine("1. Attack");
                    Console.WriteLine("2. Block");

                    do
                    {
                        keyInfo = Console.ReadKey(true);
                    } while (keyInfo.KeyChar != '1' && keyInfo.KeyChar != '2');

                    switch (keyInfo.KeyChar)
                    {
                        case '1':
                            int playerDamage = player.Attack(monster);
                            Console.WriteLine($"You attacked the monster and dealt {playerDamage} damage!");
                            monster.Health -= playerDamage;
                            if (monster.Health <= 0)
                            {
                                Console.WriteLine("You defeated the monster!");
                                encounterOver = true;
                                RemoveMonsterFromGrid(monster);
                                monsterKillCount++; // Increment monster kill counter
                                if (monsterKillCount == 10) // Check if the player has killed 10 monsters
                                {
                                    DisplayWinMenu(); // Display win menu if player has killed 10 monsters
                                    return;
                                }
                                SpawnNewMonster(); // Spawn new monster after defeating the current one
                            }
                            else
                            {
                                Console.WriteLine($"Monster Health: {monster.Health}");
                                int monsterDamage = monster.Attack(player);
                                Console.WriteLine($"The monster attacked you and dealt {monsterDamage} damage!");
                                player.Health -= monsterDamage;
                                Console.WriteLine($"Player Health: {player.Health}");
                                if (player.Health <= 0)
                                {
                                    Console.WriteLine("The monster defeated you!");
                                    encounterOver = true;
                                    GameOver();
                                }
                            }
                            break;
                        case '2':
                            Console.WriteLine("You chose to block.");
                            // Implement block logic
                            break;
                    }
                }
                break;
            case '2':
                Console.WriteLine("You chose to run away from the monster.");
                // Implement run logic
                break;
        }

        playerTurn = true;
    }

    private void RemoveMonsterFromGrid(Monster monster)
    {
        startGrid[monster.PositionX, monster.PositionY] = ".";
        monsters.Remove(monster);
    }

    private void SpawnNewMonster()
    {
        int x = random.Next(0, 10);
        int y = random.Next(0, 10);
        monsters.Add(new Monster(random.Next(50, 150), random.Next(8, 15), random.Next(10, 15)) { PositionX = x, PositionY = y });
        startGrid[x, y] = "M";
    }

    private void DisplayWinMenu()
    {
        Console.Clear();
        Draw.DrawYouWon();
        Console.WriteLine("1. Play again");
        Console.WriteLine("2. Quit game");

        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(true);
        } while (keyInfo.KeyChar != '1' && keyInfo.KeyChar != '2');

        if (keyInfo.KeyChar == '1')
        {
            Initialize();
            Start();
        }
        else if (keyInfo.KeyChar == '2')
        {
            Environment.Exit(0);
        }
    }

    private void GameOver()
    {
        monsterKillCount = 0; // Återställ monster kill counter till 0
        Console.Clear();
        Draw.DrawGameOver();
        Console.WriteLine("1. Restart game");
        Console.WriteLine("2. Quit game");

        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(true);
        } while (keyInfo.KeyChar != '1' && keyInfo.KeyChar != '2');

        if (keyInfo.KeyChar == '1')
        {
            Initialize();
            Start();
        }
        else if (keyInfo.KeyChar == '2')
        {
            Environment.Exit(0);
        }
    }

    private void HandleMonsterTurn()
    {
        int index = random.Next(0, monsters.Count);
        Monster monster = monsters[index];
        monster.Attack(player);
        playerTurn = true;
    }

    private Monster FindMonsterAtPosition(int x, int y)
    {
        foreach (Monster monster in monsters)
        {
            if (monster.PositionX == x && monster.PositionY == y)
            {
                return monster;
            }
        }
        return null;
    }
}
