using System;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Controls.AI
{
    public struct KeyPress
    {
        public readonly Keys Key;
        public readonly TimeSpan Duration;

        public KeyPress(Keys key, TimeSpan duration)
        {
            Key = key;
            Duration = duration;
        }
    }
}