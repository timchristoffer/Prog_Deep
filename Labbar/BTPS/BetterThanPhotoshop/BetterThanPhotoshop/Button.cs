using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BetterThanPhotoshop
{
    public class Button
    {
        private Texture2D _texture; // Textur för knappen.
        private Vector2 _position; // Position för knappen.
        private Rectangle _rectangle; // Rektangel som representerar knappens område på skärmen.
        private Color _color = Color.White; // Färgen på knappen.
        private ContentManager _content; // ContentManager för att ladda in knappens textur.
        private bool _wasMousePressedLastUpdate = false; // Flaggar som håller koll på om musen var nedtryckt i föregående uppdatering.

        // Egenskap för att hämta eller sätta knappens färg.
        public Color Color { get; set; }

        // Egenskaper för att hämta bredd och höjd för knappen.
        public int Width => _texture.Width;
        public int Height => _texture.Height;

        // Egenskap för att hämta eller sätta knappens position.
        public Vector2 Position { get => _position; set => _position = value; }

        // Egenskap för att definiera en tagg för knappen.
        public string Tag { get; set; }

        // Egenskap för att definiera ett namn för knappen.
        public string Name { get; set; }

        // Konstruktor för att skapa en ny knapp.
        public Button(string texturePath, Vector2 position, ContentManager content)
        {
            _content = content;
            _position = position;
            _texture = _content.Load<Texture2D>(texturePath); // Laddar in knappens textur från angiven sökväg.
            _rectangle = new Rectangle((int)position.X, (int)position.Y, _texture.Width, _texture.Height); // Skapar en rektangel runt knappen baserat på dess position och storlek.
        }

        // Metod för att uppdatera knappens tillstånd.
        public void Update()
        {
            // Hämtar musens aktuella tillstånd.
            MouseState mouseState = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1); // Skapar en rektangel runt musens aktuella position.
            bool isMousePressed = mouseState.LeftButton == ButtonState.Pressed; // Kontrollerar om vänster musknapp är nedtryckt.

            // Kontrollerar om muspekaren är över knappen.
            if (mouseRectangle.Intersects(_rectangle))
            {
                _color = Color.Gray; // Ändrar knappens färg till grå om musen är över den.
                // Kontrollerar om musen var nedtryckt i föregående uppdatering och inte är nedtryckt i denna uppdatering, vilket indikerar att användaren klickade på knappen.
                if (_wasMousePressedLastUpdate && !isMousePressed)
                {
                    IsClicked = true; // Anger att knappen är klickad.
                }
            }
            else
            {
                _color = Color.White; // Återställer knappens färg till vit om musen inte är över den.
            }

            _wasMousePressedLastUpdate = isMousePressed; // Sparar det aktuella tillståndet för musknappen för nästa uppdatering.
        }

        // Metod för att rita knappen på skärmen.
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, _color); // Ritar knappen med dess textur, position, och färg.
        }

        // Egenskap för att kontrollera om knappen är klickad.
        public bool IsClicked { get; set; } = false;

        // Metod för att återställa knappens klickstatus.
        public void ResetClick()
        {
            IsClicked = false; // Återställer knappens klickstatus till falskt.
        }
    }
}
