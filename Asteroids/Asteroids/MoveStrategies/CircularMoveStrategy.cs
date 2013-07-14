using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Asteroids.Strategies;
using Asteroids.GameObjects;

namespace Asteroids
{
    public class CircularMoveStrategy : BaseMoveStrategy
    {
        float? previousAngle;
        public CircularMoveStrategy()
        {
        }

        public CircularMoveStrategy(GameObject gameObject)
            : base(gameObject)
        {
        }

        public override void Move(GameTime gameTime)
        {            
            if (previousAngle == null)
            {
                previousAngle = (float?)Math.Acos(gameObject.MovementDirection.X);
            }
            else
            {
                this.previousAngle = (float?)(previousAngle + 0.1f);
            }

            gameObject.MovementDirection = new Vector2((float)Math.Cos(previousAngle.Value + 0.1f), (float)Math.Sin(previousAngle.Value + 0.1f));
            
            base.Move(gameTime);
        }
    }
}
