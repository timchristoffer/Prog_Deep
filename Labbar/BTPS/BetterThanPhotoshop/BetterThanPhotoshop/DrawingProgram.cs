using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class DrawingProgram
{
    private SpriteBatch _spriteBatch; // SpriteBatch för att rita former.
    private List<Shape> _shapes; // Lista med ritade former.
    private DrawingArea _drawingArea; // Referens till ritningsområdet i spelet.

    // Enum för olika typer av former som kan ritas.
    public enum ShapeType
    {
        Circle,
        Square,
        Triangle
    }

    public DrawingProgram(SpriteBatch spriteBatch, DrawingArea drawingArea)
    {
        _spriteBatch = spriteBatch; // Sparar referens till SpriteBatch.
        _shapes = new List<Shape>(); // Skapar en ny lista med former.
        _drawingArea = drawingArea; // Sparar referens till ritningsområdet.
    }

    // Metod för att ändra färgen på formerna som ritas.
    public void ChangeColor(Color color)
    {
        _drawingArea.ChangeColor(color); // Anropar metoden för att ändra färgen i ritningsområdet.
    }

    // Metod för att uppdatera ritningsprogrammet.
    public void Update(GameTime gameTime, ShapeType currentShapeType, Color currentColor, Rectangle drawingAreaBounds)
    {
        MouseState mouseState = Mouse.GetState(); // Hämtar musens aktuella tillstånd.
        Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y); // Hämtar musens aktuella position.

        // Kontrollerar om vänster musknapp är nedtryckt och musen är inom ritningsområdets gränser.
        if (mouseState.LeftButton == ButtonState.Pressed && drawingAreaBounds.Contains(mousePosition))
        {
            Shape newShape = null; // Skapar en variabel för den nya formen som ska ritas.
            switch (currentShapeType)
            {
                // Skapar en ny cirkel med musens position som centrum och en given radie.
                case ShapeType.Circle:
                    newShape = new Circle(mousePosition, 50); 
                    break;
                // Skapar en ny kvadrat med musens position som övre vänstra hörn och en given sidolängd.
                case ShapeType.Square:
                    newShape = new Square(mousePosition, 60); 
                    break;
                // Skapar en ny triangel med musens position som ena hörnet och en given sidolängd.
                case ShapeType.Triangle:
                    newShape = new Triangle(mousePosition, 60); 
                    break;
            }

            // Lägger till den nya formen i listan med ritade former om den är giltig.
            if (newShape != null)
            {
                newShape.Color = currentColor; // Använder den aktuella färgen för den nya formen.
                _shapes.Add(newShape); // Lägger till den nya formen i listan.
            }
        }
    }

    // Metod för att rita de ritade formerna på skärmen.
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var shape in _shapes)
        {
            shape.Draw(spriteBatch); // Ritar varje form i listan.
        }
    }

    // Metod för att ångra den senaste ritade formen.
    public void Undo()
    {
        if (_shapes.Count > 0)
        {
            _shapes.RemoveAt(_shapes.Count - 1); // Tar bort den sista ritade formen från listan.
        }
    }

    // Metod för att rensa alla ritade former från ritningsprogrammet.
    public void Clear()
    {
        _shapes.Clear(); // Rensar alla former från listan.
    }
}
