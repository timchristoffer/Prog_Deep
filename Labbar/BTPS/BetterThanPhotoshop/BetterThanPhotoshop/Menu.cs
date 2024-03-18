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
        // En referens till ContentManager för att ladda innehåll som texturer.
        private ContentManager _content;
        // En referens till ritningsområdet, som kommer att användas för att spara ritningen som bild.
        private DrawingArea _drawingArea;

        // Egenskap för att returnera listan med knappar.
        public List<Button> Buttons => _buttons;

        private int _padding = 10;

        // Konstruktor för att skapa en ny instans av menyn.
        public Menu(int screenWidth, int screenHeight, ContentManager content, DrawingArea drawingArea)
        {
            _content = content;
            _buttons = new List<Button>();
            _drawingArea = drawingArea; // Sparar referensen till DrawingArea.

            // Lägger till knappar för olika former.
            AddShapeButton("CircleButton", new Vector2(screenWidth - 180, 20), "Circle");
            AddShapeButton("SquareButton", new Vector2(screenWidth - 180, 60), "Square");
            AddShapeButton("TriangleButton", new Vector2(screenWidth - 180, 100), "Triangle");

            // Lägger till knappar för Ångra och Rensa.
            AddButton("UndoButton", new Vector2(screenWidth - 180, 160), "Undo");
            AddButton("ClearButton", new Vector2(screenWidth - 180, 200), "Clear");

            // Lägger till knapp för Spara.
            AddButton("SaveButton", new Vector2(screenWidth - 180, 240), "Save");
        }

        // Metod för att lägga till en vanlig knapp i menyn.
        private void AddButton(string texturePath, Vector2 position, string name)
        {
            _buttons.Add(new Button(texturePath, position, _content) { Name = name });
        }

        // Metod för att lägga till en knapp för en form i menyn.
        private void AddShapeButton(string texturePath, Vector2 position, string tag)
        {
            _buttons.Add(new Button(texturePath, position, _content) { Tag = tag });
        }

        // Metod för att uppdatera menyn.
        public void Update()
        {
            foreach (var button in _buttons)
            {
                button.Update();
            }
        }

        // Metod för att rita menyn.
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        // Metod för att kontrollera om en knapp för en form är klickad.
        public bool IsShapeButtonClicked(out string shapeType)
        {
            foreach (var button in _buttons)
            {
                if (button.IsClicked && button.Tag != null)
                {
                    shapeType = button.Tag;
                    button.ResetClick(); // Återställer klickstatus för knappen efter bearbetning.
                    return true;
                }
            }
            shapeType = null;
            return false;
        }

        // Metod för att kontrollera om Ångra-knappen är klickad.
        public bool IsUndoButtonClicked()
        {
            foreach (var button in _buttons)
            {
                if (button.IsClicked && button.Name == "Undo")
                {
                    button.ResetClick(); // Återställer klickstatus för knappen efter bearbetning.
                    return true;
                }
            }
            return false;
        }

        // Metod för att kontrollera om Rensa-knappen är klickad.
        public bool IsClearButtonClicked()
        {
            foreach (var button in _buttons)
            {
                if (button.IsClicked && button.Name == "Clear")
                {
                    button.ResetClick(); // Återställer klickstatus för knappen efter bearbetning.
                    return true;
                }
            }
            return false;
        }

        // Metod för att kontrollera om Spara-knappen är klickad.
        public bool IsSaveButtonClicked()
        {
            foreach (var button in _buttons)
            {
                if (button.IsClicked && button.Name == "Save")
                {
                    button.ResetClick(); // Återställer klickstatus för knappen efter bearbetning.
                    SaveDrawing(_drawingArea.Texture); // Skickar ritningens textur som argument.
                    return true;
                }
            }
            return false;
        }

        // Metod för att spara ritningen som en bild.
        private void SaveDrawing(Texture2D drawingTexture)
        {
            string directoryPath = @"SavedPictures";
            string fileName = "Drawing.png";
            string filePath = Path.Combine(directoryPath, fileName);

            // Se till att katalogen finns
            Directory.CreateDirectory(directoryPath);

            // Spara ritningen
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                drawingTexture.SaveAsPng(stream, drawingTexture.Width, drawingTexture.Height);
            }
        }
        public void SetPosition(int screenWidth, int screenHeight)
        {
            // Flyttar menyn till höger om fönstret med en viss marginal.
            int xOffset = screenWidth - 220; // Justerar för att flytta menyn mer till höger.
            int y = 20; // Y-koordinat för första knappen.

            // Loopar igenom knapparna och uppdatera deras positioner.
            foreach (var button in _buttons)
            {
                button.Position = new Vector2(xOffset, y);
                y += button.Height + _padding; // Ökar Y-koordinaten för nästa knapp.
            }
        }
    }
}
