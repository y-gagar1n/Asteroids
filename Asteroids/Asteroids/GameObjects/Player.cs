using System;
using System.Collections.Generic;
using Asteroids.Factories;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Asteroids.Strategies;


namespace Asteroids.GameObjects
{
    public class Player : GameObject
    {
        const float MAX_SPEED = 400.0f;
        const float SPEED_INCREASE_STEP = 10.0f;
        const float SPEED_DECREASE_STEP = 10.0f;
        const Team TEAM = GameObjects.Team.Ally;

        public Player(IGameObjectsFactory factory) : base(factory)
        {
            this.MoveStrategy = new StraightMoveStrategy(this);
            this.Speed = 0;
            this.Team = TEAM;

            this.IsDestroyable = true;
        }

        public override void LeaveWindow(Rectangle screenBounds)
        {
            if (this.Position.X < 0)
            {
                this.Position = new Vector2(screenBounds.Width, this.Position.Y);
            }
            else if (this.Position.X > screenBounds.Width)
            {
                this.Position = new Vector2(0, this.Position.Y);
            }

            if (this.Position.Y < 0)
            {
                this.Position = new Vector2(this.Position.X, screenBounds.Height);
            }
            else if (this.Position.Y > screenBounds.Height)
            {
                this.Position = new Vector2(this.Position.X, 0);
            }

        }

        public override void Die()
        {            
            base.Die();
        }

        public void SpeedUp()
        {            
            Speed = MathHelper.Clamp(Speed + SPEED_INCREASE_STEP, 0, MAX_SPEED);
        }

        public void SlowDown()
        {
            Speed = MathHelper.Clamp(Speed - SPEED_DECREASE_STEP, 0, MAX_SPEED);
        }

        public override string GetTypeName()
        {
            return TypeNames.Player;
        }
    }
}
