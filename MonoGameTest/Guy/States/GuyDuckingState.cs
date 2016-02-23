using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    internal class GuyDuckingState : IGuyState
    {
        private readonly EasySprite _sprite;

        public GuyDuckingState(EasySprite sprite)
        {
            _sprite = sprite;
        }

        public string Name
        {
            get { return "Ducking"; }
        }
        public void Enter(Guy guy) { }

        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _sprite.Draw(spriteBatch, guy.Physics.Position, spriteEffects);
        }

        public IGuyState Update(Guy guy, KeyboardState keyboardState)
        {
            return keyboardState.IsKeyDown(Keys.Down)
                ? null
                : guy.States.Idle;
        }
    }
}
