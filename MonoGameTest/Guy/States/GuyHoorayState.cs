using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    public class GuyHoorayState : IGuyState
    {
        public string Name => "Hooray";
        private readonly EasySprite _sprite;
        public GuyHoorayState(EasySprite sprite)
        {
            _sprite = sprite;
        }

        public void Enter(Guy guy, GameTime gameTime)
        {
            guy.Physics.SetYVelocity(-15);
        }

        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _sprite.Draw(spriteBatch, guy.Physics.Position, spriteEffects);
        }

        public IGuyState Update(Guy guy, KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                return guy.States.Cannonball;
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