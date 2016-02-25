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
        private readonly Vector2 _scale;
        private readonly bool _canLoop;

        public EasySpriteAnimation(Texture2D spriteSheet, int frameWidth, int frameHeight, int frameColumnCount, int frameRowCount, float frameDuration, float scale = 1f, bool canLoop = true)
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
            _scale = new Vector2(scale, scale);
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
                _scale,
                spriteEffects,
                0.0f);
        }

        public void Reset()
        {
            _frameIndex = 0;
        }
    }
}
