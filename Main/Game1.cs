using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace monJeu
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;
        private List<Walls> wallsArr;
        private List<Ghost> ghostsArr;
        private List<Intersection> interArr;
        private Intersection previousInter;
        public static int ScreenWidth = 1024;
        public static int ScreenHeight = 768;
        private SoundEffect hitSound;
        private Random r = new Random();
        Pacman player = new Pacman();
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
            previousInter = interArr[1];
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            this.mainSong = Content.Load<Song>("stage");
            MediaPlayer.Play(mainSong);
            MediaPlayer.Volume = 0.0f;
            MediaPlayer.IsRepeating = true;

            hitSound = Content.Load<SoundEffect>("hitsfx");

            ghostsArr = new List<Ghost>()
            {
                new Ghost("red") {
                },
                new Ghost("blue") {
                },
                new Ghost("pink") {
                },
                new Ghost("orange") {
                },
            };

            foreach (var ghost in ghostsArr)
            {
                ghost.LoadGhost(Content);
            }

            player.LoadPac(Content);
            wallsArr = mapWalls.LoadWalls(Content);

            interArr = mapWalls.LoadIntersection(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            player.Move();

            ScreenCollision();
            ScreenCollisionGhost(ghostsArr);
            WallsCollision(wallsArr);
            IntersectionCollisionGhost(ghostsArr);
            WallsGhostsCollision();
            TouchingGhost();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            foreach (var ghost in ghostsArr)
            {
                ghost.PositionG += ghost.Velocity;
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

        private void IntersectionCollisionGhost(List<Ghost> ghosts)
        {
            foreach (var inter in interArr)
            {
                if (inter.Position != previousInter.Position)
                {
                    foreach (var ghost in ghostsArr)
                    {
                        int direction = r.Next(1, 4);
                        if (ghost.Velocity.X > 0 && Collision.IsGhostTouchingLeftInter(inter, ghost))
                        {
                            previousInter = inter;
                            ghost.Velocity.X = 0;
                            switch (direction)
                            {
                                case 1:
                                    ghost.Velocity.X = 1;
                                    break;
                                case 2:
                                    ghost.Velocity.Y = -1;
                                    break;
                                case 3:
                                    ghost.Velocity.Y = 1;
                                    break;
                            }
                        }
                        else if (ghost.Velocity.X < 0 && Collision.IsGhostTouchingRightInter(inter, ghost))
                        {
                            previousInter = inter;
                            ghost.Velocity.X = 0;
                            switch (direction)
                            {
                                case 1:
                                    ghost.Velocity.Y = 1;
                                    break;
                                case 2:
                                    ghost.Velocity.X = -1;
                                    break;
                                case 3:
                                    ghost.Velocity.Y = -1;
                                    break;
                            }
                        }

                        else if (ghost.Velocity.Y > 0 && Collision.IsGhostTouchingTopInter(inter, ghost))
                        {
                            previousInter = inter;
                            ghost.Velocity.Y = 0;
                            switch (direction)
                            {
                                case 1:
                                    ghost.Velocity.Y = 1;
                                    break;
                                case 2:
                                    ghost.Velocity.X = 1;
                                    break;
                                case 3:
                                    ghost.Velocity.X = -1;
                                    break;
                            }
                        }
                        else if (ghost.Velocity.Y < 0 && Collision.IsGhostTouchingBottomInter(inter, ghost))
                        {
                            previousInter = inter;
                            ghost.Velocity.Y = 0;
                            switch (direction)
                            {
                                case 1:
                                    ghost.Velocity.X = -1;
                                    break;
                                case 2:
                                    ghost.Velocity.Y = -1;
                                    break;
                                case 3:
                                    ghost.Velocity.X = 1;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void ScreenCollisionGhost(List<Ghost> ghosts)
        {
            foreach (var ghost in ghosts)
            {
                if (ghost.PositionG.X > Graphics.PreferredBackBufferWidth - 30)
                {
                    int direction = r.Next(1, 3);
                    ghost.PositionG.X = Graphics.PreferredBackBufferWidth - 30;
                    ghost.Velocity.X = 0;
                    switch (direction)
                    {
                        case 1:
                            ghost.Velocity.X = -1;
                            break;
                        case 2:
                            ghost.Velocity.Y = 1;
                            break;
                        case 3:
                            ghost.Velocity.Y = -1;
                            break;
                    }
                }
                else if (ghost.PositionG.X < 0)
                {
                    int direction = r.Next(1, 3);
                    ghost.PositionG.X = 0;
                    ghost.Velocity.X = 0;
                    switch (direction)
                    {
                        case 1:
                            ghost.Velocity.X = 1;
                            break;
                        case 2:
                            ghost.Velocity.Y = 1;
                            break;
                        case 3:
                            ghost.Velocity.Y = -1;
                            break;
                    }
                }
                if (ghost.PositionG.Y > Graphics.PreferredBackBufferHeight - 30)
                {
                    int direction = r.Next(1, 3);
                    ghost.PositionG.Y = Graphics.PreferredBackBufferHeight - 30;
                    ghost.Velocity.Y = 0;
                    switch (direction)
                    {
                        case 1:
                            ghost.Velocity.X = -1;
                            break;
                        case 2:
                            ghost.Velocity.X = 1;
                            break;
                        case 3:
                            ghost.Velocity.Y = -1;
                            break;
                    }
                }
                else if (ghost.PositionG.Y < 0)
                {
                    int direction = r.Next(1, 3);
                    ghost.PositionG.Y = 0;
                    ghost.Velocity.Y = 0;
                    switch (direction)
                    {
                        case 1:
                            ghost.Velocity.X = -1;
                            break;
                        case 2:
                            ghost.Velocity.X = 1;
                            break;
                        case 3:
                            ghost.Velocity.Y = 1;
                            break;
                    }
                }
            }
        }

        public void WallsCollision(List<Walls> wallsList)
        {
            foreach (var wall in wallsList)
            {
                if ((player.Velocity.X > 0 && Collision.IsTouchingLeft(player, wall)) ||
                (player.Velocity.X < 0 & Collision.IsTouchingRight(player, wall)))
                    player.Velocity.X = 0;

                if ((player.Velocity.Y > 0 && Collision.IsTouchingTop(player, wall)) ||
                    (player.Velocity.Y < 0 & Collision.IsTouchingBottom(player, wall)))
                    player.Velocity.Y = 0;
            }
        }

        public void WallsGhostsCollision()
        {
            foreach (var wall in wallsArr)
            {
                foreach (var ghost in ghostsArr)
                {
                    int direction = r.Next(1, 4);
                    if (ghost.Velocity.X > 0 && Collision.IsGhostTouchingLeft(wall, ghost))
                    {

                        ghost.Velocity.X = 0;
                        switch (direction)
                        {
                            case 1:
                                ghost.Velocity.X = -1;
                                break;
                            case 2:
                                ghost.Velocity.Y = -1;
                                break;
                            case 3:
                                ghost.Velocity.Y = 1;
                                break;
                        }
                    }
                    else if (ghost.Velocity.X < 0 && Collision.IsGhostTouchingRight(wall, ghost))
                    {
                        ghost.Velocity.X = 0;
                        switch (direction)
                        {
                            case 1:
                                ghost.Velocity.X = 1;
                                break;
                            case 2:
                                ghost.Velocity.Y = 1;
                                break;
                            case 3:
                                ghost.Velocity.Y = -1;
                                break;
                        }
                    }

                    else if (ghost.Velocity.Y > 0 && Collision.IsGhostTouchingTop(wall, ghost))
                    {
                        ghost.Velocity.Y = 0;
                        switch (direction)
                        {
                            case 1:
                                ghost.Velocity.X = -1;
                                break;
                            case 2:
                                ghost.Velocity.X = 1;
                                break;
                            case 3:
                                ghost.Velocity.Y = 1;
                                break;
                        }
                    }
                    else if (ghost.Velocity.Y < 0 && Collision.IsGhostTouchingBottom(wall, ghost))
                    {
                        ghost.Velocity.Y = 0;
                        switch (direction)
                        {
                            case 1:
                                ghost.Velocity.X = -1;
                                break;
                            case 2:
                                ghost.Velocity.X = 1;
                                break;
                            case 3:
                                ghost.Velocity.Y = -1;
                                break;
                        }
                    }
                }
            }
        }

        public void TouchingGhost()
        {
            foreach (var ghost in ghostsArr)
            {
                if ((Collision.IsPlayerTouchingGhostLeft(player, ghost)) ||
                (Collision.IsPlayerTouchingGhostRight(player, ghost)) ||
                (Collision.IsPlayerTouchingGhostTop(player, ghost)) ||
                (Collision.IsPlayerTouchingGhostBottom(player, ghost)))
                {
                    player.Vie = player.Vie - 1;
                    hitSound.Play();
                    player.Velocity.X = 0;
                    player.Velocity.Y = 0;
                    player.Position.X = 0;
                    player.Position.Y = 0;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();

            player.Draw(SpriteBatch);

            foreach (var ghost in ghostsArr)
            {
                ghost.Draw(SpriteBatch);
            }

            foreach (var wall in wallsArr)
            {
                wall.Draw(SpriteBatch);
            }

            foreach (var inter in interArr)
            {
                inter.Draw(SpriteBatch);
            }

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
