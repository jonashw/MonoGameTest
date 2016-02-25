using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Controls
{
    public interface IKeyboardControls
    {
        KeyboardState GetState(GameTime gameTime);
    }
}