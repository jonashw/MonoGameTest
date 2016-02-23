using System;
using Microsoft.Xna.Framework;
using MonoGameTest.Logging;

namespace MonoGameTest.Guy
{
    public class GuyPhysics
    {
        public Vector2 Position;
        private Vector2 _velocity;
        private readonly int _width; 
        private readonly int _height; 
        private readonly ILogger _logger;

        public GuyPhysics(Vector2 position, Vector2 velocity, int width, int height, ILogger logger)
        {
            Position = position;
            _velocity = velocity;
            _width = width;
            _height = height;
            _logger = logger;
        }

        public void Update()
        {
            Position.X = Position.X + _velocity.X;
            Position.Y = Math.Min(Position.Y + _velocity.Y, Guy.ZeroAltitude); //Keep Guy above ground.
            _logger.Log(string.Format("Position.X = {0}; Position.Y = {1}", Position.X, Position.Y));
            _logger.Log(string.Format("Velocity.X = {0}; Velocity.Y = {1}", _velocity.X, _velocity.Y));

            if (Position.Y >= Guy.ZeroAltitude)
            {
                return;
            }

            var newYVelocity = _velocity.Y + 1; //Acceleration due to gravity
            _logger.Log(string.Format(
                "Guy has upward velocity (V0={0}).  Applying downward acceleration. (V1={1})",
                _velocity.Y,
                newYVelocity));
            _velocity.Y = newYVelocity;
        }

        public bool IsOnGround
        {
            get { return Position.Y >= Guy.ZeroAltitude; }
        }

        private const float DragCoefficient = 0.1f;
        private const float AccelerationCoefficient = 2 * DragCoefficient;
        public void Drag()
        {
            //Apply drag
            if (_velocity.X < -DragCoefficient)
            {
                _velocity.X += DragCoefficient;
            }
            else if (_velocity.X > DragCoefficient)
            {
                _velocity.X -= DragCoefficient;
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
                    _velocity.X -= AccelerationCoefficient;
                }
            }
            else if(maxVelocity > 0)
            {
                if (_velocity.X < maxVelocity)
                {
                    _velocity.X += AccelerationCoefficient;
                }
            }
        }

        public bool IsMovingHorizontally
        {
            get
            {
                return Math.Abs(_velocity.X) > DragCoefficient;
            }
        }

        public void SetYVelocity(int v)
        {
            _velocity.Y = v;
        }
    }
}