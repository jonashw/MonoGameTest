using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    internal class GuyRunningState : IGuyState
    {
        public string Name { get { return "Running"; } }
        private readonly EasySpriteAnimation _runningAnimation;
        private readonly EasySprite _slidingSprite;
        private const int RunningSpeed = 10;
        public GuyRunningState(EasySpriteAnimation runningAnimation, EasySprite slidingSprite)
        {
            _runningAnimation = runningAnimation;
            _slidingSprite = slidingSprite;
        }

        public void Enter(Guy guy, GameTime gameTime)
        {
            _runningAnimation.Reset();
            impulse(guy);
        }

        public void Exit(Guy guy)
        {
            guy.Physics.StepDecelerate(); //This will kill any lingering horizontal movement.
        }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            var maybeXDirection = guy.Physics.HorizontalMovementDirection;
            if (!maybeXDirection.HasValue || maybeXDirection == guy.Facing)
            {
                _runningAnimation.Draw(spriteBatch, guy.Physics.Position, gameTime, spriteEffects);
            }
            else if(maybeXDirection.Value != guy.Facing)
            {
                _slidingSprite.Draw(spriteBatch, guy.Physics.Position, spriteEffects);
            }
        }

        public IGuyState Update(Guy guy, KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                return guy.States.Ducking;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space))
            {
                return guy.States.Jumping;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                guy.Facing = XDirection.Right;
                impulse(guy);
            } 
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                guy.Facing = XDirection.Left;
                impulse(guy);
            }
            else if (guy.Physics.IsMovingHorizontally)
            {
                guy.Physics.StepDecelerate();
            }
            else
            {
                return guy.States.Idle;
            }
            return null;
        }

        private static void impulse(Guy guy)
        {
            guy.Physics.StepAccelerate(guy.Facing == XDirection.Right ? RunningSpeed : -RunningSpeed);
        }
    }
}