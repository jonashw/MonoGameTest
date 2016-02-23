using System;
using Microsoft.Xna.Framework;
using MonoGameTest.Logging;

namespace MonoGameTest.Guy
{
    public class GuyPhysics
    {
        public Vector2 Position;
        public Vector2 Velocity;
        private readonly ILogger _logger;

        public GuyPhysics(Vector2 position, Vector2 velocity, ILogger logger)
        {
            Position = position;
            Velocity = velocity;
            _logger = logger;
        }

        public bool OnGround
        {
            get { return Position.Y >= Guy.ZeroAltitude; }
        }

        public bool Moving
        {
            get
            {
                return Math.Abs(Velocity.X) > DragCoefficient;
            }
        }

        public void Update()
        {
            Position.Y = Math.Min(Position.Y + Velocity.Y, Guy.ZeroAltitude); //Keep Guy above ground.
            Position.X = Math.Max(Position.X + Velocity.X, 0); //Keep Guy right of level.
            _logger.Log(string.Format("Position.Y = {0}; Position.X = {1}", Position.X, Position.Y));
            if (Position.Y < Guy.ZeroAltitude)
            {
                var newYVelocity = Velocity.Y + 1; //Acceleration due to gravity
                _logger.Log(string.Format(
                    "Guy has upward velocity (V0={0}).  Applying downward acceleration. (V1={1})",
                    Velocity.Y,
                    newYVelocity));
                Velocity.Y = newYVelocity;
            }
        }

        private const float DragCoefficient = 0.1f;
        public void Drag()
        {
            //Apply drag
            if (Velocity.X < -DragCoefficient)
            {
                Velocity.X += DragCoefficient;
            }
            if (Velocity.X > DragCoefficient)
            {
                Velocity.X -= DragCoefficient;
            }
            if (!Moving)
            {
                Velocity.X = 0;
            }
        }
    }
}