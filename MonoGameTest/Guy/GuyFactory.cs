using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Guy.States;
using MonoGameTest.Logging;

namespace MonoGameTest.Guy
{
    public static class GuyFactory
    {
        public static Guy Create(Vector2 position, ContentManager content, DebugLogger logger)
        {
            const int spriteWidth = 500;
            const int spriteHeight = 667;
            const float scale = 0.5f;

            var physics = new GuyPhysics(
                position,
                new Vector2(0,0),
                spriteWidth,
                spriteHeight,
                logger);

            var states = new GuyStates(
                new GuyIdleState(
                    new EasySprite(content.Load<Texture2D>("Sprite-Idle"))),
                new GuyRunningState(
                    new EasySpriteAnimation(
                        content.Load<Texture2D>("Sprite-Running"),
                        6, 2, 0.08f),
                    new EasySprite(content.Load<Texture2D>("Sprite-Sliding"))),
                new GuyJumpingState(
                    new EasySprite(content.Load<Texture2D>("Sprite-Jumping"))),
                new GuyDuckingState(
                    new EasySprite(content.Load<Texture2D>("Sprite-Ducking"))),
                new GuyCannonballState(
                    new EasySprite(content.Load<Texture2D>("Sprite-Cannonball"))),
                new GuyCannonballCrashState(
                    new EasySpriteAnimation(
                        content.Load<Texture2D>("Sprite-CannonballCrash"),
                        2, 1, 0.2f, canLoop: false)),
                new GuyCannonballCrashRecoveryState(
                    new EasySpriteAnimation(
                        content.Load<Texture2D>("Sprite-CannonballCrashRecovery"),
                        6, 1, 0.08f, canLoop: false)));

            return new Guy(physics, states, logger);
        }
    }
}