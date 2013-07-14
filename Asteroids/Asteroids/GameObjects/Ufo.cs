using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Factories;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Asteroids.Strategies;

namespace Asteroids.GameObjects
{
    public class Ufo : GameObject
    {
        const float SPEED = 150.0f;
        const int POINT_VALUE = 1000;
        const Team TEAM = GameObjects.Team.Enemy;
        const int MIN_PLAYER_DISTANCE = 300;

        static Random rnd = new Random();

        public Ufo(IGameObjectsFactory factory, Player player, Rectangle screenBounds) : base(factory)
        {
            this.Speed = SPEED;
            this.PointValue = POINT_VALUE;
            this.Team = TEAM;

            do
            {
                this.Position = new Vector2(
                    (float)rnd.NextDouble() * screenBounds.Width,
                    (float)rnd.NextDouble() * screenBounds.Height);
            } while (Vector2.Distance(this.Position, player.Position) <= MIN_PLAYER_DISTANCE);

            this.MoveStrategy = new FollowingMoveStrategy(this, player);
        }

        public override string GetTypeName()
        {
            return TypeNames.Ufo;
        }
    }
}
