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
                (int) (spriteWidth*scale),
                (int) (spriteHeight*scale),
                logger);

            var states = new GuyStates(
                new GuyIdleState(
                    new EasySprite(
                        content.Load<Texture2D>("Sprite-Idle"),
                        spriteWidth, spriteHeight, scale)),
                new GuyRunningState(
                    new EasySpriteAnimation(
                        content.Load<Texture2D>("Sprite-Running"),
                        spriteWidth, spriteHeight, 6, 2, 0.08f, scale),
                    new EasySprite(
                        content.Load<Texture2D>("Sprite-Sliding"),
                        spriteWidth, spriteHeight, scale)),
                new GuyJumpingState(
                    new EasySprite(
                        content.Load<Texture2D>("Sprite-Jumping"),
                        spriteWidth, spriteHeight, scale)),
                new GuyDuckingState(
                    new EasySprite(
                        content.Load<Texture2D>("Sprite-Ducking"),
                        spriteWidth, spriteHeight, scale)),
                new GuyCannonballState(
                    new EasySprite(
                        content.Load<Texture2D>("Sprite-Cannonball"),
                        spriteWidth, spriteHeight, scale)),
                new GuyCannonballCrashState(
                    new EasySpriteAnimation(
                        content.Load<Texture2D>("Sprite-CannonballCrash"),
                        spriteWidth, spriteHeight, 2, 1, 0.2f, scale, false)),
                new GuyCannonballCrashRecoveryState(
                    new EasySpriteAnimation(
                        content.Load<Texture2D>("Sprite-CannonballCrashRecovery"),
                        spriteWidth, spriteHeight, 6, 1, 0.08f, scale, false)));

            return new Guy(physics, states, logger);
        }
    }
}