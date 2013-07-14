using System;
using System.Collections.Generic;
using Asteroids.Helpers;
using Asteroids.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.GameObjects;
using Asteroids.Factories;
using Asteroids.Drawers;
using Microsoft.Xna.Framework.Input;

namespace Asteroids
{
    public class AsteroidsGame : Microsoft.Xna.Framework.Game, IStateable, IOutputText
    {
        SpriteBatch spriteBatch;
        private KeyboardState previousKeyboardState;
        IGameObjectsFactory factory;
        Player player;
        SpriteFont spriteFont;

        private IGameState state;
        private Drawer drawer;
        private PolygonDrawer polygonDrawer;
        private SpriteDrawer spriteDrawer;

        private Texture2D background;
        
        public AsteroidsGame()
        {
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
                
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var gameObjects = new List<GameObject>();

            factory = new GameObjectsFactory(Content, spriteBatch, GraphicsDevice, gameObjects);

            polygonDrawer = new PolygonDrawer(new PolygonBatch(new LineBatch(GraphicsDevice, spriteBatch)));
            spriteDrawer = new SpriteDrawer(spriteBatch, Content);
            drawer = spriteDrawer;

            var keyboardHandler = new BaseKeyboardHandler(factory, GraphicsDevice, spriteBatch);
            
            this.state = new PlayingState(this, factory, this, keyboardHandler, gameObjects);

            background = Content.Load<Texture2D>("background");

            base.Initialize();            
        }        

        public void SetState(IGameState state)
        {
            this.state = state;
        }

        protected override void LoadContent()
        {       
            spriteFont = Content.Load<SpriteFont>("SpriteFont");                     
        }
    
        protected override void Update(GameTime gameTime)
        {
            HandleInput();

            state.Update(gameTime, GraphicsDevice.Viewport.Bounds, drawer);

            base.Update(gameTime);            
        }

        void HandleInput()
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Q) && previousKeyboardState.IsKeyUp(Keys.Q))
            {
                ToggleGraphicsMode();
            }

            previousKeyboardState = keyboardState;    
        }

        void ToggleGraphicsMode()
        {
            if (drawer is SpriteDrawer)
            {
                drawer = polygonDrawer;
            }
            else
            {
                drawer = spriteDrawer;
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //var background = Content.Load<Texture2D>("background");
            
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);

            state.Draw(gameTime, GraphicsDevice.Viewport.Bounds, drawer);

            spriteBatch.End();            
            
            base.Draw(gameTime);
        }

        public void OutputText(Vector2 position, float scale, string text)
        {
            Vector2 fontOrigin = spriteFont.MeasureString(text) / 2;
            spriteBatch.DrawString(spriteFont, text,
                position, Color.White, 0, fontOrigin, scale, SpriteEffects.None, 0);
        }
    }
}
