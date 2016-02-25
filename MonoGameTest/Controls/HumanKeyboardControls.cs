using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Controls
{
    public class HumanKeyboardControls : IKeyboardControls
    {
        public KeyboardState GetState(GameTime gameTime)
        {
            return Keyboard.GetState();
        }
    }
}