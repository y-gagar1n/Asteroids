using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Asteroids.GameObjects;

namespace Asteroids.Strategies
{
    public class RandomMoveStrategy : BaseMoveStrategy
    {
        public RandomMoveStrategy()
        {
        }

        public RandomMoveStrategy(GameObject gameObject)
            : base(gameObject)
        {
        }

        public override void Move(GameTime gameTime)
        {
            var rnd = new Random();
            gameObject.MovementDirection = new Vector2((float)rnd.NextDouble() * 2 - 1, (float)rnd.NextDouble() * 2 - 1);
            base.Move(gameTime);
        }
    }
}
