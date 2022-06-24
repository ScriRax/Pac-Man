using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace monJeu
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;
        //private SpriteFont _font;
        private List<Walls> wallsArr;
        public static int ScreenWidth = 1024;
        public static int ScreenHeight = 768;
        Pacman player = new Pacman();
        Ghost redGhost = new Ghost("red");
        Ghost blueGhost = new Ghost("blue");

        Map mapWalls = new Map();
        Song mainSong;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;        
        }

        protected override void Initialize()
        {
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            this.mainSong = Content.Load<Song>("stage");
            MediaPlayer.Play(mainSong);
            MediaPlayer.Volume = 0.0f;
            MediaPlayer.IsRepeating = true;

            player.LoadPac(Content);
            redGhost.LoadGhost(Content);
            blueGhost.LoadGhost(Content);
            //_font = Content.Load<SpriteFont>("TestFont");
            wallsArr = mapWalls.LoadWalls(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            player.Move();

            ScreenCollision();

            WallsCollision(wallsArr);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            player.Position += player.Velocity;

            player.Velocity = Vector2.Zero;

            base.Update(gameTime);
        }



        private void ScreenCollision()
        {
            if (player.Position.X > Graphics.PreferredBackBufferWidth - player.PacmanTextureIdle.Width)
            {
                player.Position.X = Graphics.PreferredBackBufferWidth - player.PacmanTextureIdle.Width;
            }
            else if (player.Position.X < 0)
            {
                player.Position.X = 0;
            }
            if (player.Position.Y > Graphics.PreferredBackBufferHeight - player.PacmanTextureIdle.Height)
            {
                player.Position.Y = Graphics.PreferredBackBufferHeight - player.PacmanTextureIdle.Height;
            }
            else if (player.Position.Y < 0)
            {
                player.Position.Y = 0;
            }
        }

        protected bool IsTouchingLeft(Walls wall)
        {
            return player.PlayerRec.Right + player.Velocity.X > wall.WallRec.Left &&
              player.PlayerRec.Left < wall.WallRec.Left &&
              player.PlayerRec.Bottom > wall.WallRec.Top &&
              player.PlayerRec.Top < wall.WallRec.Bottom;
        }

        protected bool IsTouchingRight(Walls wall)
        {
            return player.PlayerRec.Left + player.Velocity.X < wall.WallRec.Right &&
              player.PlayerRec.Right > wall.WallRec.Right &&
              player.PlayerRec.Bottom > wall.WallRec.Top &&
              player.PlayerRec.Top < wall.WallRec.Bottom;
        }

        protected bool IsTouchingTop(Walls wall)
        {
            return player.PlayerRec.Bottom + player.Velocity.Y > wall.WallRec.Top &&
              player.PlayerRec.Top < wall.WallRec.Top &&
              player.PlayerRec.Right > wall.WallRec.Left &&
              player.PlayerRec.Left < wall.WallRec.Right;
        }

        protected bool IsTouchingBottom(Walls wall)
        {
            return player.PlayerRec.Top + player.Velocity.Y < wall.WallRec.Bottom &&
              player.PlayerRec.Bottom > wall.WallRec.Bottom &&
              player.PlayerRec.Right > wall.WallRec.Left &&
              player.PlayerRec.Left < wall.WallRec.Right;
        }

        public void WallsCollision(List<Walls> wallsList)
        {
            foreach (var wall in wallsList)
            {
                if ((player.Velocity.X > 0 && this.IsTouchingLeft(wall)) ||
                (player.Velocity.X < 0 & this.IsTouchingRight(wall)))
                    player.Velocity.X = 0;

                if ((player.Velocity.Y > 0 && this.IsTouchingTop(wall)) ||
                    (player.Velocity.Y < 0 & this.IsTouchingBottom(wall)))
                    player.Velocity.Y = 0;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();
            //SpriteBatch.DrawString(_font,"posx",_position,Color.Black);
            player.Draw(SpriteBatch);
            blueGhost.Draw(SpriteBatch);

            foreach (var wall in wallsArr)
            {
                wall.Draw(SpriteBatch);
            }

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
