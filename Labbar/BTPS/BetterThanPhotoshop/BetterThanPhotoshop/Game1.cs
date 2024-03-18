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
        private DrawingProgram.ShapeType _currentShapeType = DrawingProgram.ShapeType.Circle; // Standardformen är en cirkel.

        // Konstruktorn för Game1-klassen.
        public Game1()
        {
            // Skapar en grafikhanterare och ange rotdiket för innehållet.
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Ställer in önskad upplösning och möjliggör ändring av fönsterstorlek.
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 800;
            Window.AllowUserResizing = true;

            // Visar muspekaren i spelet.
            IsMouseVisible = true;
        }

        // Metod för att spara ritad bild som en PNG-fil.
        public void SaveDrawingAsImage(RenderTarget2D renderTarget, string filePath)
        {
            Console.WriteLine("Saving drawing as image..."); // Skriver ut meddelande för att bekräfta sparande (för felsökning).
            // Öppnar en ström för att skapa en fil för sparande.
            using (Stream stream = File.Create(filePath))
            {
                renderTarget.SaveAsPng(stream, renderTarget.Width, renderTarget.Height); // Sparar renderingen som en PNG-fil.
            }
        }

        // Metod för att initialisera spelet.
        protected override void Initialize()
        {
            // Skapar en spritebatch.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Skapar ett ritningsområde och ett ritningsprogram och skicka med spritebatchen.
            _drawingArea = new DrawingArea(GraphicsDevice.Viewport.Width - 250, GraphicsDevice.Viewport.Height, Content.Load<Texture2D>("DrawingArea")); // Justera bredden på ritningsområdet
            _drawingProgram = new DrawingProgram(_spriteBatch, _drawingArea);

            // Skapar en meny och en färgmeny och skicka med referenser och innehållsmanagern.
            _menu = new Menu(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, Content, _drawingArea);
            _colorMenu = new ColorMenu(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, _menu.Buttons, Content);

            _menu.SetPosition(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            base.Initialize();
        }


        // Metod för att uppdatera spelet.
        protected override void Update(GameTime gameTime)
        {
            // Uppdaterar menyn och färgmenyn.
            _menu.Update();
            _colorMenu.Update();

            // Hämtar den valda färgen från färgmenyn och skicka den till ritningsprogrammet.
            Color? selectedColor = _colorMenu.GetSelectedColor();
            if (selectedColor.HasValue)
            {
                _drawingProgram.ChangeColor(selectedColor.Value);
            }

            // Kollar om Spara-knappen är klickad.
            if (_menu.IsSaveButtonClicked())
            {
                // Skapar en render target för att rita det ritade området.
                RenderTarget2D renderTarget = new RenderTarget2D(GraphicsDevice, _drawingArea.Width, _drawingArea.Height);

                // Ritar det ritade området till render target.
                GraphicsDevice.SetRenderTarget(renderTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _drawingArea.Draw(_spriteBatch);
                GraphicsDevice.SetRenderTarget(null);

                // Sparar render target som en bild.
                SaveDrawingAsImage(renderTarget, "Drawing.png");

                // Rensar render target efter användning.
                renderTarget.Dispose();
            }

            // Kollar om en formknapp är klickad och uppdatera aktuell formtyp.
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

            // Kollar om Undo-knappen är klickad och utför ångrad åtgärd.
            if (_menu.IsUndoButtonClicked())
            {
                _drawingProgram.Undo();
            }

            // Kollar om Clear-knappen är klickad och utför rensningsåtgärd.
            if (_menu.IsClearButtonClicked())
            {
                _drawingProgram.Clear();
            }

            // Uppdaterar ritningsprogrammet med aktuell formtyp och färg.
            _drawingProgram.Update(gameTime, _currentShapeType, _drawingArea.CurrentColor, _drawingArea.Bounds);

            base.Update(gameTime);
        }

        // Metod för att rita spelet.
        protected override void Draw(GameTime gameTime)
        {
            // Rensar skärmen med vit färg.
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin(); // Börjar ritning.

            // Ritar färgmenyn, ritningsprogrammet, ritningsområdet och menyn.
            _colorMenu.Draw(_spriteBatch);
            _drawingProgram.Draw(_spriteBatch);
            _drawingArea.Draw(_spriteBatch);
            _menu.Draw(_spriteBatch);

            _spriteBatch.End(); // Avslutar ritning.
            base.Draw(gameTime);
        }
    }
}
