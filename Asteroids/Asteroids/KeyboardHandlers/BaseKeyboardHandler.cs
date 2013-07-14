using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Asteroids.GameObjects;
using Asteroids.Factories;

namespace Asteroids
{
    public class BaseKeyboardHandler
    {
        private Player player;
        private IGameObjectsFactory factory;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        static KeyboardState previousKeyboardState;   
     
        public BaseKeyboardHandler(IGameObjectsFactory factory, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {   
            this.factory = factory;
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        public void HandleInput(KeyboardState keyBoardState, List<GameObject> gameObjects )
        { 
            if (player.IsAlive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && (previousKeyboardState == null || previousKeyboardState.IsKeyUp(Keys.Space)))
                {
                    gameObjects.Add(factory.GetMissile(player));
                }

                if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && (previousKeyboardState == null || previousKeyboardState.IsKeyUp(Keys.LeftControl)))
                {
                    gameObjects.Add(factory.GetLaserBeam(player));
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    player.Rotation -= 0.1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    player.Rotation += 0.1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    var x = -Math.Cos(player.Rotation + MathHelper.PiOver2);
                    var y = -Math.Sin(player.Rotation + MathHelper.PiOver2);
                    player.MovementDirection = new Vector2((float)x, (float)y);
                    player.SpeedUp();
                }
                else
                {
                    player.SlowDown();
                }
            }
            
            previousKeyboardState = keyBoardState;
        }

        public static bool PressedAnyKey(KeyboardState keyboardState)
        {
            var result = (!previousKeyboardState.GetPressedKeys().Any() || previousKeyboardState == null) && keyboardState.GetPressedKeys().Any();
            previousKeyboardState = keyboardState;
            return result;
        }
    }
}
