using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GameTools
{
    // this may become abstract
    public class Sprite
    {
        // =============================================================================
        // FIELDPROPS, GET YER FIELDPROPS!  2 FOR TREE-FIDDY
        // =============================================================================
        private readonly Texture2D texture;
        protected Vector2 location;
        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        protected int height;
        public int Height
        {
            get { return height; }
        }

        protected int width;
        public int Width
        {
            get { return width; }
        }
        protected Vector2 velocity;

        private Color alphaColor = Color.White;
        public Color AlphaColor
        {
            get { return alphaColor; }
            set { alphaColor = value; }
        }

        /// <summary>
        /// unlike textures, this rectangle has the position of the texture on the screen
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle((int)location.X, (int)location.Y, width, height); }
        }

        // =============================================================================
        // C0NSTRUCTORS, GIVE EM A BRAKE
        // =============================================================================
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.height = texture.Height;
            this.width = texture.Width;
            this.location = Vector2.Zero;
            this.velocity = Vector2.Zero;
        }

        public Sprite(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            this.location = location;
            this.height = texture.Height;
            this.width = texture.Width;
            this.velocity = Vector2.Zero;
        }

        // =============================================================================
        // DRAW
        // =============================================================================
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, alphaColor);
        }

        // Not Implemented Yet:
        // Update

    }
}
