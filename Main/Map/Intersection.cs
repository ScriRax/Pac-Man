using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monJeu
{
    public class Intersection
    {
        private Texture2D texture;
        private Color interColor = Color.White;
        public Vector2 Position;
        public Intersection(Texture2D interTexture)
        {
            texture = interTexture;
        }
        public Rectangle WallRec
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 25, 25);
            }
            set { }
        }

        // public void Draw(SpriteBatch wallsBatch)
        // {
        //     wallsBatch.Draw(texture, Position, null ,interColor, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
        // }
    }
}