using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Asteroids.GameObjects;

namespace Asteroids.Drawers
{
    public abstract class Drawer
    {
        public abstract Rectangle GetRectangle(GameObject gameObject);

        public abstract void Draw(GameObject gameObject);
    }
}
