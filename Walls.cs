using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace monJeu
{
    public class Walls
    {
        protected Texture2D _texture;

        public Vector2 Position;

        public Color Color = Color.White;

        public Rectangle Rectangle;

        public Walls(Texture2D texture)
        {
            _texture = texture;
        }
        public Rectangle WallRec
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 75, 75);
            }
        }

        public virtual void Update(GameTime gametime, List<Walls> walls)
        {

        }

        public virtual void Draw(SpriteBatch WallsBatch)
        {
            WallsBatch.Draw(_texture, Position, null, Color.White, 0f,
                Vector2.Zero, 0.3f, SpriteEffects.None, 0f);
        }
    }


}