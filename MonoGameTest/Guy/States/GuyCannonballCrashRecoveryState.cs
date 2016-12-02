using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    public class GuyCannonballCrashRecoveryState : IGuyState
    {
        private readonly EasySpriteAnimation _animation;
        private readonly EasyTimer _timer = new EasyTimer(TimeSpan.FromMilliseconds(1000));

        public GuyCannonballCrashRecoveryState(EasySpriteAnimation animation)
        {
            _animation = animation;
        }

        public string Name => "CannonballCrashRecovery";

        public void Enter(Guy guy, GameTime gameTime)
        {
            _timer.Start(gameTime);
            _animation.Reset();
        }

        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _animation.Draw(spriteBatch, guy.Physics.Position, gameTime, spriteEffects);
        }

        public IGuyState Update(Guy guy, KeyboardState keyboardState, GameTime gameTime)
        {
            return _timer.IsFinished(gameTime)
                ? guy.States.Idle
                : null;
        }
    }
}