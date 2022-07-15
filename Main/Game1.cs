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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Walls> _wallsArr;
        private List<Ghost> _ghostsArr;
        private List<Intersection> _interArr;
        private List<Coin> _coinArr;
        private Intersection _previousInter;
        public static int ScreenWidth { get; set; } = 1024;
        public static int ScreenHeight { get; set; } = 768;
        private List<SoundEffect> _sfx;
        private List<int> _classements;
        private Random _r = new Random();
        private Pacman _player = new Pacman();
        private Map _mapWalls = new Map();
        private Song _mainSong;
        public Texture2D DetectScreenBackground { get; set; }
        public Texture2D DetectTitleScreenBackground { get; set; }
        public Texture2D ControlScreenBackground { get; set; }
        public Texture2D VictoryBackground { get; set; }
        public Texture2D PausedBackground { get; set; }
        public Texture2D GameOverBackground { get; set; }
        private SpriteFont _font;
        private int _score = 0;
        private float _currentTime;
        private bool _isDetectedScreenShown;
        private bool _isTitleScreenShown;
        private bool _isControlsScreenShown;
        private bool _isVictoryScreenShown;
        private bool _isPaused;
        private bool _isGameOver;
        private bool _isPaudesSoBackground;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _sfx = new List<SoundEffect>();
            _classements = new List<int>();
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.ApplyChanges();
            base.Initialize();
            _previousInter = _interArr[1];
            _isPaudesSoBackground = false;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            this._mainSong = Content.Load<Song>("stage");
            MediaPlayer.Play(_mainSong);
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;

            _sfx.Add(Content.Load<SoundEffect>("hitsfx"));
            _sfx.Add(Content.Load<SoundEffect>("coinsfx"));

            _ghostsArr = new List<Ghost>()
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

            foreach (var ghost in _ghostsArr)
            {
                ghost.LoadGhost(Content);
            }

            _player.LoadPac(Content);
            _wallsArr = _mapWalls.LoadWalls(Content);
            _interArr = _mapWalls.LoadIntersection(Content);
            _coinArr = _mapWalls.LoadCoin(Content);

            DetectScreenBackground = Content.Load<Texture2D>("Classement_Monoman");
            DetectTitleScreenBackground = Content.Load<Texture2D>("NewMenu_Monoman");
            ControlScreenBackground = Content.Load<Texture2D>("Tuto_Monoman");
            PausedBackground = Content.Load<Texture2D>("newPause");
            GameOverBackground = Content.Load<Texture2D>("newGameover");
            VictoryBackground = Content.Load<Texture2D>("victory");

            _isDetectedScreenShown = false;
            _isTitleScreenShown = true;
            _isControlsScreenShown = false;
            _isPaudesSoBackground = false;
            _isGameOver = false;
            _isVictoryScreenShown = false;

            _font = Content.Load<SpriteFont>("Score");
        }

        protected override void Update(GameTime gameTime)
        {
            if (_isPaused == false)
            {
                _player.Move();

                IsTouchingScreenCollision();
                IsTouchingGhostScreenCollision(_ghostsArr);
                GetWallsCollision(_wallsArr);
                GetIntersectionCollisionGhost(_ghostsArr);
                GetWallsGhostsCollision();
                TouchingGhost();
                CollectingCoin();

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Exit();
                }

                foreach (var ghost in _ghostsArr)
                {
                    ghost.PositionG += ghost.Velocity;
                }

                _player.Position += _player.Velocity;
                _player.Velocity = Vector2.Zero;

                if (Keyboard.GetState().IsKeyDown(Keys.Space) == true)
                {
                    _isPaused = true;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {
                _isPaused = false;
            }
            if (_isTitleScreenShown)
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
                _isTitleScreenShown = false;
                _isDetectedScreenShown = true;
                _isControlsScreenShown = false;
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
                tw.WriteLine("Score: " + _score);
                tw.Close();
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("Score: " + _score);
                    tw.Close();
                }
            }
        }

        //Fonction qui va update l'ecran de titre pour passer a l'ecran de jeu si on appuie sur A
        private void UpdtateTitleScreen()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                _isTitleScreenShown = false;
                _isDetectedScreenShown = false;
                _isControlsScreenShown = false;
                _isPaused = false;
                _isGameOver = false;
                _isVictoryScreenShown = false;

            }
            if (_isGameOver == true)
            {
                _player.Vie = 3;
                addClassement();
                _score = 0;
                _coinArr = _mapWalls.LoadCoin(Content);
            }
        }

        //Fonction qui va draw le screen HighScore
        private void DrawDetectScreen()
        {
            _spriteBatch.Draw(DetectScreenBackground, Vector2.Zero, Color.White);
        }

        //Fonction qui va draw le screen des controls
        private void DrawControlScreen()
        {
            _spriteBatch.Draw(ControlScreenBackground, Vector2.Zero, Color.White);
        }
        private void DrawGameOver()
        {
            _spriteBatch.Draw(GameOverBackground, Vector2.Zero, Color.White);
        }
        private void DrawVictory()
        {
            _spriteBatch.Draw(VictoryBackground, Vector2.Zero, Color.White);
            _player.Position.X = 0;
            _player.Position.Y = 0;
            _player.Velocity.X = 0;
            _player.Velocity.Y = 0;
        }

        // Fonction qui va afficher le bouton pause quand le jeu est en pause
        private void DrawPausedBackground()
        {
            _spriteBatch.Draw(PausedBackground, Vector2.Zero, Color.White);
        }

        //Fonction qui draw l'ecran de titre 
        private void DrawTitleScreen()
        {
            _spriteBatch.Draw(DetectTitleScreenBackground, Vector2.Zero, Color.White);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            else if ((Keyboard.GetState().IsKeyDown(Keys.Z) == true) & (_isTitleScreenShown = true))
            {
                DrawDetectScreen();
                _isDetectedScreenShown = true;
                _isTitleScreenShown = false;
                _isControlsScreenShown = false;

                if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
                {
                    DrawTitleScreen();
                    _isDetectedScreenShown = false;
                    _isTitleScreenShown = true;
                    _isControlsScreenShown = false;
                    _isVictoryScreenShown = false;

                }
            }

            else if ((Keyboard.GetState().IsKeyDown(Keys.E) == true) & (_isTitleScreenShown = true))
            {
                DrawControlScreen();
                _isDetectedScreenShown = false;
                _isTitleScreenShown = false;
                _isControlsScreenShown = true;

                if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
                {
                    DrawTitleScreen();
                    _isDetectedScreenShown = false;
                    _isTitleScreenShown = true;
                    _isControlsScreenShown = false;
                }
            }
        }


        private void IsTouchingScreenCollision()
        {
            if (_player.Position.X > _graphics.PreferredBackBufferWidth - _player.PacmanTextureIdle.Width)
            {
                _player.Position.X = _graphics.PreferredBackBufferWidth - _player.PacmanTextureIdle.Width;
            }
            else if (_player.Position.X < 0)
            {
                _player.Position.X = 0;
            }
            if (_player.Position.Y > _graphics.PreferredBackBufferHeight - _player.PacmanTextureIdle.Height)
            {
                _player.Position.Y = _graphics.PreferredBackBufferHeight - _player.PacmanTextureIdle.Height;
            }
            else if (_player.Position.Y < 0)
            {
                _player.Position.Y = 0;
            }
        }

        private void GetIntersectionCollisionGhost(List<Ghost> ghosts)
        {
            foreach (var inter in _interArr)
            {
                if (inter.Position != _previousInter.Position)
                {
                    foreach (var ghost in _ghostsArr)
                    {
                        int direction = _r.Next(1, 4);
                        if (ghost.Velocity.X > 0 && Collision.IsGhostTouchingLeftInter(inter, ghost))
                        {
                            _previousInter = inter;
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
                            _previousInter = inter;
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
                            _previousInter = inter;
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
                            _previousInter = inter;
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
                if (ghost.PositionG.X > _graphics.PreferredBackBufferWidth - 30)
                {
                    int direction = _r.Next(1, 3);
                    ghost.PositionG.X = _graphics.PreferredBackBufferWidth - 30;
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
                    int direction = _r.Next(1, 3);
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
                if (ghost.PositionG.Y > _graphics.PreferredBackBufferHeight - 30)
                {
                    int direction = _r.Next(1, 3);
                    ghost.PositionG.Y = _graphics.PreferredBackBufferHeight - 30;
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
                    int direction = _r.Next(1, 3);
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
            for (int i = _coinArr.Count - 1; i >= 0; i--)
            {
                if ((Collision.IsTouchingCoinLeft(_player, _coinArr[i])) ||
                (Collision.IsTouchingCoinRight(_player, _coinArr[i])) ||
                (Collision.IsTouchingCoinTop(_player, _coinArr[i])) ||
                (Collision.IsTouchingCoinBottom(_player, _coinArr[i])))
                {
                    _sfx[1].Play(volume: 0.2f, pitch: 0.0f, pan: 0.0f);
                    _coinArr.Remove(_coinArr[i]);
                    _score += 50;
                }
            }
            if (_coinArr.Count <= 0)
            {
                _isVictoryScreenShown = true;
                addClassement();
                _coinArr = _mapWalls.LoadCoin(Content);

            }
        }

        private void addClassement() {
            if (_classements.Count >= 5)
                {
                    if (_score > _classements[1])
                    {
                        _classements.Remove(_classements[1]);
                        _classements.Add(_score);
                    }
                }
                else
                {
                    _classements.Add(_score);
                }
                _classements.Sort();
        }

        private void GetWallsCollision(List<Walls> wallsList)
        {
            foreach (var wall in wallsList)
            {
                if ((_player.Velocity.X > 0 && Collision.IsTouchingLeft(_player, wall)) ||
                (_player.Velocity.X < 0 & Collision.IsTouchingRight(_player, wall)))
                    _player.Velocity.X = 0;

                if ((_player.Velocity.Y > 0 && Collision.IsTouchingTop(_player, wall)) ||
                    (_player.Velocity.Y < 0 & Collision.IsTouchingBottom(_player, wall)))
                    _player.Velocity.Y = 0;
            }
        }

        private void GetWallsGhostsCollision()
        {
            foreach (var wall in _wallsArr)
            {
                foreach (var ghost in _ghostsArr)
                {
                    int direction = _r.Next(1, 4);
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
            foreach (var ghost in _ghostsArr)
            {
                if ((Collision.IsPlayerTouchingGhostLeft(_player, ghost)) ||
                (Collision.IsPlayerTouchingGhostRight(_player, ghost)) ||
                (Collision.IsPlayerTouchingGhostTop(_player, ghost)) ||
                (Collision.IsPlayerTouchingGhostBottom(_player, ghost)))
                {
                    _player.Vie = _player.Vie - 1;
                    _sfx[0].Play();
                    _player.Velocity.X = 0;
                    _player.Velocity.Y = 0;
                    _player.Position.X = 0;
                    _player.Position.Y = 0;
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
            _spriteBatch.Begin();

            if (_isDetectedScreenShown)
            {
                DrawDetectScreen();
                if (_classements.Count > 0)
                {
                    var i = 0;
                    foreach (var classement in _classements)
                    {
                        _spriteBatch.DrawString(_font, "Score: " + classement, new Vector2(450, 315 + i), Color.White);
                        i += 30;
                    }
                }
            }
            else if (_isTitleScreenShown)
            {
                DrawTitleScreen();
                _isPaused = true;
            }
            else if (_isControlsScreenShown)
            {
                DrawControlScreen();
            }
            else if (_isGameOver)
            {
                DrawGameOver();
            }
            else if (_isVictoryScreenShown)
            {
                DrawVictory();
            }
            else
            {
                _player.Draw(_spriteBatch);

                foreach (var ghost in _ghostsArr)
                {
                    ghost.Draw(_spriteBatch);
                }

                foreach (var wall in _wallsArr)
                {
                    wall.Draw(_spriteBatch);
                }

                _spriteBatch.DrawString(_font, "Score: " + _score, new Vector2(900, 25), Color.White);
                _spriteBatch.DrawString(_font, "Vie: " + _player.Vie, new Vector2(800, 25), Color.White);

                foreach (var coin in _coinArr)
                {
                    coin.Draw(_spriteBatch);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
            {
                _isTitleScreenShown = true;
                _isDetectedScreenShown = false;
                _isGameOver = false;
                _isVictoryScreenShown = false;
                DrawTitleScreen();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) == true)
            {
                _isPaused = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.V) == true)
            {
                //Permet d'ajouter du delai sur l'activation d'une touche pour reguler l'activation de celle ci
                _currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_currentTime >= 1)
                {
                    SaveScore();
                    _currentTime = 0;
                }
            }

            if (_isPaused == true & (_isTitleScreenShown == false) & (_isControlsScreenShown == false) & (_isDetectedScreenShown == false))
            {
                DrawPausedBackground();
            }

            if (_player.Vie < 1)
            {
                _isGameOver = true;
                foreach (var ghost in _ghostsArr)
                {
                    ghost.PositionG.X = 602;
                    ghost.PositionG.Y = 620;
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
