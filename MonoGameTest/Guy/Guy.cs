using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest.Guy.States;
using MonoGameTest.Logging;

namespace MonoGameTest.Guy
{
    public class Guy
    {
        public Vector2 Position;
        public Vector2 Velocity;
        internal readonly GuyStates States;
        public bool FacingRight = true;
        private IGuyState _state;
        private readonly ILogger _logger;

        public Guy(Vector2 position, Texture2D idleSprite, Texture2D jumpingSprite, Texture2D runningSprite, ILogger logger)
        {
            const int width = 500;
            const int height = 667;
            const float scale = 0.5f;
            Position = position;
            Velocity = new Vector2(0,0);
            States = new GuyStates(
                new GuyIdleState(new EasySprite(idleSprite, width, height, scale)),
                new GuyRunningState(new EasySpriteAnimation(runningSprite, width, height, 6, 2, 0.08f, scale)),
                new GuyJumpingState(new EasySprite(jumpingSprite, width, height, scale)));
            _state = States.Idle;
            _logger = logger;
        }

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
            //
            Position.Y = Math.Min(Position.Y + Velocity.Y, 720); //Keep Guy above ground.
            _logger.Log("Position.Y = " + Position.Y);
            if (Position.Y < 720)
            {
                var newYVelocity = Velocity.Y + 1; //Acceleration due to gravity
                _logger.Log(string.Format(
                    "Guy has upward velocity (V0={0}).  Applying downward acceleration. (V1={1})",
                    Velocity.Y,
                    newYVelocity));
                Velocity.Y = newYVelocity;
            }
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