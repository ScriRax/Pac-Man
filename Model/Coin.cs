using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monJeu
{
    public class Coin
    {
        private Texture2D texture;
        private Color CoinColor = Color.White;
        public Vector2 Position;
        public Coin(Texture2D interTexture)
        {
            texture = interTexture;
        }
        public Rectangle CoinRec
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 25, 25);
            }
        }

        public void Draw(SpriteBatch wallsBatch)
        {
            wallsBatch.Draw(texture, Position, null ,CoinColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}