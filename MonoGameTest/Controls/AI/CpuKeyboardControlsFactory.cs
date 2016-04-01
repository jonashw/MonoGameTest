using System;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Controls.AI
{
    public static class CpuKeyboardControlsFactory
    {
        public static CpuKeyboardControls RunBackAndForth()
        {
            return new CpuKeyboardControls("Run Back and Forth", new[]
            {
                new KeyPress(Keys.Right, TimeSpan.FromMilliseconds(900)), 
                new KeyPress(Keys.Left,  TimeSpan.FromMilliseconds(900)) 
            }, true);
        }

        public static CpuKeyboardControls Cannonball()
        {
            return new CpuKeyboardControls("Cannonball", new[]
            {
                new KeyPress(Keys.Up, TimeSpan.FromMilliseconds(500)), 
                new KeyPress(Keys.Down,  TimeSpan.FromMilliseconds(1500)),
                new KeyPress(Keys.A, TimeSpan.FromMilliseconds(2100)) 
            }, true);
        }

        public static CpuKeyboardControls JumpAndSquat()
        {
            return new CpuKeyboardControls("JumpAndSquat", new[]
            {
                new KeyPress(Keys.Up, TimeSpan.FromMilliseconds(500)), 
                new KeyPress(Keys.Right,  TimeSpan.FromMilliseconds(700)),
                new KeyPress(Keys.Down, TimeSpan.FromMilliseconds(2100)),
                new KeyPress(Keys.Up, TimeSpan.FromMilliseconds(500)), 
                new KeyPress(Keys.Left,  TimeSpan.FromMilliseconds(700)),
                new KeyPress(Keys.Down, TimeSpan.FromMilliseconds(2100)),
            }, true);
        }
    }
}