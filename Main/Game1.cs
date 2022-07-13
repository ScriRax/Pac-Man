using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace monJeu
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager Graphics;
        private SpriteBatch spriteBatch;
        private List<Walls> wallsArr;
        private List<Ghost> ghostsArr;
        private List<Intersection> interArr;
        private List<Coin> coinArr;
        private Intersection previousInter;
        public static int ScreenWidth {get; set;} = 1024;
        public static int ScreenHeight {get; set;} = 768;
        private List<SoundEffect> sfx;
        private List<int> classements;
        private Random r = new Random();
        Pacman player = new Pacman();
        Map mapWalls = new Map();
        Song mainSong;

        public Texture2D DetectScreenBackground {get; set;}
        public Texture2D DetectTitleScreenBackground {get; set;}
        public Texture2D ControlScreenBackground {get; set;}

        public Texture2D VictoryBackground {get; set;}

        public Texture2D PausedBackground {get; set;}

        public Texture2D GameOverBackground {get; set;}

        private SpriteFont font;
        private int score = 0;
        private float currentTime;

        private int classement;

        bool IsDetectedScreenShown;
        bool IsTitleScreenShown;
        bool IsControlsScreenShown;
        bool IsVictoryScreenShown;
        bool IsPaused;
        bool IsGameOver;
        bool IsPaudesSoBackground;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            sfx = new List<SoundEffect>();
            classements = new List<int>();
        }

        protected override void Initialize()
        {
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.ApplyChanges();
            base.Initialize();
            previousInter = interArr[1];
            IsPaudesSoBackground = false;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.mainSong = Content.Load<Song>("stage");
            MediaPlayer.Play(mainSong);
            MediaPlayer.Volume = 0.0f;
            MediaPlayer.IsRepeating = true;

            sfx.Add(Content.Load<SoundEffect>("hitsfx"));
            sfx.Add(Content.Load<SoundEffect>("coinsfx"));

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
            coinArr = mapWalls.LoadCoin(Content);

            DetectScreenBackground = Content.Load<Texture2D>("Classement_Monoman");
            DetectTitleScreenBackground = Content.Load<Texture2D>("NewMenu_Monoman");
            ControlScreenBackground = Content.Load<Texture2D>("Tuto_Monoman");
            PausedBackground = Content.Load<Texture2D>("newPause");
            GameOverBackground = Content.Load<Texture2D>("newGameover");
            VictoryBackground = Content.Load<Texture2D>("victory");

            IsDetectedScreenShown = false;
            IsTitleScreenShown = true;
            IsControlsScreenShown = false;
            IsPaudesSoBackground = false;
            IsGameOver = false;
            IsVictoryScreenShown = false;

            font = Content.Load<SpriteFont>("Score");
        }

        protected override void Update(GameTime gameTime)
        {
            if (IsPaused == false)
            {
                player.Move();

                IsTouchingScreenCollision();
                IsTouchingGhostScreenCollision(ghostsArr);
                GetWallsCollision(wallsArr);
                GetIntersectionCollisionGhost(ghostsArr);
                GetWallsGhostsCollision();
                TouchingGhost();
                CollectingCoin();

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

                if (Keyboard.GetState().IsKeyDown(Keys.Space) == true)
                {
                    IsPaused = true;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {
                IsPaused = false;
            }
            if (IsTitleScreenShown)
            {
                UpdtateTitleScreen();
            }

            base.Update(gameTime);
        }


        //Fonction qui met a jour le screen Detect en fonction de l'input du joueur 
        private void UpDateDetectScreen()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
            {
                IsTitleScreenShown = false;
                IsDetectedScreenShown = true;
                IsControlsScreenShown = false;
            }
        }

        //Fonction de sauvegarde du score dans un fichier txt       
        private void SaveScore()
        {
            var path = @"C:\Users\Public\Score_monogame.txt";

            if (!File.Exists(path))
            {
                File.Create(path);
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine("Score: " + score);
                tw.Close();
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("Score: " + score);
                    tw.Close();
                }
            }
        }

        //Fonction qui va update l'ecran de titre pour passer a l'ecran de jeu si on appuie sur A
        private void UpdtateTitleScreen()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                IsTitleScreenShown = false;
                IsDetectedScreenShown = false;
                IsControlsScreenShown = false;
                IsPaused = false;
                IsGameOver = false;
                IsVictoryScreenShown = false;

            }
            if (IsGameOver == true)
            {
                player.Vie = 3;
                if (classements.Count >= 5)
                {
                    if (score > classements[1])
                    {
                        classements.Remove(classements[1]);
                        classements.Add(score);
                    }
                }
                else
                {
                    classements.Add(score);
                }
                classements.Sort();
                score = 0;
                coinArr = mapWalls.LoadCoin(Content);
            }
        }

        //Fonction qui va draw le screen HighScore
        private void DrawDetectScreen()
        {
            spriteBatch.Draw(DetectScreenBackground, Vector2.Zero, Color.White);
        }

        //Fonction qui va draw le screen des controls
        private void DrawControlScreen()
        {
            spriteBatch.Draw(ControlScreenBackground, Vector2.Zero, Color.White);
        }
private void DrawGameOver()
{
    spriteBatch.Draw(GameOverBackground,Vector2.Zero, Color.White);
}
private void DrawVictory()
{
    spriteBatch.Draw(VictoryBackground,Vector2.Zero, Color.White);
    player.Position.X = 0;
    player.Position.Y = 0;
    player.Velocity.X = 0;
    player.Velocity.Y = 0;
}

        // Fonction qui va afficher le bouton pause quand le jeu est en pause
        private void DrawPausedBackground()
        {
            spriteBatch.Draw(PausedBackground, Vector2.Zero, Color.White);
        }

        //Fonction qui draw l'ecran de titre 
        private void DrawTitleScreen()
        {
            spriteBatch.Draw(DetectTitleScreenBackground, Vector2.Zero, Color.White);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            else if ((Keyboard.GetState().IsKeyDown(Keys.Z) == true) & (IsTitleScreenShown = true))
            {
                DrawDetectScreen();
                IsDetectedScreenShown = true;
                IsTitleScreenShown = false;
                IsControlsScreenShown = false;

                if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
                {
                    DrawTitleScreen();
                    IsDetectedScreenShown = false;
                    IsTitleScreenShown = true;
                    IsControlsScreenShown = false;
                    IsVictoryScreenShown = false;

                }
            }

            else if ((Keyboard.GetState().IsKeyDown(Keys.E) == true) & (IsTitleScreenShown = true))
            {
                DrawControlScreen();
                IsDetectedScreenShown = false;
                IsTitleScreenShown = false;
                IsControlsScreenShown = true;

                if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
                {
                    DrawTitleScreen();
                    IsDetectedScreenShown = false;
                    IsTitleScreenShown = true;
                    IsControlsScreenShown = false;
                }
            }
        }


        private void IsTouchingScreenCollision()
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

        private void GetIntersectionCollisionGhost(List<Ghost> ghosts)
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

        private void IsTouchingGhostScreenCollision(List<Ghost> ghosts)
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

        private void CollectingCoin()
        {
            for (int i = coinArr.Count - 1; i >= 0; i--)
            {
                if ((Collision.IsTouchingCoinLeft(player, coinArr[i])) ||
                (Collision.IsTouchingCoinRight(player, coinArr[i])) ||
                (Collision.IsTouchingCoinTop(player, coinArr[i])) ||
                (Collision.IsTouchingCoinBottom(player, coinArr[i])))
                {
                    sfx[1].Play(volume: 0.2f, pitch: 0.0f, pan: 0.0f);
                    coinArr.Remove(coinArr[i]);
                    score += 50;
                }
            }
            if (coinArr.Count <= 0)
            {
                IsVictoryScreenShown = true;
                coinArr = mapWalls.LoadCoin(Content);
                
            }
        }

        private void GetWallsCollision(List<Walls> wallsList)
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

        private void GetWallsGhostsCollision()
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

        private void TouchingGhost()
        {
            foreach (var ghost in ghostsArr)
            {
                if ((Collision.IsPlayerTouchingGhostLeft(player, ghost)) ||
                (Collision.IsPlayerTouchingGhostRight(player, ghost)) ||
                (Collision.IsPlayerTouchingGhostTop(player, ghost)) ||
                (Collision.IsPlayerTouchingGhostBottom(player, ghost)))
                {
                    player.Vie = player.Vie - 1;
                    sfx[0].Play();
                    player.Velocity.X = 0;
                    player.Velocity.Y = 0;
                    player.Position.X = 0;
                    player.Position.Y = 0;
                }
            }
        }

        // private void SaveGame() 
        // {
        //     player.Position;
        //     score;
        //     player.Vie;
        // }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if (IsDetectedScreenShown)
            {
                DrawDetectScreen();
                if (classements.Count > 0)
                {
                    var i = 0;
                    foreach (var classement in classements)
                    {
                        spriteBatch.DrawString(font, "Score: " + classement, new Vector2(450, 315 + i), Color.White);
                        i += 30;
                    }
                }
            }
            else if (IsTitleScreenShown)
            {
                DrawTitleScreen();
                IsPaused = true;
            }
            else if (IsControlsScreenShown)
            {
                DrawControlScreen();
            }
            else if (IsGameOver)
            {
                DrawGameOver();
            }
            else if (IsVictoryScreenShown)
            {
                DrawVictory();
            }
            else
            {
                player.Draw(spriteBatch);

                foreach (var ghost in ghostsArr)
                {
                    ghost.Draw(spriteBatch);
                }

                foreach (var wall in wallsArr)
                {
                    wall.Draw(spriteBatch);
                }

                spriteBatch.DrawString(font, "Score: " + score, new Vector2(900, 25), Color.White);
                spriteBatch.DrawString(font, "Vie: " + player.Vie, new Vector2(800, 25), Color.White);

                foreach (var coin in coinArr)
                {
                    coin.Draw(spriteBatch);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
            {
                IsTitleScreenShown = true;
                IsDetectedScreenShown = false;
                IsGameOver = false;
                IsVictoryScreenShown = false;
                DrawTitleScreen();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) == true)
            {
                IsPaused = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.V) == true)
            {
                //Permet d'ajouter du delai sur l'activation d'une touche pour reguler l'activation de celle ci
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (currentTime >= 1)
                {
                    SaveScore();
                    currentTime = 0;
                }
            }

            if (IsPaused == true & (IsTitleScreenShown == false) & (IsControlsScreenShown == false) & (IsDetectedScreenShown == false))
            {
                DrawPausedBackground();
            }

            if (player.Vie < 1)
            {
                IsGameOver = true;
                foreach (var Ghost in ghostsArr)
                {
                    Ghost.PositionG.X = 602;
                    Ghost.PositionG.Y = 620;
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
