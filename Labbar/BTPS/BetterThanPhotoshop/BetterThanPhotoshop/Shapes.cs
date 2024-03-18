using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

public abstract class Shape
{
    public Color Color { get; set; }
    public Vector2 Position { get; set; }

    // Abstrakt metod för att rita formen.
    public abstract void Draw(SpriteBatch spriteBatch);
}

// Cirkelformklass som ärver från Shape.
public class Circle : Shape
{
    private float _radius; // Radien för cirkeln.

    // Konstruktor för cirkelformen.
    public Circle(Vector2 position, float radius)
    {
        Position = position; // Anger position.
        _radius = radius; // Anger radie.
    }

    // Implementerar metoden för att rita cirkelformen.
    public override void Draw(SpriteBatch spriteBatch)
    {
        // Skapar en cirkel med centrum vid angiven position och specificerad radie.
        var circle = new CircleF(Position, _radius);
        // Ritar cirkeln med specificerad färg.
        spriteBatch.DrawCircle(circle, 20, Color);
    }
}

// Kvadratformklass som ärver från Shape.
public class Square : Shape
{
    private int _sideLength; // Sidolängden för kvadraten.

    // Konstruktor för kvadratformen.
    public Square(Vector2 position, int sideLength)
    {
        Position = position; // Anger position.
        _sideLength = sideLength; // Anger sidolängd.
    }

    // Implementerar metoden för att rita kvadratformen.
    public override void Draw(SpriteBatch spriteBatch)
    {
        // Ritar en rektangel med angiven position och sidolängd.
        spriteBatch.DrawRectangle(new Rectangle((int)Position.X, (int)Position.Y, _sideLength, _sideLength), Color);
    }
}

// Triangelklass som ärver från Shape.
public class Triangle : Shape
{
    private int _sideLength; // Sidolängden för triangeln.

    // Konstruktor för triangelklassen.
    public Triangle(Vector2 position, int sideLength)
    {
        Position = position; // Anger position.
        _sideLength = sideLength; // Anger sidolängd.
    }

    // Implementerar metoden för att rita triangeln.
    public override void Draw(SpriteBatch spriteBatch)
    {
        // Beräknar de tre hörnpositionerna för en liksidig triangel.
        Vector2 topVertex = new Vector2(Position.X, Position.Y - (_sideLength * (float)Math.Sqrt(3) / 2));
        Vector2 leftVertex = new Vector2(Position.X - (_sideLength / 2), Position.Y + (_sideLength / 2) * (float)Math.Sqrt(3));
        Vector2 rightVertex = new Vector2(Position.X + (_sideLength / 2), Position.Y + (_sideLength / 2) * (float)Math.Sqrt(3));

        // Ritar triangeln genom att dra linjer mellan hörnpositionerna.
        spriteBatch.DrawLine(topVertex, leftVertex, Color);
        spriteBatch.DrawLine(leftVertex, rightVertex, Color);
        spriteBatch.DrawLine(rightVertex, topVertex, Color);
    }
}
