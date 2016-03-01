using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest.Logging;

namespace MonoGameTest.Guy
{
    public class Guy : IEntity
    {
        public readonly GuyPhysics Physics;
        internal readonly GuyStates States;
        public XDirection Facing = XDirection.Right;
        private IGuyState _state;
        private readonly ILogger _logger;

        internal Guy(GuyPhysics physics, GuyStates states, ILogger logger)
        {
            Physics = physics;
            States = states;
            _state = states.Idle;
            _logger = logger;
        }

        public const int ZeroAltitude = 720;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _state.Draw(
                this,
                spriteBatch,
                gameTime,
                Facing == XDirection.Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var maybeNewState = _state.Update(this, keyboardState, gameTime);
            Physics.Update();
            if (maybeNewState == null)
            {
                return;
            }
            _state.Exit(this);
            maybeNewState.Enter(this, gameTime);
            _logger.Log(string.Format("Transitioning from {0} to {1}", _state.Name, maybeNewState.Name));
            _state = maybeNewState;
            keepOnScreen();
        }

        private void keepOnScreen()
        {
            //Make the level wrap around, horizontally.
            if (Physics.Position.X < 0)
            {
                Physics.Position.X = 1280 + Physics.Width;
            }
            if (Physics.Position.X > 1280 + Physics.Width)
            {
                Physics.Position.X = 0;
            }
        }
    }
}