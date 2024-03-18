using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace BetterThanPhotoshop
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private DrawingProgram _drawingProgram;
        private DrawingArea _drawingArea;
        private Menu _menu;
        private ColorMenu _colorMenu;
        private DrawingProgram.ShapeType _currentShapeType = DrawingProgram.ShapeType.Circle; // Default shape type

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Ställ in önskad upplösning
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            // Säkerställ att fönstret har denna storlek
            Window.AllowUserResizing = true;

            IsMouseVisible = true;
        }

        // Metod för att spara ritad bild som en PNG-fil
        public void SaveDrawingAsImage(RenderTarget2D renderTarget, string filePath)
        {
            Console.WriteLine("Saving drawing as image..."); // Felsökning
            using (Stream stream = File.Create(filePath))
            {
                renderTarget.SaveAsPng(stream, renderTarget.Width, renderTarget.Height);
            }
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Skapa en instans av DrawingArea
            _drawingArea = new DrawingArea(GraphicsDevice.Viewport.Width - 250, GraphicsDevice.Viewport.Height, Content.Load<Texture2D>("DrawingArea")); // Justera bredden på ritningsområdet

            // Skapa en instans av DrawingProgram och skicka med en instans av DrawingArea
            _drawingProgram = new DrawingProgram(_spriteBatch, _drawingArea);

            // Skapa en instans av Menu och skicka med texturerna, skärmens höjd och DrawingArea
            _menu = new Menu(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, Content, _drawingArea);

            // Skapa ett ColorMenu-objekt med referenser till Menu-knapparna
            _colorMenu = new ColorMenu(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, _menu.Buttons, Content);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            _menu.Update();
            _colorMenu.Update();
            Color? selectedColor = _colorMenu.GetSelectedColor();
            if (selectedColor.HasValue)
            {
                _drawingProgram.ChangeColor(selectedColor.Value); // Skicka den valda färgen till ritningsprogrammet
            }

            // Kolla om Save-knappen är klickad
            if (_menu.IsSaveButtonClicked())
            {
                // Skapa en render target för att rita det ritade området
                RenderTarget2D renderTarget = new RenderTarget2D(GraphicsDevice, _drawingArea.Width, _drawingArea.Height);

                // Rita det ritade området till render target
                GraphicsDevice.SetRenderTarget(renderTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _drawingArea.Draw(_spriteBatch);
                GraphicsDevice.SetRenderTarget(null);

                // Spara render target som bild
                SaveDrawingAsImage(renderTarget, "Drawing.png");

                // Radera render target efter användning
                renderTarget.Dispose();
            }

            // Check if a shape button is clicked and update the current shape type accordingly
            string shapeType;
            if (_menu.IsShapeButtonClicked(out shapeType))
            {
                switch (shapeType)
                {
                    case "Circle":
                        _currentShapeType = DrawingProgram.ShapeType.Circle;
                        break;
                    case "Square":
                        _currentShapeType = DrawingProgram.ShapeType.Square;
                        break;
                    case "Triangle":
                        _currentShapeType = DrawingProgram.ShapeType.Triangle;
                        break;
                }
            }

            // Check if Undo button is clicked and perform undo operation
            if (_menu.IsUndoButtonClicked())
            {
                _drawingProgram.Undo();
            }

            // Check if Clear button is clicked and perform clear operation
            if (_menu.IsClearButtonClicked())
            {
                _drawingProgram.Clear();
            }

            // Update the drawing program with the current shape type and color
            _drawingProgram.Update(gameTime, _currentShapeType, _drawingArea.CurrentColor, _drawingArea.Bounds); // Använd egenskapen CurrentColor istället för GetColor()
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin(); // Begin ritningen

            // Rita färgmenyn, ritningsprogrammet, ritningsområdet och menyn
            _colorMenu.Draw(_spriteBatch);
            _drawingProgram.Draw(_spriteBatch);
            _drawingArea.Draw(_spriteBatch);
            _menu.Draw(_spriteBatch);

            _spriteBatch.End(); // Avsluta ritningen
            base.Draw(gameTime);
        }
    }
}
