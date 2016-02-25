using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Controls.AI
{
    public class CpuKeyboardControls : IKeyboardControls
    {
        private readonly KeyPress[] _keyPresses;
        private readonly bool _canLoop;

        public CpuKeyboardControls(KeyPress[] keyPresses, bool canLoop)
        {
            _keyPresses = keyPresses;
            _canLoop = canLoop;
        }

        public KeyboardState GetState(GameTime gameTime)
        {
            if (_keyPresses.Length == 0)
            {
                return new KeyboardState();
            }
            var timeLeft = gameTime.TotalGameTime;
            do
            {
                foreach (var press in _keyPresses)
                {
                    timeLeft -= press.Duration;
                    if (timeLeft.TotalMilliseconds <= 0)
                    {
                        return new KeyboardState(press.Key);
                    }
                }
            } while (_canLoop);
            return new KeyboardState();
        }
    }
}