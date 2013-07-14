using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Factories;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Asteroids.Strategies;

namespace Asteroids.GameObjects
{
    public class AsteroidPiece : GameObject
    {
        const float SPEED = 50.0f;
        const int POINT_VALUE = 100;
        const Team TEAM = GameObjects.Team.Enemy;

        public AsteroidPiece(IGameObjectsFactory factory, Asteroid asteroid) : base(factory)
        {   
            this.Position = asteroid.Position;
            this.Speed = SPEED;
            this.PointValue = POINT_VALUE;
            this.Team = TEAM;
            this.MoveStrategy = new StraightMoveStrategy(this);
            this.Team = GameObjects.Team.Enemy;
        }

        public override string GetTypeName()
        {
            return TypeNames.AsteroidPiece;
        }
    }
}
