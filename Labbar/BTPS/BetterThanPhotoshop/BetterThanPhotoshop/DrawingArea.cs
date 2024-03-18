using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class DrawingArea
{
    private List<Shape> _shapes;
    private Color _currentColor;
    public Color CurrentColor => _currentColor;


    public int Width { get; private set; } // Bredden på ritningsområdet
    public int Height { get; private set; } // Höjden på ritningsområdet
    public Texture2D Texture { get; private set; } // Texturen för ritningsområdet
    public Rectangle Bounds => new Rectangle(0, 0, Width, Height); // Egenskap för att returnera områdets gränser

    public DrawingArea(int width, int height, Texture2D texture)
    {
        _shapes = new List<Shape>();
        _currentColor = Color.Black;
        Width = width;
        Height = height;
        Texture = texture;
    }

    public void Update(GameTime gameTime)
    {
        // Uppdatera logik för ritningsområdet, om det behövs
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var shape in _shapes)
        {
            shape.Draw(spriteBatch);
        }
    }

    public void AddShape(Shape shape)
    {
        shape.Color = _currentColor; // Ange formens färg
        _shapes.Add(shape);
    }

    public void Clear()
    {
        _shapes.Clear(); // Rensa alla former från ritningsområdet
    }

    public void Undo()
    {
        if (_shapes.Count > 0)
        {
            _shapes.RemoveAt(_shapes.Count - 1); // Ångra den senaste formen som ritats
        }
    }

    // Metod för att ändra den aktuella färgen i ritningsområdet
    public void ChangeColor(Color color)
    {
        Console.WriteLine("New color selected: " + color); // Lägg till denna rad för felsökning
        _currentColor = color;
    }
}