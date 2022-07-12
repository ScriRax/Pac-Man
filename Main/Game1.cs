using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace monJeu
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;
        private List<Walls> wallsArr;
        public static int ScreenWidth = 1024;
        public static int ScreenHeight = 768;
        Pacman player = new Pacman();
        Ghost redGhost = new Ghost("red");
        Ghost blueGhost = new Ghost("blue");

       public Texture2D DetectScreenBackground;
        public Texture2D DetectTitleScreenBackground;
        public Texture2D ControlScreenBackground;

        public Texture2D PausedBackground;

        private SpriteFont font;
        public int score = 23;
        private float currentTime;
        
        

        bool IsDetectedScreenShown;
        bool IsTitleScreenShown;

        bool IsControlsScreenShown;

        bool IsPaused;

        bool IsPaudesSoBackground;

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
            

            DetectScreenBackground = Content.Load<Texture2D>("Classement_Monoman");
            DetectTitleScreenBackground = Content.Load<Texture2D>("NewMenu_Monoman");
            ControlScreenBackground = Content.Load<Texture2D>("Tuto_Monoman");
            PausedBackground = Content.Load<Texture2D>("pause");

            IsDetectedScreenShown = false;
            IsTitleScreenShown = true;
            IsControlsScreenShown= false;
            IsPaudesSoBackground = false;
            font = Content.Load<SpriteFont>("Score");


            

        }


        protected override void Update(GameTime gameTime)
        {

            if (IsPaused == false)
            {
                 player.Move();

                 ScreenCollision();

                 WallsCollision(wallsArr);
           
                 player.Position += player.Velocity;

                 player.Velocity = Vector2.Zero;

            
            
           

                 if (Keyboard.GetState().IsKeyDown(Keys.Space)== true)
                 {
                        IsPaused = true;
                   
                 }

    
            

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)== true)
                {
                    IsPaused = false;
                }
                 if(IsTitleScreenShown)
            {
                UpdtateTitleScreen();
            }
            
            base.Update(gameTime);
        }

//Fonction qui met a jour le screen Detect en fonction de l'input du joueur 
        private void UpDateDetectScreen()
        {

           
           
            if(Keyboard.GetState().IsKeyDown(Keys.Back) == true)
            {
                
                IsTitleScreenShown = false;
                IsDetectedScreenShown = true;
                IsControlsScreenShown=false;
                
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
    if (Keyboard.GetState().IsKeyDown(Keys.A)== true)
    {
        IsTitleScreenShown = false;
        IsDetectedScreenShown = false;
        IsControlsScreenShown = false;
        IsPaused = false;
        
    }
    
}

private void UpdateControlScreen()
{
    if(Keyboard.GetState().IsKeyDown(Keys.E)==true)
    {
        IsDetectedScreenShown = false;
        IsTitleScreenShown = false;
        IsTitleScreenShown = true;
    }
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

//Fonction qui va draw le screen HighScore
    private void DrawDetectScreen()
    {
        SpriteBatch.Draw(DetectScreenBackground, Vector2.Zero, Color.White) ;
        
   }

   private void DrawControlScreen()
   {
    SpriteBatch.Draw(ControlScreenBackground,Vector2.Zero, Color.White);
   }

private void DrawPausedBackground()
{
        SpriteBatch.Draw(PausedBackground, Vector2.Zero, Color.White) ;
}

//Fonction qui draw l'ecran de titre 
   private void DrawTitleScreen()
   {
     SpriteBatch.Draw(DetectTitleScreenBackground, Vector2.Zero, Color.White);
     if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            else if ((Keyboard.GetState().IsKeyDown(Keys.Z)==true) & (IsTitleScreenShown=true))
            {
                DrawDetectScreen();
                IsDetectedScreenShown = true;
                IsTitleScreenShown = false;
                IsControlsScreenShown = false;
                
                
                 if (Keyboard.GetState().IsKeyDown(Keys.Back)== true)

                 {
                   DrawTitleScreen();
                   IsDetectedScreenShown = false;
                   IsTitleScreenShown = true; 
                   IsControlsScreenShown = false;
                 }
            

            }

            else if ((Keyboard.GetState().IsKeyDown(Keys.E)==true) & (IsTitleScreenShown=true))
            {
                DrawControlScreen();
                IsDetectedScreenShown = false;
                IsTitleScreenShown = false;
                IsControlsScreenShown = true;
                
                
                 if (Keyboard.GetState().IsKeyDown(Keys.Back)== true)

                 {
                   DrawTitleScreen();
                   IsDetectedScreenShown = false;
                   IsTitleScreenShown = true; 
                   IsControlsScreenShown = false;
                 }
            

            }

   }
        protected override void Draw(GameTime gameTime)
        {

            
            
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();
    
            
            

            if(IsDetectedScreenShown)
            {
                DrawDetectScreen();
                SpriteBatch.DrawString(font, "Score: " + score, new Vector2(450, 315), Color.White);
            }
            else if(IsTitleScreenShown)
            {
                DrawTitleScreen();
                IsPaused = true;
                
            }
            else if (IsControlsScreenShown)
            {
                DrawControlScreen();
            }

            
            else 
            {
                    player.Draw(SpriteBatch);
            blueGhost.Draw(SpriteBatch);
            foreach (var wall in wallsArr)
            {
                wall.Draw(SpriteBatch);
            }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back)== true)
            {
                IsTitleScreenShown = true;
                IsDetectedScreenShown = false;
                DrawTitleScreen();
            
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space)== true)
            {
                IsPaused = true;
                
                
              
            }

            if(Keyboard.GetState().IsKeyDown(Keys.V)==true)
            {
               

                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;    //Permet d'ajouter du delai sur l'activation d'une touche pour reguler l'activation de celle ci
                if(currentTime >= 1)                                            //
            {                                                                   //    
            SaveScore();                                                        //
            currentTime = 0;                                                    //
                }                                                               //
  
            }
        if (IsPaused == true & (IsTitleScreenShown == false)& (IsControlsScreenShown == false)& (IsDetectedScreenShown == false))
        {
                DrawPausedBackground();
        }
            

            SpriteBatch.End();

            base.Draw(gameTime);
            }
            
        }
}


