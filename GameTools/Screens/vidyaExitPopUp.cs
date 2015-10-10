using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GameTools
{
    class PopVidyaBack : MenuScreen
    {

        string message;
        Vector2 messagePosition;

        private Sprite background;
        private Sprite fadedBackground;
        private Texture2D blackButton;
        private static int buttonOffset = 5;   // to be used for spacing the buttons out a little.

        MenuEntry backToMainMenu;
        MenuEntry exitGame;
        MenuEntry cancel;

        public event EventHandler<EventArgs> BackToMainMenu;
        public event EventHandler<EventArgs> ExitGame;
        public event EventHandler<EventArgs> Cancel;


        // ===================================================================
        // CUNTSTRUCTER
        // ===================================================================
        public PopVidyaBack(string message) : base()
        {
            this.message = message;


            IsPopUp = true;

            TransInTime = TimeSpan.FromSeconds(0.2);
            TransOutTime = TimeSpan.FromSeconds(0.2);

            backToMainMenu = new MenuEntry("Main Menu");
            backToMainMenu.Description = "main menu";
            backToMainMenu.Font = Fonts.SegoeMono;
            backToMainMenu.Selected += openMainMenu;
            MenuEntries.Add(backToMainMenu);

            exitGame = new MenuEntry("Exit Game");
            exitGame.Description = "No";
            exitGame.Font = Fonts.SegoeMono;
            exitGame.Selected += closeGame;
            MenuEntries.Add(exitGame);

            cancel = new MenuEntry("Cancel");
            cancel.Description = "cancel";
            cancel.Font = Fonts.SegoeMono;
            cancel.Selected += cancelMenu;
            MenuEntries.Add(cancel);


        }


        // ===================================================================
        // LOADCONTENT
        // ===================================================================
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            blackButton = content.Load<Texture2D>(@"Screens/blackButtonSmall");

            // the popup itself
            background = new Sprite(content.Load<Texture2D>(@"Screens/popUpZiggy")); 
            Vector2 backgroundLocation = new Vector2(
                (viewport.Width - background.Width) / 2, (viewport.Height - background.Height) / 2);
            background.Location = backgroundLocation;


            // now that the popup's background is created, we can find the proper placement for menuEntries
            Vector2 textSize = backToMainMenu.Font.MeasureString(backToMainMenu.Text);
            backToMainMenu.Position = new Vector2((background.Location.X + (background.Width / 2) - (blackButton.Width / 2)),
                background.Location.Y + ((background.Height / 2) - blackButton.Height));
            exitGame.Position = new Vector2(backToMainMenu.Position.X, (backToMainMenu.Position.Y + blackButton.Height + buttonOffset));
            cancel.Position = new Vector2(backToMainMenu.Position.X, (exitGame.Position.Y + blackButton.Height + buttonOffset));

            backToMainMenu.Texture = blackButton;
            exitGame.Texture = blackButton;
            cancel.Texture = blackButton;

            fadedBackground = new Sprite(content.Load<Texture2D>(@"Screens/faded"), Vector2.Zero);
            fadedBackground.AlphaColor = Fonts.FadedBlack; // this probably shouldn't be in fonts.

            texturesToDraw.Add(background);
            texturesToDraw.Add(fadedBackground);


            message = Fonts.BreakTextIntoLines(message, 50, 2);
            Vector2 messagePositionX = Fonts.Peric18.MeasureString(message);
            int mX = (int)messagePositionX.X;

            messagePosition.X = ((background.Location.X + (background.Width / 2)) - (mX / 2));
            messagePosition.Y = background.Location.Y + 10;


            base.LoadContent();
        }

        // ===================================================================
        // UN_LOADCONTENT
        // ===================================================================
        public override void UnloadContent()
        {
            background = null;
            blackButton = null;
            fadedBackground = null;
        }

        // ===================================================================
        // HANDLE INPUT
        // ===================================================================

        public override void HandleInput()
        {
            if (InputManager.IsActionTriggered(InputManager.Action.Back))
            {
                ExitScreen();
                return;
            }
            base.HandleInput();
        }


        // ===================================================================
        // DRAW
        // ===================================================================
        public override void Draw(GameTime gameTime)
        {
            // TODO:  This is a hack for now until I can update MenuScreen to draw this for me.
            // it will need to know if what it is drawing is a Sprite, Texture, SpriteFont, etc.

            base.Draw(gameTime);
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.DrawString(Fonts.Peric18, message, messagePosition, Color.White);
            spriteBatch.End();
        }

        // ===================================================================
        // EVENTS
        // ===================================================================
        void openMainMenu(object sender, EventArgs e)
        {
            ExitScreen();
            ScreenManager.RemoveAllScreens();
            ScreenManager.AddScreen(new MainMenuScreen());
            
        }

        void closeGame(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new MessageBoxScreen("Do you really, really want to quit??"));
        }

        void cancelMenu(object sender, EventArgs e)
        {
            ExitScreen();
        }
    }
}
