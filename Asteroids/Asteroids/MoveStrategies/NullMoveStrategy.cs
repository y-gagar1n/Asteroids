using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Asteroids.GameObjects;

namespace Asteroids.Strategies
{
    public class NullMoveStrategy : BaseMoveStrategy
    {
        public NullMoveStrategy()
        {
        }

        public override void Move(GameTime gameTime)
        {
            
        }
    }
}
