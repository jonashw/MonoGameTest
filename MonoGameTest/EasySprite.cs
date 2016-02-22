using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
    public class EasySprite
    {
        private readonly Texture2D _spriteSheet;
        private readonly Vector2 _origin;
        private float _time;
        private readonly Rectangle[] _frameRectangles;
        private int _frameIndex;
        private readonly float _frameDuration;

        public EasySprite(Texture2D spriteSheet, int frameWidth, int frameHeight, int frameColumnCount, int frameRowCount, float frameDuration)
        {
            _spriteSheet = spriteSheet;
            _origin = new Vector2(frameWidth, frameHeight);
            _frameRectangles = Enumerable.Range(0, frameRowCount)
                .SelectMany(rowIndex => Enumerable.Range(0, frameColumnCount)
                    .Select(colIndex => new Rectangle(
                        colIndex * frameWidth,
                        rowIndex * frameHeight,
                        frameWidth,
                        frameHeight)))
                .ToArray();
            _frameDuration = frameDuration;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_time > _frameDuration)
            {
                _frameIndex++;
                _time = 0f;
            }

            if (_frameIndex >= _frameRectangles.Length)
            {
                _frameIndex = 0;
            }

            spriteBatch.Draw(
                _spriteSheet,
                position,
                _frameRectangles[_frameIndex],
                Color.White,
                0.0f,
                _origin,
                1.0f,
                SpriteEffects.None,
                0.0f);
        }
    }
}
