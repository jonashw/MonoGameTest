using System;
using Microsoft.Xna.Framework;
using MonoGameTest.Logging;

namespace MonoGameTest.Guy
{
    public class GuyPhysics
    {
        public Vector2 Position;
        private Vector2 _velocity;
        public readonly int Width; 
        public readonly int Height; 
        private readonly ILogger _logger;
        private readonly int _zeroAltitude;

        public GuyPhysics(Vector2 position, Vector2 velocity, int width, int height, ILogger logger)
        {
            Position = position;
            _velocity = velocity;
            Width = width;
            Height = height;
            _logger = logger;
            _zeroAltitude = 0;
        }

        public void Update()
        {
            Position.X = Position.X + _velocity.X;
            Position.Y = Math.Min(Position.Y + _velocity.Y, _zeroAltitude); //Keep Guy above ground.
            //_logger.Log($"Position.X = {Position.X}; Position.Y = {Position.Y}");
            //_logger.Log(string.Format("Velocity.X = {0}; Velocity.Y = {1}", _velocity.X, _velocity.Y));

            if (Position.Y >= _zeroAltitude)
            {
                return;
            }

            var newYVelocity = _velocity.Y + 1; //Acceleration due to gravity
            /*
            _logger.Log(string.Format(
                "Guy has upward velocity (V0={0}).  Applying downward acceleration. (V1={1})",
                _velocity.Y,
                newYVelocity));
            */
            _velocity.Y = newYVelocity;
        }

        private const float DecelerationStep = 0.3f;
        private const float AccelerationStep = 2 * DecelerationStep;
        public void StepDecelerate()
        {
            if (_velocity.X < -DecelerationStep)
            {
                _velocity.X += DecelerationStep;
            }
            else if (_velocity.X > DecelerationStep)
            {
                _velocity.X -= DecelerationStep;
            }
            else
            {
                _velocity.X = 0;
            }
        }

        public void StepAccelerate(int maxVelocity)
        {
            if (maxVelocity < 0)
            {
                if (maxVelocity < _velocity.X)
                {
                    _velocity.X -= AccelerationStep;
                }
            }
            else if(maxVelocity > 0)
            {
                if (_velocity.X < maxVelocity)
                {
                    _velocity.X += AccelerationStep;
                }
            }
        }

        public void Stop()
        {
            _velocity.X = 0;
        }

        public void SetYVelocity(int v)
        {
            _velocity.Y = v;
        } 

        public bool IsMovingHorizontally => Math.Abs(_velocity.X) > 0;
        public bool IsOnGround           => Position.Y >= _zeroAltitude;
        public XDirection? HorizontalMovementDirection =>
            IsMovingHorizontally
                ? _velocity.X > 0
                    ? XDirection.Right
                    : XDirection.Left
                : (XDirection?) null;

        public float Right => Position.X + Width;
        public float Left => Position.X;
    }
}