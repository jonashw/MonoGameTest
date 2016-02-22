using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy
{
    public interface IGuyState
    {
        void Enter(Guy guy);
        void Exit(Guy guy);
        void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects);
        IGuyState HandleInput(Guy guy, KeyboardState keyboardState);
        IGuyState Update(Guy guy);
        string Name { get; }
    }
}