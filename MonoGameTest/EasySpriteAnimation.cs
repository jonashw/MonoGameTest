using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
    public class EasySpriteAnimation
    {
        private readonly Texture2D _spriteSheet;
        private readonly Vector2 _origin;
        private float _time;
        private readonly Rectangle[] _frameRectangles;
        private int _frameIndex;
        private readonly float _frameDuration;
        private readonly bool _canLoop;

        public EasySpriteAnimation(Texture2D spriteSheet, int frameColumnCount, int frameRowCount, float frameDuration, bool canLoop = true)
        {
            _spriteSheet = spriteSheet;
            var frameWidth = spriteSheet.Width/frameColumnCount;
            var frameHeight = spriteSheet.Height/frameRowCount;
            _origin = new Vector2(0,0);
            _frameRectangles = Enumerable.Range(0, frameRowCount)
                .SelectMany(rowIndex => Enumerable.Range(0, frameColumnCount)
                    .Select(colIndex => new Rectangle(
                        colIndex * frameWidth,
                        rowIndex * frameHeight,
                        frameWidth,
                        frameHeight)))
                .ToArray();
            _frameDuration = frameDuration;
            _canLoop = canLoop;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _time += (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (_time > _frameDuration)
            {
                _frameIndex++;
                _time = 0f;
            }

            if (_frameIndex >= _frameRectangles.Length)
            {
                _frameIndex = _canLoop ? 0 : _frameRectangles.Length - 1;
            }

            spriteBatch.Draw(
                _spriteSheet,
                position,
                _frameRectangles[_frameIndex],
                Color.White,
                0.0f,
                _origin,
                1f,
                spriteEffects,
                0.0f);
        }

        public void Reset()
        {
            _frameIndex = 0;
        }
    }
}
