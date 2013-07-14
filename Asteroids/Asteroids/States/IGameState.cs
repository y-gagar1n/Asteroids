using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Drawers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.States
{
    public interface IGameState
    {
        void Update(GameTime gameTime, Rectangle windowBounds, Drawer drawer);
        void Draw(GameTime gameTime, Rectangle windowBounds, Drawer drawer);
    }
}
