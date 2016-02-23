using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest.Guy.States;
using MonoGameTest.Logging;

namespace MonoGameTest.Guy
{
    public class Guy
    {
        public readonly GuyPhysics Physics;
        internal readonly GuyStates States;
        public bool FacingRight = true;
        private IGuyState _state;
        private readonly ILogger _logger;

        public Guy(Vector2 position, Texture2D idleTexture, Texture2D jumpingTexture, Texture2D runningTexture, ILogger logger)
        {
            const int spriteWidth = 500;
            const int spriteHeight = 667;
            const float scale = 0.5f;
            Physics = new GuyPhysics(position, new Vector2(0,0), (int) (spriteWidth*scale), (int) (spriteHeight*scale), logger);
            States = new GuyStates(
                new GuyIdleState(new EasySprite(idleTexture, spriteWidth, spriteHeight, scale)),
                new GuyRunningState(new EasySpriteAnimation(runningTexture, spriteWidth, spriteHeight, 6, 2, 0.08f, scale)),
                new GuyJumpingState(new EasySprite(jumpingTexture, spriteWidth, spriteHeight, scale)));
            _state = States.Idle;
            _logger = logger;
        }

        public const int ZeroAltitude = 720;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _state.Draw(
                this,
                spriteBatch,
                gameTime,
                FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var maybeNewState = _state.Update(this, keyboardState);
            Physics.Update();
            if (maybeNewState == null)
            {
                return;
            }
            _state.Exit(this);
            maybeNewState.Enter(this);
            _logger.Log(string.Format("Transitioning from {0} to {1}", _state.Name, maybeNewState.Name));
            _state = maybeNewState;
        }
    }
}