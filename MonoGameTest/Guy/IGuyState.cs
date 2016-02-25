using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy
{
    public interface IGuyState
    {
        void Enter(Guy guy, GameTime gameTime);
        void Exit(Guy guy);
        void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects);
        IGuyState Update(Guy guy, KeyboardState keyboardState, GameTime gameTime);
        string Name { get; }
    }
}