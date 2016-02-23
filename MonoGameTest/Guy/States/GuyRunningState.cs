using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    internal class GuyRunningState : IGuyState
    {
        public string Name { get { return "Running"; } }
        private readonly EasySpriteAnimation _animation;
        private const int RunningSpeed = 5;
        public GuyRunningState(EasySpriteAnimation animation)
        {
            _animation = animation;
        }

        public void Enter(Guy guy)
        {
            _animation.Reset();
            runFullSpeed(guy);
        }

        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _animation.Draw(spriteBatch, guy.Physics.Position, gameTime, spriteEffects);
        }

        public IGuyState HandleInput(Guy guy, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space))
            {
                return guy.States.Jumping;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                guy.FacingRight = true;
                runFullSpeed(guy);
            } 
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                guy.FacingRight = false;
                runFullSpeed(guy);
            }
            return null;
        }

        private static void runFullSpeed(Guy guy)
        {
            guy.Physics.SetXVelocity(guy.FacingRight ? RunningSpeed : -RunningSpeed);
        }

        public IGuyState Update(Guy guy)
        {
            if (guy.Physics.IsMovingHorizontally)
            {
                guy.Physics.Drag();
            }
            else
            {
                return guy.States.Idle;
            }
            return null;
        }
    }
}