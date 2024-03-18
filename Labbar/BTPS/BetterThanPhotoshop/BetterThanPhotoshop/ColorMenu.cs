using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BetterThanPhotoshop
{
    public class ColorMenu
    {
        private List<ColorButton> _colorButtons; // Lista med färgknappar.
        private int _buttonSize = 30; // Storleken på färgknapparna.
        private int _padding = 10; // Mellanrum mellan knapparna.
        private int _xOffset; // Avstånd från vänsterkant.
        private List<Button> _referenceButtons; // Lista med referensknappar för att placera färgknapparna bredvid andra knappar.
        private ContentManager _content; // Referens till ContentManager för att ladda in färgknappans textur.

        // Konstruktor för att skapa en ny färgmeny
        public ColorMenu(int screenWidth, int screenHeight, List<Button> referenceButtons, ContentManager content)
        {
            _colorButtons = new List<ColorButton>(); // Skapar en ny lista med färgknappar.
            _referenceButtons = referenceButtons; // Sparar referensen till referensknapparna.
            _content = content; // Sparar referensen till ContentManager.

            // Listar med filnamn för varje färgknapp.
            string[] textureNames = new string[]
            {
                "BlackButton", "BlueButton", "GreenButton", "OrangeButton",
                "PurpleButton", "RedButton", "VioletButton", "YellowButton"
            };

            // Beräknar positionen för färgmenyn baserat på referensknapparna.
            _xOffset = (int)_referenceButtons[_referenceButtons.Count - 1].Position.X + _referenceButtons[_referenceButtons.Count - 1].Width + _padding;
            int y = (int)_referenceButtons[_referenceButtons.Count - 1].Position.Y;

            // Skapar färgknappar för varje filnamn och lägg till dem i listan med färgknappar.
            foreach (string textureName in textureNames)
            {
                // Laddar in textur för färgknappen.
                Texture2D texture = _content.Load<Texture2D>(textureName);

                // Skapar en ny färgknapp och lägg till den i listan med färgknappar.
                Rectangle bounds = new Rectangle(_xOffset, y, _buttonSize, _buttonSize); // Skapar en rektangel för knappens område baserat på position och storlek.
                _colorButtons.Add(new ColorButton(bounds, texture)); // Lägger till den nya färgknappen i listan.
                y += _buttonSize + _padding; // Ökar y-koordinaten för nästa knapp.
            }
        }

        // Metod för att uppdatera färgmenyn.
        public void Update()
        {
            foreach (var button in _colorButtons)
            {
                button.Update(); // Uppdaterar varje färgknapp.
            }
        }

        // Metod för att rita färgmenyn på skärmen.
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in _colorButtons)
            {
                button.Draw(spriteBatch); // Ritar varje färgknapp.
            }
        }

        // Metod för att hämta den valda färgen från färgmenyn.
        public Color? GetSelectedColor()
        {
            foreach (var button in _colorButtons)
            {
                if (button.IsClicked) // Kontrollerar om färgknappen är klickad.
                {
                    return button.Color; // Returnerar färgen på den klickade knappen.
                }
            }
            return null; // Returnerar null om ingen knapp är klickad.
        }
    }

    // En klass som representerar en färgknapp i färgmenyn.
    public class ColorButton
    {
        // Privata medlemsvariabler för färgknappen.
        private Rectangle _bounds; // Rektangel som representerar knappens område på skärmen.
        private Color _color; // Färgen på knappen.
        private Texture2D _texture; // Textur för färgknappen.

        // Egenskap för att hämta färgen på knappen.
        public Color Color => _color;

        // Egenskap för att kontrollera om knappen är klickad.
        public bool IsClicked { get; private set; }

        // Konstruktor för att skapa en ny färgknapp.
        public ColorButton(Rectangle bounds, Texture2D texture)
        {
            _bounds = bounds; // Sparar rektangeln för knappens område.
            _texture = texture; // Sparar knappens textur.

            // Extraherar färgen från texturnamnet.
            _color = ExtractColorFromTextureName(texture.Name);
        }

        // Metod för att extrahera färgen från texturnamnet.
        private Color ExtractColorFromTextureName(string textureName)
        {
            string colorName = textureName.Replace("Button", ""); // Tar bort "Button" från texturnamnet för att få färgnamnet.
            switch (colorName)
            {
                // Returnerar motsvarande färg beroende på färgnamnet.
                case "Black":
                    return Color.Black;
                case "Blue":
                    return Color.Blue;
                case "Green":
                    return Color.Green;
                case "Orange":
                    return Color.Orange;
                case "Purple":
                    return Color.Purple;
                case "Red":
                    return Color.Red;
                case "Violet":
                    return Color.Violet;
                case "Yellow":
                    return Color.Yellow;
                default:
                    return Color.White; 
            }
        }

        // Metod för att uppdatera färgknappen.
        public void Update()
        {
            var mouseState = Mouse.GetState(); // Hämtar musens aktuella tillstånd.
            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1); // Skapar en rektangel runt musens aktuella position.

            IsClicked = false; // Återställer klickstatusen för knappen.

            // Kontrollerar om musen är över knappen och vänster musknapp är nedtryckt.
            if (mouseRectangle.Intersects(_bounds) && mouseState.LeftButton == ButtonState.Pressed)
            {
                IsClicked = true; // Anger att knappen är klickad.
            }
        }

        // Metod för att rita färgknappen på skärmen.
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _bounds, Color.White); // Ritar knappen med dess textur och position.
        }
    }
}
