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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Guy.Guy _guy;
        private CpuGuyDecorator _cpuGuyDecorator;
        private readonly HumanKeyboardControls _humanKeyboard;
        private bool _useCpuKeyboard;
        private SpriteFont _font;

        public Game1()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
            Window.AllowUserResizing = true;

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
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //
            var cb = Window.ClientBounds;
            _guy = GuyFactory.Create(
                new Vector2(cb.Width/2f, cb.Height),
                Content,
                new DebugLogger());
            //
            _font = Content.Load<SpriteFont>("Consolas");
            _cpuGuyDecorator = new CpuGuyDecorator(_guy, _font);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                keyboardState.IsKeyDown(Keys.Escape))
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

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(CustomColors.Teal);

            //
            spriteBatch.Begin();
            if (_useCpuKeyboard)
            {
                _cpuGuyDecorator.Draw(spriteBatch, gameTime);
            }
            else
            {
                _guy.Draw(spriteBatch,gameTime);
            }
            spriteBatch.DrawString(
                _font,
                string.Format("Press Enter to toggle between Human/CPU controls.  (Currently using \"{0}\" controls)", _useCpuKeyboard ? "CPU" : "Human"),
                new Vector2(5, 5),
                Color.Black);

            spriteBatch.End();
            //

            base.Draw(gameTime);
        }
    }
}