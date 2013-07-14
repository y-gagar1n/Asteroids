using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Drawers;
using Asteroids.Factories;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Timers;
using Asteroids.Helpers;

namespace Asteroids.GameObjects
{
    public class LaserBeam : GameObject
    {
        const int RESTORE_PERIOD_MILLISECONDS = 3000;
        static DateTime LastUseTime;

        private LineBatch lineBatch;

        public LaserBeam(IGameObjectsFactory factory, LineBatch lineBatch, Player player):base(factory)
        {
            this.lineBatch = lineBatch;

            if ((DateTime.Now - LastUseTime).TotalMilliseconds >= RESTORE_PERIOD_MILLISECONDS)
            {
                this.Rotation = player.Rotation;
                this.Position = player.Position;
                this.MovementDirection = player.RotationVector;
                this.Team = GameObjects.Team.Ally;
            }
            else
            {
                Die();
            }
        }

        public override void Draw(Drawer drawer)
        {
            if (IsAlive)
            {
                lineBatch.DrawInfiniteLine(Color.Red, this.Position, this.Position + this.MovementDirection);
                LastUseTime = DateTime.Now;
                Die();
            }
        }

        public override bool IsCollisionWith(GameObject anotherObject, Drawer drawer)
        {
            if (!CanCollide(anotherObject))
            {
                return false;
            }

            var ray = new Ray(new Vector3(this.Position.X, this.Position.Y, 0), new Vector3(this.MovementDirection.X, this.MovementDirection.Y, 0));

            var rect = drawer.GetRectangle(anotherObject);
            var box = new BoundingBox(new Vector3(rect.X, rect.Y, 0),
                new Vector3(rect.X + rect.Width, rect.Y + rect.Height, 0));

            return ray.Intersects(box).HasValue;
        }

        protected override void Collide(GameObject gameObject)
        {
            gameObject.Die();
        }

        public override string GetTypeName()
        {
            return TypeNames.LaserBeam;
        }
        
    }
}
