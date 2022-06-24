using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace monJeu
{
    public class Ghost
    {
        private Vector2 positionG;
        private Texture2D ghostTexture;
        public string GhostColor;
        //public int speed;
        public Rectangle GhostRec
        {
            get
            {
                return new Rectangle((int)positionG.X, (int)positionG.Y, ghostTexture.Width, ghostTexture.Height);
            }
        }
        Random rand = new Random();

        public Ghost(string color/*, int speed*/)
        {
            //this.speed = speed;
            this.GhostColor = color;

            positionG.X = 602;//rand.Next(1024);
            positionG.Y = 620;//rand.Next(768);
        }

        public void LoadGhost(ContentManager content)
        {
            switch (this.GhostColor)
            {
                case "blue":
                    ghostTexture = content.Load<Texture2D>("Ghost_Blue_Right");
                    break;
                case "red":
                    ghostTexture = content.Load<Texture2D>("Ghost_Red_Right");
                    break;
                case "pink":
                    ghostTexture = content.Load<Texture2D>("Ghost_Pink");
                    break;
                case "orange":
                    ghostTexture = content.Load<Texture2D>("Ghost_Orange");
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ghostTexture, positionG, Color.White);
        }
    }
}