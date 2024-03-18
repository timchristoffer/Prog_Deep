using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BetterThanPhotoshop
{
    public class Button
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _rectangle;
        private Color _color = Color.White;
        private ContentManager _content;
        private bool _wasMousePressedLastUpdate = false;

        public Color Color { get; set; }

        public int Width => _texture.Width;
        public int Height => _texture.Height;
        public Vector2 Position { get => _position; set => _position = value; }
        public string Tag { get; set; } // Add Tag property
        public string Name { get; set; } // Add Name property

        public Button(string texturePath, Vector2 position, ContentManager content)
        {
            _content = content;
            _position = position;
            _texture = _content.Load<Texture2D>(texturePath);
            _rectangle = new Rectangle((int)position.X, (int)position.Y, _texture.Width, _texture.Height);
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            bool isMousePressed = mouseState.LeftButton == ButtonState.Pressed;

            if (mouseRectangle.Intersects(_rectangle))
            {
                _color = Color.Gray;
                if (_wasMousePressedLastUpdate && !isMousePressed)
                {
                    IsClicked = true;
                }
            }
            else
            {
                _color = Color.White;
            }

            _wasMousePressedLastUpdate = isMousePressed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, _color);
        }

        public bool IsClicked { get; set; } = false;

        // Reset button click status
        public void ResetClick()
        {
            IsClicked = false;
        }
    }
}
