using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    public class GuyJumpingState : IGuyState
    {
        public string Name { get { return "Jumping"; } }
        private readonly EasySprite _sprite;
        public GuyJumpingState(EasySprite sprite)
        {
            _sprite = sprite;
        }

        public void Enter(Guy guy)
        {
            guy.Physics.SetYVelocity(-22);
        }

        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _sprite.Draw(spriteBatch, guy.Physics.Position, spriteEffects);
        }

        public IGuyState Update(Guy guy, KeyboardState keyboardState)
        {
            var maybeDirection = guy.Physics.HorizontalMovementDirection;
            if (maybeDirection.HasValue)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    guy.Facing = XDirection.Right;
                } 
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    guy.Facing = XDirection.Left;
                }
            }
            if (!guy.Physics.IsOnGround)
            {
                return null;
            }
            if (guy.Physics.IsMovingHorizontally)
            {
                return guy.States.Running;
            }
            return guy.States.Idle;
        }
    }
}