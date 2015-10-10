using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameTools
{
    public class ScreenManager : DrawableGameComponent
    {
        List<Screen> screens = new List<Screen>();
        List<Screen> screensToUpdate = new List<Screen>();

        bool isInitialized;
        bool traceEnabled;

        SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }

        public int NumberOfScreens
        {
            get { return screens.Count; }
        }

        new public Game Game
        {
            get { return base.Game; }
        }


        // =========================================================================================
        // CONSTRUCTOR FLEEET (vogon) 
        // =========================================================================================
        public ScreenManager(Game game) : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            isInitialized = true;
        }

        // =========================================================================================
        // LOADCONTENT 
        // =========================================================================================
        // I belive this method is probably useless.
        // When a new screen is added with AddScreen(), that
        // screen's LoadContent() is called at that time.
        protected override void LoadContent()
        {

        }
        // =========================================================================================
        // UNLOAD 
        // =========================================================================================
        protected override void UnloadContent()
        {
            foreach (Screen screen in screens)
            {
                screen.UnloadContent();
            }
        }
        // =========================================================================================
        // UPDATE 
        // =========================================================================================
        public override void Update(GameTime gameTime)
        {
            screensToUpdate.Clear();

            foreach (Screen screen in screens)
            {
                screensToUpdate.Add(screen);
            }
            bool otherScreenHasFocus = false;
            // TODO:  YOU MAY NEED TO REVISIT THIS WHEN ADDING NEW SCREENS!!!!!!!!!!
            // ^^ originally Game.IsActive
            bool coveredByOtherScreen = false;

            while (screensToUpdate.Count > 0)
            {
                Screen screen = screensToUpdate[screensToUpdate.Count - 1];
                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
                if (screen.ScreenState == ScreenState.TransitionOn || screen.ScreenState == ScreenState.Active)
                {
                    // if this is the first encoutered active screen, let it handle the input
                    if(!otherScreenHasFocus)
                    {
                        screen.HandleInput();
                        otherScreenHasFocus = true;
                    }
                    // if this screen isn't a popup inform all other screens they are covered
                    if (!screen.IsPopUp)
                    {
                        coveredByOtherScreen = true;
                    }
                }
                if (traceEnabled)
                {
                    TraceScreens();
                }
            }
        }
        // =========================================================================================
        // TRACE 
        // =========================================================================================
        void TraceScreens()
        {
            List<String> screenNames = new List<String>();
            foreach (Screen screen in screens)
            {
                screenNames.Add(screen.GetType().Name);
            }
        }


        // =========================================================================================
        // DRAW 
        // =========================================================================================
        public override void Draw(GameTime gameTime)
        {
            foreach (Screen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                {
                    continue;
                }
                else
                {
                    screen.Draw(gameTime);
                }
            }
        }

        // =========================================================================================
        // ADD SCREEN 
        // =========================================================================================
        public void AddScreen(Screen screen)
        {
            screen.ScreenManager = this;
            screen.IsExiting = false;
            if (isInitialized)
            {
                screen.LoadContent();
            }
            screens.Add(screen);
        }
        // =========================================================================================
        // REMOVE SCREEN 
        // =========================================================================================
        public void RemoveScreen(Screen screen)
        {
            if (isInitialized)
            {
                screen.UnloadContent();
            }
            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }

        // =========================================================================================
        // GET array - not used currently 
        // =========================================================================================
        public Screen[] GetScreens()
        {
            return screens.ToArray();
        }
    }
}
