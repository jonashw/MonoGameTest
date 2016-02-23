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

        public Guy(Vector2 position, Texture2D idleSprite, Texture2D jumpingSprite, Texture2D runningSprite, ILogger logger)
        {
            const int width = 500;
            const int height = 667;
            const float scale = 0.5f;
            Physics = new GuyPhysics(position, new Vector2(0,0), logger);
            States = new GuyStates(
                new GuyIdleState(new EasySprite(idleSprite, width, height, scale)),
                new GuyRunningState(new EasySpriteAnimation(runningSprite, width, height, 6, 2, 0.08f, scale)),
                new GuyJumpingState(new EasySprite(jumpingSprite, width, height, scale)));
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

        public void HandleInput(KeyboardState keyboardState)
        {
            maybeTransitionToNewState(
                _state.HandleInput(this, keyboardState));
        }

        public void Update(GameTime gameTime)
        {
            maybeTransitionToNewState(
                _state.Update(this));
            Physics.Update();
        }

        private void maybeTransitionToNewState(IGuyState maybeNewState)
        {
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