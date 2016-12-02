using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
    public class BoundingBox
    {
        private readonly Texture2D _boxTexture;
        private readonly SpriteFont _font;
        public bool ShouldDraw = false;

        public BoundingBox(Texture2D boxTexture, SpriteFont font)
        {
            _boxTexture = boxTexture;
            _font = font;
        }

        public void Draw(SpriteBatch sb, Vector2 position)
        {
            if (!ShouldDraw)
            {
                return;
            }
            var tl = position;
            var tr = position + new Vector2(_boxTexture.Width, 0);
            var br = position + new Vector2(_boxTexture.Width, _boxTexture.Height);
            var bl = position + new Vector2(0, _boxTexture.Height);

            sb.Draw(_boxTexture, position);
            sb.DrawString(_font, $"({tl.X},{tl.Y})", tl, Color.White);
            sb.DrawString(_font, $"({tr.X},{tr.Y})", tr, Color.White);
            sb.DrawString(_font, $"({br.X},{br.Y})", br, Color.White);
            sb.DrawString(_font, $"({bl.X},{bl.Y})", bl, Color.White);
        }
    }
}