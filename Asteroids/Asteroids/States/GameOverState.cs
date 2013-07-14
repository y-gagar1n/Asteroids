using System;
using Asteroids.Drawers;
using Asteroids.Factories;
using Asteroids.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Asteroids.States
{
    public class GameOverState : IGameState
    {
        private IStateable automat;
        private IGameObjectsFactory factory;
        private IOutputText output;
        private BaseKeyboardHandler keyboardHandler;
        private List<GameObject> gameObjects;

        private int finalScore;

        public GameOverState(IStateable automat, IGameObjectsFactory factory, IOutputText output, BaseKeyboardHandler keyboardHandler, List<GameObject> gameObjects, int finalScore)
        {
            this.automat = automat;
            this.factory = factory;
            this.output = output;
            this.keyboardHandler = keyboardHandler;
            this.gameObjects = gameObjects;

            this.finalScore = finalScore;
        }

        public void Update(GameTime gameTime, Rectangle windowBounds, Drawer drawer)
        {
            if (BaseKeyboardHandler.PressedAnyKey(Keyboard.GetState()))
            {
                automat.SetState(new PlayingState(automat, factory, output, keyboardHandler, gameObjects));
            }   
        }

        public void Draw(GameTime gameTime, Rectangle windowBounds, Drawer drawer)
        {
            gameObjects.Clear();

            string gameOverStr = "GAME OVER";
            string scoreStr = String.Format("Your score: {0}", finalScore);
            string infoStr = "Press any key to start new game";

            output.OutputText(new Vector2(windowBounds.Width / 2, windowBounds.Height / 4), 2, gameOverStr);
            output.OutputText(new Vector2(windowBounds.Width / 2, windowBounds.Height / 2), 1, scoreStr);
            output.OutputText(new Vector2(windowBounds.Width / 2, 3 * windowBounds.Height / 4), 1, infoStr);
        }
    }
}
