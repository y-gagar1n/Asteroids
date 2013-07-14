using System.Collections.Generic;
using Asteroids.Factories;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Asteroids.Strategies;


namespace Asteroids.GameObjects
{
    public class Missile : GameObject
    {
        const float SPEED = 400.0f;

        public Missile(Player player) : base()
        {
            this.MoveStrategy = new StraightMoveStrategy(this);
            this.Position = player.Position;
            this.MovementDirection = player.RotationVector;
            this.Rotation = player.Rotation;
            this.Speed = SPEED + player.Speed;
            this.Team = GameObjects.Team.Ally;
        }

        public override string GetTypeName()
        {
            return TypeNames.Missile;
        }
    }
}
