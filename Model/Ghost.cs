using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace monJeu
{
    public class Ghost
    {
        public Vector2 PositionG;
        public Texture2D GhostTexture;
        public string GhostColor;
        //public int speed;
        public Vector2 Velocity = new Vector2(1,0);
        public Rectangle GhostRec
        {
            get
            {
                return new Rectangle((int)PositionG.X, (int)PositionG.Y, 30, 30);
            }
        }
        Random rand = new Random();

        public Ghost(string color)
        {
            this.GhostColor = color;
            PositionG.X = 602;
            PositionG.Y = 620;
        }

        public void LoadGhost(ContentManager content)
        {
            switch (this.GhostColor)
            {
                case "blue":
                    GhostTexture = content.Load<Texture2D>("Ghost_Blue_Right");
                    break;
                case "red":
                    GhostTexture = content.Load<Texture2D>("Ghost_Red_Right");
                    break;
                case "pink":
                    GhostTexture = content.Load<Texture2D>("Ghost_Pink_Right");
                    break;
                case "orange":
                    GhostTexture = content.Load<Texture2D>("Ghost_Orange_Right");
                    break;
            }
        }

        // public void UpdateLocation()
        // {
        //     Velocity.X = 18;
        // }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GhostTexture, PositionG, null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
        }
    }
}