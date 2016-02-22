using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
    public class EasySprite
    {
        private readonly Texture2D _texture;
        private readonly Vector2 _origin;
        private readonly Vector2 _scale;

        public EasySprite(Texture2D texture, int frameWidth, int frameHeight, float scale = 1f)
        {
            _texture = texture;
            _origin = new Vector2(frameWidth, frameHeight);
            _scale = new Vector2(scale, scale);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            spriteBatch.Draw(
                _texture,
                position,
                scale: _scale,
                origin: _origin,
                color: Color.White,
                effects: spriteEffects);
        }
    }
}
