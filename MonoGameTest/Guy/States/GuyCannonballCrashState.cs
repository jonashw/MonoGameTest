using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    internal class GuyCannonballCrashState : IGuyState
    {
        private readonly EasySpriteAnimation _animation;
        private readonly EasyTimer _timer = new EasyTimer(TimeSpan.FromMilliseconds(1500));

        public GuyCannonballCrashState(EasySpriteAnimation animation)
        {
            _animation = animation;
        }

        public string Name => "CannonballCrash";

        public void Enter(Guy guy, GameTime gameTime)
        {
            guy.Physics.Stop();
            _animation.Reset();
            _timer.Start(gameTime);
        }

        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _animation.Draw(spriteBatch, guy.Physics.Position, gameTime, spriteEffects);
        }

        public IGuyState Update(Guy guy, KeyboardState keyboardState, GameTime gameTime)
        {
            return _timer.IsFinished(gameTime)
                ? guy.States.CannonballCrashRecovery
                : null;
        }
    }
}