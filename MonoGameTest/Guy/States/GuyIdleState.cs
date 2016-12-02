using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    internal class GuyIdleState : IGuyState
    {
        public string Name => "Idle";
        private readonly EasySprite _sprite;
        public GuyIdleState(EasySprite sprite)
        {
            _sprite = sprite;
        }
        public void Enter(Guy guy, GameTime gameTime) { }
        public void Exit(Guy guy) { }
        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _sprite.Draw(spriteBatch, guy.Physics.Position, spriteEffects);
        }

        public IGuyState Update(Guy guy, KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space))
            {
                return guy.States.Jumping;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                return guy.States.Ducking;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                guy.Facing = XDirection.Right;
                return guy.States.Running;
            } 
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                guy.Facing = XDirection.Left;
                return guy.States.Running;
            }
            return null;
        }
    }
}