using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Asteroids.GameObjects;

namespace Asteroids.Strategies
{
    public class FollowingMoveStrategy : StraightMoveStrategy
    {        
        GameObject followee;

        public FollowingMoveStrategy(GameObject gameObject, GameObject followee)
        {
            this.gameObject = gameObject;
            this.followee = followee;
        }

        public override void Move(GameTime gameTime)
        {
            var angle = (float)Math.Atan2(followee.Position.Y - gameObject.Position.Y, followee.Position.X - gameObject.Position.X);

            gameObject.MovementDirection = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            
            base.Move(gameTime);
        }
    }
}
