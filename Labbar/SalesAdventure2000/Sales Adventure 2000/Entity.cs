public abstract class Entity
{
    protected int health;
    protected int strength;
    protected int endurance;
    public char Symbol { get; protected set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Entity(int health, int strength, int endurance, char symbol, int x, int y)
    {
        this.health = health;
        this.strength = strength;
        this.endurance = endurance;
        Symbol = symbol;
        X = x;
        Y = y;
    }

    public Entity(int health, int strength, int endurance)
    {
        this.health = health;
        this.strength = strength;
        this.endurance = endurance;
    }
}
