using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace monJeu
{
    enum SpriteState
    {
        Left,
        Right,
        Top,
        Down,
        Idle
    }
    public class Pacman
    {
        public Texture2D PacmanTextureRight;
        public Texture2D PacmanTextureLeft;
        public Texture2D PacmanTextureIdle;
        public Texture2D PacmanTextureTop;
        public Texture2D PacmanTextureDown;
        public Vector2 Position;
        public Vector2 Velocity;
        public int Vie = 3;
        public Rectangle PlayerRec
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, PacmanTextureIdle.Width, PacmanTextureIdle.Height);
            }
        }
        SpriteState currentSpriteState = SpriteState.Idle;

        public void LoadPac(ContentManager content)
        {
            PacmanTextureRight = content.Load<Texture2D>("Pac_Right");
            PacmanTextureLeft = content.Load<Texture2D>("Pac_Left");
            PacmanTextureTop = content.Load<Texture2D>("Pac_Up");
            PacmanTextureDown = content.Load<Texture2D>("Pac_Down");
            PacmanTextureIdle = content.Load<Texture2D>("Pac_Idle");
        }

        public void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                currentSpriteState = SpriteState.Top;
                Velocity.Y -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                currentSpriteState = SpriteState.Down;
                Velocity.Y += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                currentSpriteState = SpriteState.Left;
                Velocity.X -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                currentSpriteState = SpriteState.Right;
                Velocity.X += 2;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentSpriteState)
            {
                case SpriteState.Left:
                    spriteBatch.Draw(PacmanTextureLeft, Position, Color.White);
                    break;
                case SpriteState.Right:
                    spriteBatch.Draw(PacmanTextureRight, Position, Color.White);
                    break;
                case SpriteState.Top:
                    spriteBatch.Draw(PacmanTextureTop, Position, Color.White);
                    break;
                case SpriteState.Down:
                    spriteBatch.Draw(PacmanTextureDown, Position, Color.White);
                    break;
                case SpriteState.Idle:
                    spriteBatch.Draw(PacmanTextureIdle, Position, Color.White);
                    break;
            }
        }
    }
}