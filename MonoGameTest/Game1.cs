using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest.Controls;
using MonoGameTest.Controls.AI;
using MonoGameTest.Guy;
using MonoGameTest.Logging;

namespace MonoGameTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Guy.Guy _guy;
        private readonly CpuKeyboardControls _cpuKeyboard;
        private readonly HumanKeyboardControls _humanKeyboard;
        private bool _useCpuKeyboard;

        public Game1()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
            Window.AllowUserResizing = true;

            _cpuKeyboard = new CpuKeyboardControls(new []
            {
                new KeyPress(Keys.Right, TimeSpan.FromMilliseconds(900)), 
                new KeyPress(Keys.Left,  TimeSpan.FromMilliseconds(900)) 
            }, true);

            _humanKeyboard = new HumanKeyboardControls();
        }

        private IKeyboardControls getKeyboardControls()
        {
            return _useCpuKeyboard
                ? (IKeyboardControls) _cpuKeyboard
                : _humanKeyboard;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //IsFixedTimeStep = false;
            base.Initialize();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //
            var cb = Window.ClientBounds;
            _guy = GuyFactory.Create(
                new Vector2(cb.Width/2f, cb.Height),
                Content,
                new DebugLogger());
            //
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _useCpuKeyboard = !_useCpuKeyboard;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            //
            _guy.Update(gameTime, getKeyboardControls().GetState(gameTime));
            keepGuyOnScreen();
            //

            base.Update(gameTime);
        }

        private void keepGuyOnScreen()
        {
            //Make the level wrap around, horizontally.
            if (_guy.Physics.Position.X < 0)
            {
                _guy.Physics.Position.X = 1280 + _guy.Physics.Width;
            }
            if (_guy.Physics.Position.X > 1280 + _guy.Physics.Width)
            {
                _guy.Physics.Position.X = 0;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(CustomColors.Teal);

            //
            spriteBatch.Begin();
            _guy.Draw(spriteBatch,gameTime);
            spriteBatch.End();
            //

            base.Draw(gameTime);
        }
    }

    public static class CustomColors
    {
        public static readonly Color Teal = new Color(38, 165, 153);
        public static readonly Color TealDark = new Color(28,124,114);
        public static readonly Color Blue = new Color(85,102,194);
        public static readonly Color BlueDark = new Color(56,72,153);
    }
}