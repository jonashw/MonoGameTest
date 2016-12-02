using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
    public class Camera
    {
        private Viewport _viewport;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; } = 0;
        public float Zoom { get; set; } = 1;

        private readonly Vector2 _origin;
        private readonly Vector2 _originalPosition;

        public Camera(Viewport viewport, Vector2? position = null)
        {
            _viewport = viewport;
            _origin =  new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = _originalPosition = position ?? new Vector2(0, 0);
        }

        public void Reset()
        {
            Position = _originalPosition;
            Rotation = 0;
            Zoom = 1;
        }

        public Matrix GetViewMatrix() =>
            Matrix.CreateTranslation(new Vector3(-Position, 0.0f))
            *Matrix.CreateTranslation(new Vector3(-_origin, 0.0f))
            *Matrix.CreateRotationZ(Rotation)
            *Matrix.CreateScale(Zoom, Zoom, 1)
            *Matrix.CreateTranslation(new Vector3(_origin, 0.0f));

        public Rectangle GetVisibleArea()
        {   //This implementation was adapted from http://gamedev.stackexchange.com/a/59450/18518.
            var inverseViewMatrix = Matrix.Invert(GetViewMatrix());
            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(_viewport.Width, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, _viewport.Height), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(_viewport.Width, _viewport.Height), inverseViewMatrix);
            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            return new Rectangle((int) min.X, (int) min.Y, (int) (max.X - min.X), (int) (max.Y - min.Y));
        }
    }
}