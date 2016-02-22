using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    internal class GuyIdleState : IGuyState
    {
        public string Name { get { return "Idle"; } }
        private readonly EasySprite _sprite;
        public GuyIdleState(EasySprite sprite)
        {
            _sprite = sprite;
        }
        public void Enter(Guy guy) { }
        public void Exit(Guy guy) { }
        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _sprite.Draw(spriteBatch, guy.Position, spriteEffects);
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
                return guy.States.Running;
            } 
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                guy.FacingRight = false;
                return guy.States.Running;
            }
            return null;
        }

        public IGuyState Update(Guy guy)
        {
            return null;
        }
    }
}