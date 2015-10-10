using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameTools
{
    class MainMenuScreen : MenuScreen
    {
        private Sprite background;

        private Texture2D blackButton;
        

        MenuEntry exitGame_MenuEntry;
        MenuEntry doSomething_MenuEntry;

        // =========================================================================================================
        // CONSTRUCTOR
        // =========================================================================================================
        public MainMenuScreen() : base()
        {

            doSomething_MenuEntry = new MenuEntry("Load Game");
            doSomething_MenuEntry.Description = "Load Game";
            doSomething_MenuEntry.Font = Fonts.DefaultFont;
            doSomething_MenuEntry.Position = new Vector2(150, 50);
            doSomething_MenuEntry.Selected += LoadGame;
            MenuEntries.Add(doSomething_MenuEntry);

            exitGame_MenuEntry = new MenuEntry("Exit Game");
            exitGame_MenuEntry.Description = "Exit the game.";
            exitGame_MenuEntry.Font = Fonts.DefaultFont;
            exitGame_MenuEntry.Position = new Vector2(150, 165);
            exitGame_MenuEntry.Selected += ExitGame;
            MenuEntries.Add(exitGame_MenuEntry);



        }

        // =========================================================================================================
        // LOADCONTENT
        // =========================================================================================================
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            //background = content.Load<Texture2D>(@"Screens/bg_blueGrunge");
            background = new Sprite(content.Load<Texture2D>(@"Screens/bg_blueGrunge"), Vector2.Zero);
            blackButton = content.Load<Texture2D>(@"Screens/blackButton");

            doSomething_MenuEntry.Texture = blackButton;
            exitGame_MenuEntry.Texture = blackButton;

            texturesToDraw.Add(background);

            base.LoadContent();
        }

        // =========================================================================================================
        // UNLOAD CONTENT
        // =========================================================================================================
        public override void UnloadContent()
        { 
            background = null;
            blackButton = null;
        }

        // =========================================================================================================
        // HANDLE INPUT
        // =========================================================================================================
        public override void HandleInput()
        {

            base.HandleInput();
        }

        // =========================================================================================================
        // DRAW
        // =========================================================================================================
        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

        // =========================================================================================================
        // EVENTS
        // =========================================================================================================
        void ExitGame(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new MessageBoxScreen("Do you really, really want to quit??"));
        }

        void LoadGame(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new VidyaGameScreen());
            ScreenManager.RemoveScreen(this);
            
        }
    }
}
