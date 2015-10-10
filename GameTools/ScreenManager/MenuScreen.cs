using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameTools
{
    abstract class MenuScreen : Screen
    {
        List<MenuEntry> menuEntries = new List<MenuEntry>();
        protected int selectedEntry = 0;
        protected IList<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }

        protected MenuEntry SelectedMenuEntry
        {
            get
            {
                if (selectedEntry < 0 || selectedEntry >= menuEntries.Count)
                {
                    return null;
                }
                return menuEntries[selectedEntry];
            }
        }

        // this exists so that we only need to call spritebatch.begin once for menuscreens.
        // each menuscreen, like mainmenuscreen, can package up all it's textures and assign them
        // here to have the functionality in one place.
        protected List<Sprite> texturesToDraw = new List<Sprite>();

        // =============================================================================
        // CONSTRUCT YOSELF
        // =============================================================================
        public MenuScreen()
        {
            TransInTime = TimeSpan.FromSeconds(3);
            TransOutTime = TimeSpan.FromSeconds(3);
        }


        // =============================================================================
        // HANDLE INPUT
        // =============================================================================
        public override void HandleInput()
        {
            int oldSelectedEntry = selectedEntry;
            bool buttonClicked = false;


            // Mouse Input
            for (int i = 0; i < menuEntries.Count; i++)
            {
                if (menuEntries[i].Bounds.Contains(InputManager.GetMouseCoords()))
                {
                    // the mouse is over one of the menu entries, select it
                    selectedEntry = i;
                }
                if (InputManager.IsMouseTriggered() && menuEntries[i].Bounds.Contains(InputManager.GetMouseCoords()))
                {
                    buttonClicked = true;
                }
            }



            // Move UP an entry
            if(InputManager.IsActionTriggered(InputManager.Action.CursorUp))
            {
                selectedEntry--;
                if(selectedEntry < 0)
                {
                    selectedEntry = menuEntries.Count - 1;
                }
            }
            // Move DOWN an entry
            if(InputManager.IsActionTriggered(InputManager.Action.CursorDown))
            {
                selectedEntry++;
                if(selectedEntry >= menuEntries.Count)
                {
                    selectedEntry = 0;
                }
            }
            // Accept or cancel menu entry
            if(InputManager.IsActionTriggered(InputManager.Action.Ok) || buttonClicked)
            {
                // TODO: Add audio support to play a sound
                OnSelectEntry(selectedEntry);
            }
            else if (InputManager.IsActionTriggered(InputManager.Action.Back) ||
                InputManager.IsActionTriggered(InputManager.Action.ExitGame))
            {
                ScreenManager.AddScreen(new MessageBoxScreen("Do you really, really want to quit??"));
            }
            else if (selectedEntry != oldSelectedEntry)
            {
                // TODO: play menu move
            }
        }



        // =============================================================================
        // UPDATE
        // =============================================================================

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // update each nested MenuEntry object
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);
                menuEntries[i].Update(this, isSelected, gameTime);
            }
        }


        // =============================================================================
        // DRAW
        // =============================================================================
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();

            // draw non-menuEnties thingies
            foreach (Sprite sprite in texturesToDraw)
            {
                sprite.Draw(ScreenManager.SpriteBatch);
            }

            // draw menuEntries
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];
                bool isSelected = IsActive && (i == selectedEntry);
                menuEntry.Draw(this, isSelected, gameTime);
            }
            ScreenManager.SpriteBatch.End();
        }


        // =============================================================================
        // EVENTS
        // =============================================================================
        protected virtual void OnSelectEntry(int entryIndex)
        {
            menuEntries[selectedEntry].OnSelectEntry();
        }




        protected virtual void OnCancel()
        {
            ExitScreen();
        }



        protected void OnCancel(object sender, EventArgs e)
        {
            OnCancel();
        }


    }
}
