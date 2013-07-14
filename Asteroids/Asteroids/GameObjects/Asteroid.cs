using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Asteroids.Strategies;
using Asteroids.GameObjects;
using Asteroids.Factories;

namespace Asteroids.GameObjects
{
    public class Asteroid : GameObject
    {        
        const float SPEED = 100.0f;
        const int POINT_VALUE = 500;
        const Team TEAM = GameObjects.Team.Enemy;
        const int MIN_PLAYER_DISTANCE = 200;

        static Random rnd = new Random();

        private List<GameObject> gameObjects;

        public Asteroid(IGameObjectsFactory factory, Player player, Rectangle screenBounds, List<GameObject> gameObjects) : base(factory)
        {
            do
            {
                this.Position = new Vector2(
                    (float)rnd.NextDouble() * screenBounds.Width,
                    (float)rnd.NextDouble() * screenBounds.Height);
            } while (Vector2.Distance(this.Position, player.Position) <= MIN_PLAYER_DISTANCE);
                        
            this.Speed = SPEED;
            this.PointValue = POINT_VALUE;
            this.Team = TEAM;
            var angle = MathHelper.Lerp(0, MathHelper.Pi * 2, (float)rnd.NextDouble());
            this.MovementDirection = new Vector2((float)(Math.Cos(angle)), (float)(Math.Sin(angle)));
            this.MoveStrategy = new StraightMoveStrategy(this);
            this.Team = GameObjects.Team.Enemy;
            this.gameObjects = gameObjects;
        }

        public override void Die()
        {
            var piece1 = factory.GetAsteroidPiece(this);
            piece1.MovementDirection = new Vector2(-1, -1);
            piece1.Position = new Vector2(this.Position.X - 25, this.Position.Y - 25);
            var piece2 = factory.GetAsteroidPiece(this);
            piece2.MovementDirection = new Vector2(-1, 1);
            piece2.Position = new Vector2(this.Position.X - 25, this.Position.Y + 25);
            var piece3 = factory.GetAsteroidPiece(this);
            piece3.MovementDirection = new Vector2(1, -1);
            piece3.Position = new Vector2(this.Position.X + 25, this.Position.Y - 25);
            var piece4 = factory.GetAsteroidPiece(this);
            piece4.MovementDirection = new Vector2(1, 1);
            piece4.Position = new Vector2(this.Position.X + 25, this.Position.Y + 25);

            gameObjects.AddRange(new List<AsteroidPiece> {
                piece1, piece2, piece3, piece4
            });

            base.Die();
        }

        public override string GetTypeName()
        {
            return TypeNames.Asteroid;
        }
    }
}
