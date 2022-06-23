using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace monJeu
{
    enum SpriteState {
            Left,
            Right,
            Top,
            Down,
            Idle
        }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _font;

        public static Random random;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 960;

        private Texture2D pacmanTextureRight;
        private Texture2D pacmanTextureLeft;
        private Texture2D pacmanTextureIdle;
        private Texture2D pacmanTextureTop;
        private Texture2D pacmanTextureDown;
        private Vector2 _position;

        Ghost blueGhost = new Ghost("red");

        SpriteState currentSpriteState = SpriteState.Idle;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";            
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Window.AllowUserResizing = true;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pacmanTextureRight = Content.Load<Texture2D>("Pac_Right");
            pacmanTextureLeft = Content.Load<Texture2D>("Pac_Left");
            pacmanTextureTop = Content.Load<Texture2D>("Pac_Up");
            pacmanTextureDown = Content.Load<Texture2D>("Pac_Down");
            pacmanTextureIdle = Content.Load<Texture2D>("Pac_Idle");
            //_font = Content.Load<SpriteFont>("TestFont");

            blueGhost.LoadGhost(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                currentSpriteState = SpriteState.Top;
                _position.Y -= 10;
                
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                currentSpriteState = SpriteState.Down;
                _position.Y += 10;
                
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                currentSpriteState = SpriteState.Left;
                _position.X -= 10;
                
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                currentSpriteState = SpriteState.Right;
                _position.X += 10;
                
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if(_position.X > _graphics.PreferredBackBufferWidth - pacmanTextureIdle.Width)
            {
                _position.X = _graphics.PreferredBackBufferWidth - pacmanTextureIdle.Width;
            }
            else if(_position.X < 0) {
                _position.X = 0;
            }

            if(_position.Y > _graphics.PreferredBackBufferHeight - pacmanTextureIdle.Height) 
            {
                _position.Y = _graphics.PreferredBackBufferHeight - pacmanTextureIdle.Height;
            }
            else if(_position.Y < 0)
            {
                _position.Y = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
             //_spriteBatch.DrawString(_font,"Pos X : ",_position.X,Color.Black);
            switch(currentSpriteState) 
            {
                case SpriteState.Left:
                _spriteBatch.Draw(pacmanTextureLeft, _position, Color.White);
                break;
                case SpriteState.Right:
                _spriteBatch.Draw(pacmanTextureRight, _position, Color.White);
                break;
                case SpriteState.Top:
                _spriteBatch.Draw(pacmanTextureTop, _position, Color.White);
                break;
                case SpriteState.Down:
                _spriteBatch.Draw(pacmanTextureDown, _position, Color.White);
                break;
                case SpriteState.Idle:
                _spriteBatch.Draw(pacmanTextureIdle, _position, Color.White);
                break;
            } 
            
            blueGhost.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
