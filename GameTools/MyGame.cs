using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTools
{
    

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
    public class MyGame : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;

        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";

            

        }


        // =========================================================================================
        // INIT 
        // =========================================================================================
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        // =========================================================================================
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            InputManager.Initialize();
            IsMouseVisible = true;
            base.Initialize();

            
        }




        // =========================================================================================
        // LOAD 
        // =========================================================================================
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Fonts.LoadContent(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager = new ScreenManager(this);
            screenManager.AddScreen(new MainMenuScreen());
            // screenManager.LoadContent();
            // base.LoadContent();
            // TODO: use this.Content to load your game content here
        }


        // =========================================================================================
        // UNLOAD 
        // =========================================================================================
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Fonts.UnloadContent();
        }





        // =========================================================================================
        // UPDATE 
        // =========================================================================================
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //  Exit();
            
            InputManager.Update();
            screenManager.Update(gameTime);

            if (screenManager.NumberOfScreens <= 0)
            {
                Exit();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }




        // =========================================================================================
        // DRAW 
        // =========================================================================================
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            // TODO: Add your drawing code here
            screenManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
