using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace monJeu
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //private SpriteFont _font;
        private List<Walls> wallsArr;

        public static Random random;
        public static int ScreenWidth = 1024;
        public static int ScreenHeight = 768;

        Pacman player = new Pacman();

        Ghost redGhost = new Ghost("red");
        Ghost blueGhost = new Ghost("blue");

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadPac(Content);
            //_font = Content.Load<SpriteFont>("TestFont");
            var wallTexture = Content.Load<Texture2D>("WallPac");
            wallsArr = new List<Walls>() {
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,35),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,100),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(100,100),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(100,35),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(100,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(215,35),
                },
                };

            redGhost.LoadGhost(Content);
            blueGhost.LoadGhost(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            player.Movement();

            ScreenCollision();

            WallsCollision(wallsArr);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            player.position += player.velocity;

            player.velocity = Vector2.Zero;

            base.Update(gameTime);
        }



        private void ScreenCollision()
        {
            if (player.position.X > _graphics.PreferredBackBufferWidth - player.pacmanTextureIdle.Width)
            {
                player.position.X = _graphics.PreferredBackBufferWidth - player.pacmanTextureIdle.Width;
            }
            else if (player.position.X < 0)
            {
                player.position.X = 0;
            }
            if (player.position.Y > _graphics.PreferredBackBufferHeight - player.pacmanTextureIdle.Height)
            {
                player.position.Y = _graphics.PreferredBackBufferHeight - player.pacmanTextureIdle.Height;
            }
            else if (player.position.Y < 0)
            {
                player.position.Y = 0;
            }
        }

        protected bool IsTouchingLeft(Walls wall)
        {
            return player.PlayerRec.Right + player.velocity.X > wall.WallRec.Left &&
              player.PlayerRec.Left < wall.WallRec.Left &&
              player.PlayerRec.Bottom > wall.WallRec.Top &&
              player.PlayerRec.Top < wall.WallRec.Bottom;
        }

        protected bool IsTouchingRight(Walls wall)
        {
            return player.PlayerRec.Left + player.velocity.X < wall.WallRec.Right &&
              player.PlayerRec.Right > wall.WallRec.Right &&
              player.PlayerRec.Bottom > wall.WallRec.Top &&
              player.PlayerRec.Top < wall.WallRec.Bottom;
        }

        protected bool IsTouchingTop(Walls wall)
        {
            return player.PlayerRec.Bottom + player.velocity.Y > wall.WallRec.Top &&
              player.PlayerRec.Top < wall.WallRec.Top &&
              player.PlayerRec.Right > wall.WallRec.Left &&
              player.PlayerRec.Left < wall.WallRec.Right;
        }

        protected bool IsTouchingBottom(Walls wall)
        {
            return player.PlayerRec.Top + player.velocity.Y < wall.WallRec.Bottom &&
              player.PlayerRec.Bottom > wall.WallRec.Bottom &&
              player.PlayerRec.Right > wall.WallRec.Left &&
              player.PlayerRec.Left < wall.WallRec.Right;
        }

        public void WallsCollision(List<Walls> wallsList)
        {
            foreach (var wall in wallsList)
            {
                if ((player.velocity.X > 0 && this.IsTouchingLeft(wall)) ||
                (player.velocity.X < 0 & this.IsTouchingRight(wall)))
                    player.velocity.X = 0;

                if ((player.velocity.Y > 0 && this.IsTouchingTop(wall)) ||
                    (player.velocity.Y < 0 & this.IsTouchingBottom(wall)))
                    player.velocity.Y = 0;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //_spriteBatch.DrawString(_font,"posx",_position,Color.Black);
            player.Draw(_spriteBatch);
            blueGhost.Draw(_spriteBatch);

            foreach (var wall in wallsArr)
            {
                wall.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
