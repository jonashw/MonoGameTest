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
            guy.Velocity.Y = -22;
        }
        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _sprite.Draw(spriteBatch, guy.Position, spriteEffects);
        }

        public IGuyState HandleInput(Guy guy, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                return guy.States.Idle;
            }
            return null;
        }

        public IGuyState Update(Guy guy)
        {
            if (guy.Position.Y >= 720)
            {
                return guy.States.Idle;
            }
            return null;
        }
    }
}