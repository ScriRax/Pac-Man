using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monJeu
{
    public class Walls
    {
        private Texture2D texture;
        private Color wallColor = Color.White;
        public Vector2 Position;
        public Walls(Texture2D wallTexture)
        {
            texture = wallTexture;
        }
        public Rectangle WallRec
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 70, 70);
            }
        }

        public void Draw(SpriteBatch wallsBatch)
        {
            wallsBatch.Draw(texture, Position, null, wallColor, 0f,
                Vector2.Zero, 0.3f, SpriteEffects.None, 0f);
        }
    }
}