using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BetterThanPhotoshop
{
    public class ColorMenu
    {
        private List<ColorButton> _colorButtons;
        private int _buttonSize = 30; // Storleken på färgknapparna
        private int _padding = 10; // Mellanrum mellan knapparna
        private int _xOffset; // Avstånd från vänsterkant
        private List<Button> _referenceButtons; // Lista med referensknappar för att placera färgknapparna bredvid andra knappar
        private ContentManager _content; // Referens till ContentManager för att ladda in färgknappans textur

        public ColorMenu(int screenWidth, int screenHeight, List<Button> referenceButtons, ContentManager content)
        {
            _colorButtons = new List<ColorButton>();
            _referenceButtons = referenceButtons;
            _content = content;

            // Lista med filnamn för varje färgknapp
            string[] textureNames = new string[]
            {
                "BlackButton", "BlueButton", "GreenButton", "OrangeButton",
                "PurpleButton", "RedButton", "VioletButton", "YellowButton"
            };

            // Beräkna positionen för färgmenyn baserat på referensknapparna
            _xOffset = (int)_referenceButtons[_referenceButtons.Count - 1].Position.X + _referenceButtons[_referenceButtons.Count - 1].Width + _padding;
            int y = (int)_referenceButtons[_referenceButtons.Count - 1].Position.Y;

            foreach (string textureName in textureNames)
            {
                // Ladda in textur för färgknappen
                Texture2D texture = _content.Load<Texture2D>(textureName);

                // Skapa en ny färgknapp och lägg till den i listan med färgknappar
                Rectangle bounds = new Rectangle(_xOffset, y, _buttonSize, _buttonSize);
                _colorButtons.Add(new ColorButton(bounds, texture));
                y += _buttonSize + _padding;
            }
        }

        public void Update()
        {
            foreach (var button in _colorButtons)
            {
                button.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in _colorButtons)
            {
                button.Draw(spriteBatch);
            }
        }

        public Color? GetSelectedColor()
        {
            foreach (var button in _colorButtons)
            {
                if (button.IsClicked)
                {
                    return button.Color;
                }
            }
            return null;
        }
    }

    public class ColorButton
    {
        private Rectangle _bounds;
        private Color _color;
        private Texture2D _texture; // Textur för färgknappen
        public Color Color => _color; // Lägg till egenskapen för färgen
        public bool IsClicked { get; private set; }

        public ColorButton(Rectangle bounds, Texture2D texture)
        {
            _bounds = bounds;
            _texture = texture;

            // Extrahera färgen från texturnamnet
            _color = ExtractColorFromTextureName(texture.Name);
        }

        // Metod för att extrahera färgen från texturnamnet
        private Color ExtractColorFromTextureName(string textureName)
        {
            // Anta att texturnamnet följer mönstret "{Color}Button" och extrahera färgen
            string colorName = textureName.Replace("Button", ""); // Ta bort "Button" från slutet av texturnamnet
            switch (colorName)
            {
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
                    return Color.White; // Default color if not found
            }
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            IsClicked = false;

            if (mouseRectangle.Intersects(_bounds) && mouseState.LeftButton == ButtonState.Pressed)
            {
                IsClicked = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null && _bounds != null)
            {
                // Ändra färgen på knappen om den är klickad
                Color buttonColor = IsClicked ? Color.Gray : Color.White;
                spriteBatch.Draw(_texture, _bounds, buttonColor);
            }
        }
    }
}
