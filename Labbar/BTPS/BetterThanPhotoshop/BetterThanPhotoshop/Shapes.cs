using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

public abstract class Shape
{
    public Color Color { get; set; }
    public Vector2 Position { get; set; }

    public abstract void Draw(SpriteBatch spriteBatch);
}

public class Circle : Shape
{
    private float _radius;

    public Circle(Vector2 position, float radius)
    {
        Position = position;
        _radius = radius;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var circle = new CircleF(Position, _radius);
        spriteBatch.DrawCircle(circle, 20, Color);
    }
}

public class Square : Shape
{
    private int _sideLength;

    public Square(Vector2 position, int sideLength)
    {
        Position = position;
        _sideLength = sideLength;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Implement code to draw a square with the specified position and side length
        spriteBatch.DrawRectangle(new Rectangle((int)Position.X, (int)Position.Y, _sideLength, _sideLength), Color);
    }
}

public class Triangle : Shape
{
    private int _sideLength;

    public Triangle(Vector2 position, int sideLength)
    {
        Position = position;
        _sideLength = sideLength;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Calculate the three vertices of the equilateral triangle
        Vector2 topVertex = new Vector2(Position.X, Position.Y - (_sideLength * (float)Math.Sqrt(3) / 2));
        Vector2 leftVertex = new Vector2(Position.X - (_sideLength / 2), Position.Y + (_sideLength / 2) * (float)Math.Sqrt(3));
        Vector2 rightVertex = new Vector2(Position.X + (_sideLength / 2), Position.Y + (_sideLength / 2) * (float)Math.Sqrt(3));

        // Draw the triangle using lines
        spriteBatch.DrawLine(topVertex, leftVertex, Color);
        spriteBatch.DrawLine(leftVertex, rightVertex, Color);
        spriteBatch.DrawLine(rightVertex, topVertex, Color);
    }
}