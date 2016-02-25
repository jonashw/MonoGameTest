using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    internal class GuyCannonballState : IGuyState
    {
        private readonly EasySprite _sprite;

        public GuyCannonballState(EasySprite sprite)
        {
            _sprite = sprite;
        }

        public string Name
        {
            get { return "Cannonball"; }
        }

        public void Enter(Guy guy, GameTime gameTime)
        {
            guy.Physics.SetYVelocity(5);
        }

        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _sprite.Draw(spriteBatch, guy.Physics.Position, spriteEffects);
        }

        public IGuyState Update(Guy guy, KeyboardState keyboardState, GameTime gameTime)
        {
            if (guy.Physics.IsOnGround)
            {
                return guy.States.CannonballCrash;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                return guy.States.Jumping;
            }
            return null;
        }
    }
}