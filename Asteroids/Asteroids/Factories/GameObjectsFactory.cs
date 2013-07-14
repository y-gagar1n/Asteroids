using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Drawers;
using Asteroids.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.Helpers;

namespace Asteroids.Factories
{
    public class GameObjectsFactory : IGameObjectsFactory
    {
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;
        private LineBatch lineBatch;
        private GraphicsDevice graphicsDevice;
        private Rectangle screenBounds;
        private List<GameObject> gameObjects;

        public GameObjectsFactory(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice,
                                  List<GameObject> gameObjects)
        {
            this.contentManager = contentManager;
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            this.screenBounds = graphicsDevice.Viewport.Bounds;
            this.lineBatch = new LineBatch(graphicsDevice, spriteBatch);

            this.gameObjects = gameObjects;
        }

        public Player GetPlayer()
        {
            return new Player(this) { Position = new Vector2(screenBounds.Width / 2, screenBounds.Height / 2) };
        }

        public Asteroid GetAsteroid(Player player)
        {
            return new Asteroid(this, player, screenBounds, gameObjects);
        }

        public AsteroidPiece GetAsteroidPiece(Asteroid asteroid)
        {
            return new AsteroidPiece(this, asteroid);
        }

        public Missile GetMissile(Player playerShip)
        {
            return new Missile(playerShip);
        }

        public Ufo GetUfo(Player player)
        {
            return new Ufo(this, player, screenBounds);
        }

        public LaserBeam GetLaserBeam(Player player)
        {
            return new LaserBeam(this, lineBatch, player);
        }
    }
}
