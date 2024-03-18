using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace BetterThanPhotoshop
{
    public class Menu
    {
        private List<Button> _buttons;
        private ContentManager _content;
        private DrawingArea _drawingArea;
        public List<Button> Buttons => _buttons; // Lägg till denna egenskap för att returnera listan med knappar

        public Menu(int screenWidth, int screenHeight, ContentManager content, DrawingArea drawingArea)
        {
            _content = content;
            _buttons = new List<Button>();
            _drawingArea = drawingArea; // Spara referensen till DrawingArea

            // Add buttons for different shapes
            AddShapeButton("CircleButton", new Vector2(screenWidth - 180, 20), "Circle");
            AddShapeButton("SquareButton", new Vector2(screenWidth - 180, 60), "Square");
            AddShapeButton("TriangleButton", new Vector2(screenWidth - 180, 100), "Triangle");

            // Add buttons for Undo and Clear
            AddButton("UndoButton", new Vector2(screenWidth - 180, 160), "Undo");
            AddButton("ClearButton", new Vector2(screenWidth - 180, 200), "Clear");

            //Add button for Save
            AddButton("SaveButton", new Vector2(screenWidth - 180, 240), "Save");
        }

        private void AddButton(string texturePath, Vector2 position, string name)
        {
            _buttons.Add(new Button(texturePath, position, _content) { Name = name });
        }

        private void AddShapeButton(string texturePath, Vector2 position, string tag)
        {
            _buttons.Add(new Button(texturePath, position, _content) { Tag = tag });
        }

        public void Update()
        {
            foreach (var button in _buttons)
            {
                button.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        public bool IsShapeButtonClicked(out string shapeType)
        {
            foreach (var button in _buttons)
            {
                if (button.IsClicked && button.Tag != null)
                {
                    shapeType = button.Tag;
                    button.ResetClick(); // Reset button click status after processing
                    return true;
                }
            }
            shapeType = null;
            return false;
        }

        public bool IsUndoButtonClicked()
        {
            foreach (var button in _buttons)
            {
                if (button.IsClicked && button.Name == "Undo")
                {
                    button.ResetClick(); // Reset button click status after processing
                    return true;
                }
            }
            return false;
        }

        public bool IsClearButtonClicked()
        {
            foreach (var button in _buttons)
            {
                if (button.IsClicked && button.Name == "Clear")
                {
                    button.ResetClick(); // Reset button click status after processing
                    return true;
                }
            }
            return false;
        }

        public bool IsSaveButtonClicked()
        {
            foreach (var button in _buttons)
            {
                if (button.IsClicked && button.Name == "Save")
                {
                    button.ResetClick(); // Återställ knappens klickstatus efter bearbetning
                    SaveDrawing(_drawingArea.Texture); // Pass the drawing texture as an argument
                    return true;
                }
            }
            return false;
        }

        private void SaveDrawing(Texture2D drawingTexture)
        {
            string directoryPath = @"SavedPictures";
            string fileName = "Drawing.png";
            string filePath = Path.Combine(directoryPath, fileName);

            // Ensure the directory exists
            Directory.CreateDirectory(directoryPath);

            // Save the drawing
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                drawingTexture.SaveAsPng(stream, drawingTexture.Width, drawingTexture.Height);
            }
        }
    }
}
