using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GameTools
{
    class VidyaGameScreen : Screen
    {

        private Sprite background;



        // =============================================================================
        // CONSTRUCTOR
        // =============================================================================
        public VidyaGameScreen()
        {
            TransInTime = TimeSpan.FromSeconds(0.2f);
            TransOutTime = TimeSpan.FromSeconds(0.2f);

        }

        // =============================================================================
        // LOAD
        // =============================================================================
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            background = new Sprite(content.Load<Texture2D>(@"Screens/Vidya/background"),Vector2.Zero);
        }

        // =============================================================================
        // UNLOAD
        // =============================================================================
        public override void UnloadContent()
        {
            background = null;
        }

        // =============================================================================
        // HANDLE INPUT
        // =============================================================================
        public override void HandleInput()
        {
            // Escape / Back
            if (InputManager.IsActionTriggered(InputManager.Action.Back))
            {
                popExit();
            }
        }
        // =============================================================================
        // UPDATE
        // =============================================================================
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        // =============================================================================
        // DRAW
        // =============================================================================
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            background.Draw(ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.End();
        }

        // =============================================================================
        // EVENTS
        // =============================================================================
        protected void popExit()
        {
            ScreenManager.AddScreen(new PopVidyaBack("Menu"));
        }

    }
}
