using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Guy.States;
using MonoGameTest.Logging;

namespace MonoGameTest.Guy
{
    public static class GuyFactory
    {
        private const int SpriteWidth = 250;
        private const int SpriteHeight = 333;
        public static Guy Create(GraphicsDevice graphics, ContentManager content, DebugLogger logger, Vector2 position)
        {
            var physics = new GuyPhysics(
                position,
                new Vector2(0,0),
                SpriteWidth,
                SpriteHeight,
                logger);

            var states = new GuyStates(
                jumping:    new GuyJumpingState   (new EasySprite(content.Load<Texture2D>("Guy-Jumping"))),
                hooray:     new GuyHoorayState    (new EasySprite(content.Load<Texture2D>("Guy-Hooray"))),
                ducking:    new GuyDuckingState   (new EasySprite(content.Load<Texture2D>("Guy-Ducking"))),
                idle:       new GuyIdleState      (new EasySprite(content.Load<Texture2D>("Guy-Idle"))),
                cannonball: new GuyCannonballState(new EasySprite(content.Load<Texture2D>("Guy-Cannonball"))),
                running: new GuyRunningState(
                            new EasySpriteAnimation(content.Load<Texture2D>("Guy-Running"), 6, 2, 0.08f),
                            new EasySprite(content.Load<Texture2D>("Guy-Sliding"))),
                cannonballCrash: new GuyCannonballCrashState(
                            new EasySpriteAnimation(content.Load<Texture2D>("Guy-CannonballCrash"), 2, 1, 0.2f, canLoop: false)),
                cannonballCrashRecovery: new GuyCannonballCrashRecoveryState(
                            new EasySpriteAnimation(content.Load<Texture2D>("Guy-CannonballCrashRecovery"), 6, 1, 0.08f, canLoop: false)));

            var boundingBox = new BoundingBox(
                TextureFactory.Rectangle(graphics, SpriteWidth, SpriteHeight, Color.Blue, false),
                content.Load<SpriteFont>("Consolas"));

            return new Guy(physics, states, logger, boundingBox);
        }
    }
}