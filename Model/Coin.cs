using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monJeu
{
    public class Coin
    {
        private Texture2D _texture;
        private Color _coinColor = Color.White;
        public Vector2 Position { get; set; }
        public Coin(Texture2D interTexture)
        {
            _texture = interTexture;
        }
        public Rectangle CoinRec
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 25, 25);
            }
            set { }
        }

        public void Draw(SpriteBatch wallsBatch)
        {
            wallsBatch.Draw(_texture, Position, null, _coinColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}