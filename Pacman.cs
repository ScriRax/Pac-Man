using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        public Texture2D pacmanTextureRight;
        public Texture2D pacmanTextureLeft;
        public Texture2D pacmanTextureIdle;
        public Texture2D pacmanTextureTop;
        public Texture2D pacmanTextureDown;

        public Vector2 position;

        public Vector2 velocity;

        SpriteState currentSpriteState = SpriteState.Idle;

        public Rectangle Rectangle;
        public Rectangle PlayerRec
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, pacmanTextureIdle.Width, pacmanTextureIdle.Height);
            }
        }


        public void LoadPac(ContentManager Content)
        {
            pacmanTextureRight = Content.Load<Texture2D>("Pac_Right");
            pacmanTextureLeft = Content.Load<Texture2D>("Pac_Left");
            pacmanTextureTop = Content.Load<Texture2D>("Pac_Up");
            pacmanTextureDown = Content.Load<Texture2D>("Pac_Down");
            pacmanTextureIdle = Content.Load<Texture2D>("Pac_Idle");
        }

        public void Movement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                currentSpriteState = SpriteState.Top;
                velocity.Y -= 5;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                currentSpriteState = SpriteState.Down;
                velocity.Y += 5;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                currentSpriteState = SpriteState.Left;
                velocity.X -= 5;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                currentSpriteState = SpriteState.Right;
                velocity.X += 5;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentSpriteState)
            {
                case SpriteState.Left:
                    spriteBatch.Draw(pacmanTextureLeft, position, Color.White);
                    break;
                case SpriteState.Right:
                    spriteBatch.Draw(pacmanTextureRight, position, Color.White);
                    break;
                case SpriteState.Top:
                    spriteBatch.Draw(pacmanTextureTop, position, Color.White);
                    break;
                case SpriteState.Down:
                    spriteBatch.Draw(pacmanTextureDown, position, Color.White);
                    break;
                case SpriteState.Idle:
                    spriteBatch.Draw(pacmanTextureIdle, position, Color.White);
                    break;
            }
        }
    }
}