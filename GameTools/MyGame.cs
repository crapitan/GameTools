using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// =======================================
// INFO:   This little framework is based on an example
// originally found within the XNA community platform.
// The copy I found was very broken.  
// I have updated to make it work, and added mouse support.
// =======================================

namespace GameTools
{
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
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Fonts.UnloadContent();
        }





        // =========================================================================================
        // UPDATE 
        // =========================================================================================
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
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            // TODO: Add your drawing code here
            screenManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
