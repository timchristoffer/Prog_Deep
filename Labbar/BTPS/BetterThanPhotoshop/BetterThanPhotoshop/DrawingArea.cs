using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class DrawingArea
{
    private List<Shape> _shapes; // Lista med ritade former
    private Color _currentColor; // Aktuell färg för att rita former

    // Egenskap för att få tillgång till den aktuella färgen utan möjlighet att ändra den utifrån.
    public Color CurrentColor => _currentColor;

    // Egenskaper för att få tillgång till ritningsområdets bredd, höjd och textur.
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Texture2D Texture { get; private set; }

    // Egenskap för att returnera en rektangel som representerar ritningsområdets gränser.
    public Rectangle Bounds => new Rectangle(0, 0, Width, Height);

    // Konstruktor för att skapa en ny instans av DrawingArea med angiven bredd, höjd och textur.
    public DrawingArea(int width, int height, Texture2D texture)
    {
        _shapes = new List<Shape>(); // Skapar en ny lista med ritade former.
        _currentColor = Color.Black; // Anger den initiala färgen till svart.
        Width = width; // Sätter ritningsområdets bredd.
        Height = height; // Sätter ritningsområdets höjd.
        Texture = texture; // Sätter ritningsområdets textur.
    }

    // Metod för att rita ritningsområdet och alla ritade former.
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var shape in _shapes)
        {
            shape.Draw(spriteBatch); // Ritar varje form i listan med ritade former.
        }
    }

    // Metod för att lägga till en ny ritad form i ritningsområdet.
    public void AddShape(Shape shape)
    {
        shape.Color = _currentColor; // Anger formens färg till den aktuella färgen.
        _shapes.Add(shape); // Lägg till den nya formen i listan med ritade former.
    }

    // Metod för att rensa alla ritade former från ritningsområdet.
    public void Clear()
    {
        _shapes.Clear(); // Rensar alla former från listan med ritade former.
    }

    // Metod för att ångra den senaste ritade formen.
    public void Undo()
    {
        if (_shapes.Count > 0)
        {
            _shapes.RemoveAt(_shapes.Count - 1); // Tar bort den sista ritade formen från listan med ritade former.
        }
    }

    // Metod för att ändra den aktuella färgen i ritningsområdet.
    public void ChangeColor(Color color)
    {
        _currentColor = color; // Sätter den nya färgen som den aktuella färgen för att rita former.
    }
}
