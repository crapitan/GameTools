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


        public MainMenuScreen() : base()
        {

            exitGame_MenuEntry = new MenuEntry("Exit Game");
            exitGame_MenuEntry.Description = "Exit the game.";
            exitGame_MenuEntry.Font = Fonts.DefaultFont;
            exitGame_MenuEntry.Position = new Vector2(150, 50);
            exitGame_MenuEntry.Selected += ExitGame;
            MenuEntries.Add(exitGame_MenuEntry);

            doSomething_MenuEntry = new MenuEntry("Do Something");
            doSomething_MenuEntry.Description = "Do something, ya'll";
            doSomething_MenuEntry.Font = Fonts.DefaultFont;
            doSomething_MenuEntry.Position = new Vector2(150, 165);
            doSomething_MenuEntry.Selected += DoSomething;
            MenuEntries.Add(doSomething_MenuEntry);

        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            //background = content.Load<Texture2D>(@"Screens/bg_blueGrunge");
            background = new Sprite(content.Load<Texture2D>(@"Screens/bg_blueGrunge"), Vector2.Zero);
            blackButton = content.Load<Texture2D>(@"Screens/blackButton");
            exitGame_MenuEntry.Texture = blackButton;
            doSomething_MenuEntry.Texture = blackButton;

            texturesToDraw.Add(background);

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            background = null;
            blackButton = null;
        }

        public override void HandleInput()
        {
            if (InputManager.IsActionTriggered(InputManager.Action.Back))
            {
                //AudioManager.PopMusic();
                ExitScreen();
                return;
            }

           

            base.HandleInput();
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

        void ExitGame(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new MessageBoxScreen("Do you really, really want to quit??"));
        }

        void DoSomething(object sender, EventArgs e)
        {
            Console.WriteLine("DO SOMETHING!");
        }
    }
}
