using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class DrawingProgram
{
    private SpriteBatch _spriteBatch;
    private List<Shape> _shapes;
    private DrawingArea _drawingArea;

    public enum ShapeType
    {
        Circle,
        Square,
        Triangle
    }

    public DrawingProgram(SpriteBatch spriteBatch, DrawingArea drawingArea)
    {
        _spriteBatch = spriteBatch;
        _shapes = new List<Shape>();
        _drawingArea = drawingArea;
    }

    public void ChangeColor(Color color)
    {
        _drawingArea.ChangeColor(color); // Ändring här för att skicka den valda färgen till ritningsområdet
    }

    public void Update(GameTime gameTime, ShapeType currentShapeType, Color currentColor, Rectangle drawingAreaBounds)
    {
        MouseState mouseState = Mouse.GetState();
        Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

        if (mouseState.LeftButton == ButtonState.Pressed && drawingAreaBounds.Contains(mousePosition))
        {
            Shape newShape = null;
            switch (currentShapeType)
            {
                case ShapeType.Circle:
                    newShape = new Circle(mousePosition, 50); // Exempelradie
                    break;
                case ShapeType.Square:
                    newShape = new Square(mousePosition, 50); // Exempelsidolängd
                    break;
                case ShapeType.Triangle:
                    newShape = new Triangle(mousePosition, 50); // Exempelsidolängd
                    break;
            }

            if (newShape != null)
            {
                newShape.Color = currentColor; // Använd den valda färgen
                _shapes.Add(newShape);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var shape in _shapes)
        {
            shape.Draw(spriteBatch);
        }
    }

    public void Undo()
    {
        if (_shapes.Count > 0)
        {
            _shapes.RemoveAt(_shapes.Count - 1); // Remove the last added shape
        }
    }

    public void Clear()
    {
        _shapes.Clear(); // Clear all shapes from the drawing area
    }
}
