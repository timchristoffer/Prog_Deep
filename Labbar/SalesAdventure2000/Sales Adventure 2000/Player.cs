// Player.cs
using System;
using System.Collections.Generic;

public class Player : Creature
{
    public string Name { get; set; }
    public List<Item> Inventory { get; set; }

    public Player(string name, int health, int strength, int endurance, char symbol) : base(health, strength, endurance, symbol)
    {
        Name = name;
        Health = health;
        Strength = strength;
        Endurance = endurance;
        Inventory = new List<Item>();
    }

    public void PickUpItem(Item item)
    {
        Inventory.Add(item);
    }

    public void UseItem(Item item)
    {
        if (item.Health > 0)
        {
            Health += item.Health;
            Console.WriteLine($"Player's Health increased by {item.Health}.");
        }
        if (item.Strength > 0)
        {
            Strength += item.Strength;
            Console.WriteLine($"Player's Strength increased by {item.Strength}.");
        }
        if (item.Endurance > 0)
        {
            Endurance += item.Endurance;
            Console.WriteLine($"Player's Endurance increased by {item.Endurance}.");
        }

        Console.WriteLine($"Player used {item.Name}.");

        if (item.Consumable)
        {
            Inventory.Remove(item);
        }
    }

    public override int Attack(Creature target)
    {
        int attackPower = random.Next(Strength / 2, Strength + 1);

        if (random.Next(1, 5) == 3)
        {
            Console.WriteLine("Player's attack missed!");
        }
        else
        {
            Console.WriteLine($"Player attacks with {attackPower} damage!");
            target.Health -= attackPower;
        }

        return attackPower;
    }

    public void Block()
    {
        Console.WriteLine("Player blocks!");
    }

    public void Run()
    {
        Console.WriteLine("Player runs away!");
    }
}
