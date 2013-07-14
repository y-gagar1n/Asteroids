using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Drawers;
using Asteroids.Factories;
using Microsoft.Xna.Framework;
using Asteroids.Strategies;

namespace Asteroids.GameObjects
{
    public abstract class GameObject
    {
        protected IGameObjectsFactory factory;
        public bool IsDestroyable;
        public Vector2 Position { get; set; }
        public Vector2 MovementDirection { get; set; }
        public Vector2 Velocity
        {
            get
            {
                return MovementDirection * Speed;
            }
        }
        public float Rotation { get; set; }
        public Vector2 RotationVector
        {
            get
            {
                var x = -Math.Cos(Rotation + MathHelper.PiOver2);
                var y = -Math.Sin(Rotation + MathHelper.PiOver2);
                return new Vector2((float)x, (float)y);
            }
        }
        public float Speed { get; set; }
        public BaseMoveStrategy MoveStrategy { get; set; }
        public bool IsAlive;
        public Team Team;
        public int PointValue { get; set; }

        public Action<GameObject> OnDie;
        
        public virtual bool IsCollisionWith(GameObject anotherObject, Drawer drawer)
        {
            if (!CanCollide(anotherObject))
            {
                return false;
            }

            var thisBounds = drawer.GetRectangle(this);
            var anotherBounds = drawer.GetRectangle(anotherObject);

            if (thisBounds != default(Rectangle) && anotherBounds != default(Rectangle))
            {
                return CanCollide(anotherObject) && thisBounds.Intersects(anotherBounds);
            }

            return false;
        }

        public virtual bool CanCollide(GameObject anotherObject)
        {
            return this.IsAlive && anotherObject.IsAlive && this.Team != anotherObject.Team;
        }

        public GameObject IsCollisionWith(IEnumerable<GameObject> anotherObjects, Drawer drawer)
        {
            return anotherObjects.FirstOrDefault(x => x.IsCollisionWith(this, drawer));
        }

        public void CollideWith(GameObject gameObject, Drawer drawer)
        {
            if (IsCollisionWith(gameObject, drawer))
            {
                Collide(gameObject);
            }
        }

        public void CollideWith(IEnumerable<GameObject> gameObjects, Drawer drawer)
        {   
            var initialCount = gameObjects.Count();
            for (int i = 0; i < initialCount; i++)
            {
                CollideWith(gameObjects.ElementAt(i), drawer);
            }
        }

        protected virtual void Collide(GameObject gameObject)
        {            
            if (this.IsDestroyable)
            {
                this.Die();
            }
            if (gameObject.IsDestroyable)
            {
                gameObject.Die();
            }
        }

        protected GameObject()
        {
            IsAlive = true;
            IsDestroyable = true;

            this.MoveStrategy = new NullMoveStrategy();
        }

        protected GameObject(IGameObjectsFactory factory)
            : this()
        {
            this.factory = factory;
        }
        
        protected GameObject(IGameObjectsFactory factory, BaseMoveStrategy moveStrategy) : this(factory)
        {
            MovementDirection = new Vector2(0, -1);

            this.MoveStrategy = moveStrategy;
            moveStrategy.gameObject = this;
        }

        protected GameObject(IGameObjectsFactory factory, Vector2 position, Vector2 movementDirection, float rotation, float step, BaseMoveStrategy moveStrategy)
            : this(factory, moveStrategy)
        {
            this.Position = position;
            this.MovementDirection = movementDirection;
            this.Rotation = rotation;
            this.Speed = step;
        }

        public void UpdatePosition(GameTime gameTime, Rectangle windowBounds, Drawer drawer)
        {
            MoveStrategy.Move(gameTime);

            var rectangle = drawer.GetRectangle(this);

            if (this.Position.X < -rectangle.Width || this.Position.X > windowBounds.Width + rectangle.Width ||
                this.Position.Y < -rectangle.Height || this.Position.Y > windowBounds.Height + rectangle.Height)
            {
                LeaveWindow(windowBounds);
            }

        }

        public virtual void Draw(Drawer drawer)
        {
            drawer.Draw(this);
        }

        public void Dispose()
        {
            this.IsAlive = false;
        }

        public virtual void LeaveWindow(Rectangle windowBounds)
        {
            this.Dispose();
        }

        public virtual void Die()
        {
            Dispose();

            if (OnDie != null)
            {
                OnDie(this);
            }
        }        

        public virtual string GetTypeName()
        {
            return "";
        }
    }
}
