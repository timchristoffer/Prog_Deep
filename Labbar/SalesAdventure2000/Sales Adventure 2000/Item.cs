using System;
using System.Collections.Generic;

public class Item : Entity
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Endurance { get; set; }
    public bool Consumable { get; set; }

    // Lägg till PositionX och PositionY egenskaper
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public Item(string name, int health, int strength, int endurance, bool consumable, char symbol, int x, int y)
        : base(health, strength, endurance, symbol, x, y)
    {
        Name = name;
        Health = health;
        Strength = strength;
        Endurance = endurance;
        Consumable = consumable;

        // Tilldela PositionX och PositionY värden
        PositionX = x;
        PositionY = y;
    }

    public static List<Item> GenerateItems(int itemCount, Random random)
    {
        List<Item> items = new List<Item>();

        for (int i = 0; i < itemCount; i++)
        {
            int x = random.Next(0, 10);
            int y = random.Next(0, 10);

            Item newItem;
            int itemType = random.Next(1, 101);

            if (itemType <= 5)
            {
                newItem = new Item("Rainbow Sword", 0, 20, 0, false, '¤', x, y);
            }
            else if (itemType <= 25)
            {
                newItem = new Item("Golden Pineapple", 100, 0, 0, true, '¤', x, y);
            }
            else if (itemType <= 45)
            {
                newItem = new Item("Rainbow Shield", 50, 0, 30, true, '¤', x, y);
            }
            else if (itemType <= 65)
            {
                newItem = new Item("Just Bread", 50, 0, 0, true, '¤', x, y);
            }
            else
            {
                newItem = new Item("Wooden Stick", 0, 2, 0, false, '¤', x, y);
            }

            Console.WriteLine($"Generated item: {newItem.Name}, Health: {newItem.Health}, Strength: {newItem.Strength}, Endurance: {newItem.Endurance}");

            items.Add(newItem);
        }

        return items;
    }

    public void Use(Player player)
    {
        if (Health > 0)
            player.Health += Health;
        if (Strength > 0)
            player.Strength += Strength;
        if (Endurance > 0)
            player.Endurance += Endurance;

        Console.WriteLine($"Player used {Name}.");

        if (Consumable)
        {
            player.Inventory.Remove(this);
        }
    }
}
