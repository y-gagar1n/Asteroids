using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Drawers;
using Asteroids.Factories;
using Asteroids.GameObjects;
using Asteroids.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.States
{
    public class PlayingState : IGameState
    {
        private IStateable automat;
        private IGameObjectsFactory factory;
        private List<GameObject> gameObjects;

        private Player player;
        private BaseKeyboardHandler keyboardHandler;
        private IOutputText output;

        private int score;

        public PlayingState(IStateable automat, IGameObjectsFactory factory, IOutputText output, BaseKeyboardHandler keyboardHandler, List<GameObject> gameObjects )
        {
            this.automat = automat;
            this.output = output;
            this.factory = factory;
            this.gameObjects = gameObjects;

            gameObjects.Clear();
            score = 0;
            player = factory.GetPlayer();
            player.OnDie = OnPlayerDie;
            gameObjects.Add(player);
            this.keyboardHandler = keyboardHandler;
            keyboardHandler.SetPlayer(player);
        }

        public void Update(GameTime gameTime, Rectangle windowBounds, Drawer drawer)
        {
            gameObjects.RemoveAll(x => !x.IsAlive);

            keyboardHandler.HandleInput(Keyboard.GetState(), gameObjects);

            if (RandomUtility.IsOneOf(100))
            {
                var asteroid = factory.GetAsteroid(player);
                asteroid.OnDie = OnNonPlayerDie;
                gameObjects.Add(asteroid);
            }

            if (RandomUtility.IsOneOf(200))
            {
                var ufo = factory.GetUfo(player);
                ufo.OnDie = OnNonPlayerDie;
                gameObjects.Add(ufo);
            }

            var initialCount = gameObjects.Count;
            for (int i = 0; i < initialCount; i++)
            {
                var gameObject = gameObjects[i];
                gameObject.UpdatePosition(gameTime, windowBounds, drawer);
                gameObject.CollideWith(gameObjects, drawer);
            }
        }

        public void Draw(GameTime gameTime, Rectangle windowBounds, Drawer drawer)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Draw(drawer);
            }

            output.OutputText(new Vector2(windowBounds.Width - 80, 30), 1, String.Format("SCORE: {0}", score));
        }

        public void OnPlayerDie(GameObject sender)
        {
            automat.SetState(new GameOverState(automat, factory, output, keyboardHandler, gameObjects, score));
        }

        public void OnNonPlayerDie(GameObject sender)
        {
            score += sender.PointValue;
        }
    }
}
