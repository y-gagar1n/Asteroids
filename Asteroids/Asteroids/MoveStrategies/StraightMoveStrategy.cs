using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Asteroids.GameObjects;

namespace Asteroids.Strategies
{
    public class StraightMoveStrategy : BaseMoveStrategy
    {
        public StraightMoveStrategy()
        {
        }

        public StraightMoveStrategy(GameObject gameObject)
            : base(gameObject)
        {
        }

        public override void Move(GameTime gameTime)
        {
            gameObject.Position += gameObject.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
