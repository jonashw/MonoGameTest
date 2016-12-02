using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest.Controls;
using MonoGameTest.Guy;
using MonoGameTest.Logging;

namespace MonoGameTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Guy.Guy _guy;
        private CpuGuyDecorator _cpuGuyDecorator;
        private readonly HumanKeyboardControls _humanKeyboard;
        private bool _useCpuKeyboard;
        private SpriteFont _font;
        private Camera _camera;
        private CameraController _cameraController;

        public Game1()
        {
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
            Window.AllowUserResizing = true;
            IsMouseVisible = true;

            _humanKeyboard = new HumanKeyboardControls();
            KeyboardEventRegistry.OnKeyDown(Keys.Enter, () =>
            {
                if (_useCpuKeyboard)
                {
                    _cpuGuyDecorator.Deactivate();
                }
                else
                {
                    _cpuGuyDecorator.Activate();
                }
                _useCpuKeyboard = !_useCpuKeyboard;
            });

            KeyboardEventRegistry.OnKeyDown(Keys.B, () =>
            {
                _guy.BoundingBox.ShouldDraw = !_guy.BoundingBox.ShouldDraw;
            });
        }

        protected override void Initialize()
        {
            _camera = new Camera(GraphicsDevice.Viewport, position: new Vector2(0, -_graphics.PreferredBackBufferHeight/2f));
            _cameraController = new CameraController(_camera);
            base.Initialize();
        } 

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //
            var cb = Window.ClientBounds;
            _guy = GuyFactory.Create(
                GraphicsDevice,
                Content,
                new DebugLogger(),
                new Vector2(0,0));
            //
            _font = Content.Load<SpriteFont>("Consolas");
            _cpuGuyDecorator = new CpuGuyDecorator(_guy, _font);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            KeyboardEventRegistry.Update(keyboardState);
            if (_useCpuKeyboard)
            {
                _cpuGuyDecorator.Update(gameTime);
            }
            else
            {
                _guy.Update(gameTime, keyboardState);
            }


            keepGuyOnCamera(_guy, _camera);

            _cameraController.Update(keyboardState);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(CustomColors.Teal);

            //
            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            if (_useCpuKeyboard)
            {
                _cpuGuyDecorator.Draw(_spriteBatch, gameTime);
            }
            else
            {
                _guy.Draw(_spriteBatch, gameTime);
            }
            _spriteBatch.DrawString(_font, $"Press Enter to toggle between Human/CPU controls.  (Currently using \"{(_useCpuKeyboard ? "CPU" : "Human")}\" controls)",
                _camera.Position + new Vector2(5, 5),
                Color.Black);

            _spriteBatch.DrawString(_font, $"Press B to toggle drawing of bounding boxes.  (Currently {(_guy.BoundingBox.ShouldDraw ? "ON" : "OFF")})",
                _camera.Position + new Vector2(5, 25),
                Color.Black);

            _spriteBatch.DrawString(_font, $"Use HJKL to manipulate the camera. Shift-R to reset. [z]oom, [p]osition, [r]otation. (Currently: {_cameraController.State})",
                _camera.Position + new Vector2(5, 45),
                Color.Black);

            _spriteBatch.End();
            //

            base.Draw(gameTime);
        }

        private static void keepGuyOnCamera(Guy.Guy guy, Camera camera)
        {
            //Make the level wrap around, horizontally.
            var cameraRect = camera.GetVisibleArea();
            if (guy.Physics.Right < cameraRect.Left)
            {
                guy.Physics.Position.X = cameraRect.Right;
            }
            if (guy.Physics.Left > cameraRect.Right + 1)
            {
                //Wrap from the right to the left.
                guy.Physics.Position.X = cameraRect.Left - guy.Physics.Width;
            }
        }
    }
}