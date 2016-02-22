using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
    public class Guy
    {
        public Vector2 Position;
        private readonly EasySprite _sprite;

        public Guy(Vector2 position, Texture2D spriteSheet)
        {
            Position = position;
            _sprite = new EasySprite(spriteSheet, 500, 667, 6, 2, 0.08f);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, Position, gameTime);
        }
    }
}