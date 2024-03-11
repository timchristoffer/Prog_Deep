using System;

public abstract class Creature : Entity
{
    public int Strength { get; set; }
    public int Health { get; set; }
    public int Endurance { get; set; }
    public new char Symbol { get; protected set; }
    protected static Random random = new Random(); // Flytta Random till en statisk variabel

    public Creature(int health, int strength, int endurance, char symbol) : base(health, strength, endurance)
    {
        Strength = strength;
        Health = health;
        Endurance = endurance;
        Symbol = symbol;
    }

    public Creature(int health, int strength, int endurance) : base(health, strength, endurance)
    {
    }

    public bool IsAlive
    {
        get { return Health > 0; }
    }

    public virtual int Attack(Creature creature)
    {
        int attackPower = random.Next(Strength / 2, Strength + 1);

        if (random.Next(1, 5) == 3)
        {
            return 0; // Miss
        }
        else
        {
            creature.Health -= attackPower;
            return attackPower; // Lyckad attack
        }
    }
}