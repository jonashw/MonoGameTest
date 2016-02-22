using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    internal class GuyRunningState : IGuyState
    {
        public string Name { get { return "Running"; } }
        private readonly EasySpriteAnimation _animation;
        public GuyRunningState(EasySpriteAnimation animation)
        {
            _animation = animation;
        }

        public void Enter(Guy guy)
        {
            _animation.Reset();
        }

        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _animation.Draw(spriteBatch, guy.Position, gameTime, spriteEffects);
        }

        public IGuyState HandleInput(Guy guy, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space))
            {
                return guy.States.Jumping;
            }
            if (!keyboardState.IsKeyDown(Keys.Right) && !keyboardState.IsKeyDown(Keys.Left))
            {
                return guy.States.Idle;
            }
            return null;
        }

        public IGuyState Update(Guy guy)
        {
            if (guy.FacingRight)
            {
                guy.Position.X += 5;
            }
            else
            {
                guy.Position.X -= 5;
            }
            return null;
        }
    }
}