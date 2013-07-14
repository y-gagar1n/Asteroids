using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Asteroids.Factories
{
    public interface IGameObjectsFactory
    {
        Player GetPlayer();
        Asteroid GetAsteroid(Player player);
        AsteroidPiece GetAsteroidPiece(Asteroid asteroid);
        Missile GetMissile(Player player);
        Ufo GetUfo(Player player);
        LaserBeam GetLaserBeam(Player player);
    }
}
