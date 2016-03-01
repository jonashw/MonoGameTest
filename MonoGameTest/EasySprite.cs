using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
    public class EasySprite
    {
        private readonly Texture2D _texture;
        private readonly Vector2 _origin;

        public EasySprite(Texture2D texture)
        {
            _texture = texture;
            _origin = new Vector2(texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            spriteBatch.Draw(
                _texture,
                position,
                origin: _origin,
                color: Color.White,
                effects: spriteEffects);
        }
    }
}
