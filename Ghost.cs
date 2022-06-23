using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace monJeu
{
    public class Ghost
    {
        public string color;
        //public int speed;
        private Vector2 _positionG;
        private Texture2D GhostTexture;
        public Ghost(string color/*, int speed*/)
        {
            //this.speed = speed;
            this.color = color;
            _positionG.X = 100;
            _positionG.Y = 100;
        }

        public void LoadGhost(ContentManager Content)
        {
            GhostTexture = Content.Load<Texture2D>("Ghost_Red_Right");
            // switch(color) {
            //     case "blue":
            //     GhostTexture = Content.Load<Texture2D>("Ghost_Blue");
            //     break;
            //     case "red":
            //     GhostTexture = Content.Load<Texture2D>("Ghost_Red_Right");
            //     break;
            //     case "pink":
            //     GhostTexture = Content.Load<Texture2D>("Ghost_Pink");
            //     break;
            //     case "orange":
            //     GhostTexture = Content.Load<Texture2D>("Ghost_Orange");
            //     break;
            // }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GhostTexture,_positionG,Color.White);
        }
    }
}