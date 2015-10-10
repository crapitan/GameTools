using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTools
{
    public enum ScreenState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }

    public abstract class Screen
    {
        //==============================================================
        // FIELDS AND PROPERTIES
        //==============================================================



        // If the screen is a popup, other screens don't need to transition off.
        bool isPopUp = false;
        public bool IsPopUp
        {
            get { return isPopUp; }
            protected set { isPopUp = value; }
        }

        /// The amount of time it takes to transition on to the screen
        TimeSpan transInTime = TimeSpan.Zero;
        public TimeSpan TransInTime
        {
            get { return transInTime; }
            protected set { transInTime = value; }
        }


        /// The amount of time it takes to transition off the screen
        TimeSpan transOutTime = TimeSpan.Zero;
        public TimeSpan TransOutTime
        {
            get { return transOutTime; }
            protected set { transOutTime = value; }
        }

        // 0 = fully active, 1 = gone
        float transitionPosition = 1;
        public float TransitionPosition
        {
            get { return transitionPosition;  }
            protected set{ transitionPosition = value; }
        }

        // 255 = fully active, 0 = gone
        public byte TransitionAlpha
        {
            get  { return (byte)(255 - transitionPosition * 255); }
        }

        ScreenState screenState = ScreenState.TransitionOn;
        public ScreenState ScreenState
        {
            get { return screenState; }
            protected set { screenState = value; }
        }


        // properties for exiting the screen
        public EventHandler Exiting;

        bool isExiting = false;
        public bool IsExiting
        {
            get { return isExiting; }
            protected internal set
            {
                bool fireEvent = !isExiting && value;
                isExiting = value;
                if (fireEvent && (Exiting != null))
                {
                    Exiting(this, EventArgs.Empty);
                }
            }
        }

        bool otherScreenHasFocus;
        public bool IsActive
        {
            get
            {
                // return false unless we have focus and we need to be either active or transitioning
                return !otherScreenHasFocus && 
                    (screenState == ScreenState.TransitionOn || screenState == ScreenState.Active);
            }
        }

        // the manager that this screen belongs to.
        ScreenManager screenManager;

        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            internal set { screenManager = value; }
        }

        //==============================================================
        // METHODS
        //==============================================================

        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }


        //==============================================================
        // UPDATE()
        //==============================================================
        public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            this.otherScreenHasFocus = otherScreenHasFocus;

            if(IsExiting)
            {
                screenState = ScreenState.TransitionOff;
                if(!UpdateTransition(gameTime,transOutTime, 1))
                {
                    ScreenManager.RemoveScreen(this);
                }
            }
            else if (coveredByOtherScreen)
            {
                // if this screen is covered by some other screen, we should transition off
                if(UpdateTransition(gameTime, transOutTime, 1))
                {
                    // still transitioning off
                    screenState = ScreenState.TransitionOff;
                } else
                {
                    screenState = ScreenState.Hidden;
                }
            }
            else
            {
                // just become active if we aren't going away
                if(UpdateTransition(gameTime, transInTime, -1))
                {
                    screenState = ScreenState.TransitionOn;
                } else
                {
                    screenState = ScreenState.Active;
                }

            }

        }

        // false = done
        // true = in progress
        bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
        {
            // how much to move
            float transitionDelta;
            if(time == TimeSpan.Zero)
            {
                transitionDelta = 1;
            } else
            {
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);
            }

            // update trans position
            transitionPosition += transitionDelta * direction;
            if (transitionPosition <= 0 || transitionPosition >=1)
            {
                transitionPosition = MathHelper.Clamp(transitionPosition, 0, 1);
                // done transitioning
                return false;
            }
            // still busy transitioning
            return true;
            
        }

        public virtual void HandleInput() { }




        public virtual void Draw(GameTime gameTime) { }

        public void ExitScreen()
        {
            // flag that it should transition off and then exit.
            isExiting = true;
            // If the screen has a zero transition time, remove it immediately.
            if (TransOutTime == TimeSpan.Zero)
            {
                ScreenManager.RemoveScreen(this);
            }
        }



    }
}
