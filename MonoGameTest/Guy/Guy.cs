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
        public readonly BoundingBox BoundingBox;

        internal Guy(GuyPhysics physics, GuyStates states, ILogger logger, BoundingBox boundingBox)
        {
            Physics = physics;
            States = states;
            _state = states.Idle;
            _logger = logger;
            BoundingBox = boundingBox;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BoundingBox.Draw(spriteBatch, Physics.Position);
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
            _logger.Log($"Transitioning from {_state.Name} to {maybeNewState.Name}");
            _state = maybeNewState;
        }
    }
}