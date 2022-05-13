using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace monJeu
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Random random;
        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;
        
      


        private Texture2D pacmanTexture;
        private Vector2 _position;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pacmanTexture = Content.Load<Texture2D>("pacman");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                _position.Y -= 5;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _position.Y += 5;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                _position.X -= 5;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _position.X += 5;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

    

            // TODO: Add your drawing code here

             _spriteBatch.Begin();
             _spriteBatch.Draw(pacmanTexture, _position, Color.White);
             _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
