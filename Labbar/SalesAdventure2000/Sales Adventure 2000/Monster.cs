
using System.Collections.Generic;
using System;

class Monster : Creature
{
    public int PositionX { get; set; } // X-koordinat för monsterposition
    public int PositionY { get; set; } // Y-koordinat för monsterposition

    public Monster(int health, int strength, int endurance) : base(health, strength, endurance, 'M')
    {
        Health = health;
        Strength = strength;
        Endurance = endurance;
    }

    // En statisk metod för att skapa olika monsterinstanser
    public static List<Monster> CreateMonsters()
    {
        List<Monster> monsters = new List<Monster>();

        // Skapa olika monsterinstanser och lägg till dem i listan
        monsters.Add(new Monster(100, 10, 15)); // Monster typ M med 100 hälsa
        monsters.Add(new Monster(150, 15, 20)); // Monster typ M med 150 hälsa
        monsters.Add(new Monster(200, 20, 25)); // Monster typ M med 200 hälsa

        return monsters;
    }

    public override int Attack(Creature target)
    {
        int attackPower = random.Next(Strength / 2, Strength + 1);

        if (random.Next(1, 5) == 4)
        {
            Console.WriteLine("Monster's attack missed!");
        }
        else
        {
            Console.WriteLine($"Monster attacks with {attackPower} damage!");
            target.Health -= attackPower; // Minimize target's health
        }

        return attackPower; // Return attack power
    }
}