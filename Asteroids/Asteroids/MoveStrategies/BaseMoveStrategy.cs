using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Asteroids.GameObjects;

namespace Asteroids.Strategies
{
    public abstract class BaseMoveStrategy
    {
        public GameObject gameObject;

        protected BaseMoveStrategy()
        {
        }

        protected BaseMoveStrategy(GameObject gameObject)
        {
            this.gameObject = gameObject;   
        }

        public virtual void Move(GameTime gameTime)
        {
            
        }

    }
}