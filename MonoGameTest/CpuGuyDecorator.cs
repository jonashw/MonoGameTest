using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest.Controls;
using MonoGameTest.Controls.AI;

namespace MonoGameTest
{
    public class CpuGuyDecorator
    {
        public readonly Guy.Guy Guy;
        private readonly CircularArray<CpuKeyboardControls> _controlPatterns;
        private bool _active;
        private readonly SpriteFont _font;

        public CpuGuyDecorator(Guy.Guy guy, SpriteFont font)
        {
            Guy = guy;
            _font = font;
            _controlPatterns = new CircularArray<CpuKeyboardControls>(
                CpuKeyboardControlsFactory.RunningCelebration(),
                CpuKeyboardControlsFactory.RunBackAndForth(),
                CpuKeyboardControlsFactory.Cannonball(),
                CpuKeyboardControlsFactory.JumpAndSquat());
            KeyboardEventRegistry.OnKeyDown(Keys.Left, OnLeft);
            KeyboardEventRegistry.OnKeyDown(Keys.Right, OnRight);
        }

        public void OnLeft()
        {
            if (!_active)
            {
                return;
            }
            _controlPatterns.Prev();
        }
        public void OnRight()
        {
            if (!_active)
            {
                return;
            }
            _controlPatterns.Next();
        }

        public void Update(GameTime gameTime)
        {
            Guy.Update(
                gameTime,
                _controlPatterns.GetCurrent().GetState(gameTime));
        }

        public void Activate()
        {
            _active = true;
        }

        public void Deactivate()
        {
            _active = false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Guy.Draw(spriteBatch, gameTime);
            spriteBatch.DrawString(
                _font,
                string.Format("Use Left/Right to cycle through the CPU control patterns (Currently on \"{0}\")", _controlPatterns.GetCurrent().Name),
                new Vector2(5, 25),
                Color.Black);
        }
    }
}