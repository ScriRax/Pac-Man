using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace monJeu
{
    public class Ghost
    {
        public string color;
        //public int speed;
        private Vector2 _positionG;
        private Texture2D GhostTexture;
        Random rand = new Random();
        public Rectangle Rectangle;
        public Rectangle PlayerRec
        {
            get
            {
                return new Rectangle((int)_positionG.X, (int)_positionG.Y, GhostTexture.Width, GhostTexture.Height);
            }
        }
        public Ghost(string color/*, int speed*/)
        {
            //this.speed = speed;
            this.color = color;

            _positionG.X = rand.Next(1024);
            _positionG.Y = rand.Next(768);
        }

        public void LoadGhost(ContentManager Content)
        {
            switch (this.color)
            {
                case "blue":
                    GhostTexture = Content.Load<Texture2D>("Ghost_Blue_Right");
                    break;
                case "red":
                    GhostTexture = Content.Load<Texture2D>("Ghost_Red_Right");
                    break;
                case "pink":
                    GhostTexture = Content.Load<Texture2D>("Ghost_Pink");
                    break;
                case "orange":
                    GhostTexture = Content.Load<Texture2D>("Ghost_Orange");
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GhostTexture, _positionG, Color.White);
        }
    }
}