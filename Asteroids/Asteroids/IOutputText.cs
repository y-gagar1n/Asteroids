using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    public interface IOutputText
    {
        void OutputText(Vector2 position, float scale, string text);
    }
}
