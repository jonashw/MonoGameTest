using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest
{
    public class CameraController
    {
        public CameraControlState State { get; private set; } = CameraControlState.Position;
        private readonly Camera _camera;
        private readonly Vector2 _panIncrementX = new Vector2(5,0);
        private readonly Vector2 _panIncrementY = new Vector2(0,5);
        private readonly float _rotationIncrement = 0.01f;
        private readonly float _zoomIncrement = 0.01f;

        public CameraController(Camera camera)
        {
            _camera = camera;
        }

        public void Update(KeyboardState ks)
        {
            switch (State)
            {
                case CameraControlState.Position:
                    if (ks.IsKeyDown(Keys.H)) _camera.Position += _panIncrementX;
                    if (ks.IsKeyDown(Keys.L)) _camera.Position -= _panIncrementX;
                    if (ks.IsKeyDown(Keys.J)) _camera.Position -= _panIncrementY;
                    if (ks.IsKeyDown(Keys.K)) _camera.Position += _panIncrementY;
                    break;
                case CameraControlState.Rotation:
                    if (ks.IsKeyDown(Keys.H)) _camera.Rotation -= _rotationIncrement;
                    if (ks.IsKeyDown(Keys.L)) _camera.Rotation += _rotationIncrement;
                    break;
                case CameraControlState.Zoom:
                    if (ks.IsKeyDown(Keys.H)) _camera.Zoom -= _zoomIncrement;
                    if (ks.IsKeyDown(Keys.L)) _camera.Zoom += _zoomIncrement;
                    break;
            }
            if (ks.IsKeyDown(Keys.P)) State = CameraControlState.Position;
            if (ks.IsKeyDown(Keys.Z)) State = CameraControlState.Zoom;
            if (ks.IsKeyDown(Keys.R))
            {
                if(ks.IsKeyDown(Keys.LeftShift))
                {
                    _camera.Reset();
                } else
                {
                    State = CameraControlState.Rotation;
                }
            }
        }

        public enum CameraControlState
        {
            Position,
            Rotation,
            Zoom
        }
    }
}