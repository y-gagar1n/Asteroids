using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Asteroids.Helpers
{
    public class PolygonBatch
    {
        readonly private LineBatch lineBatch;

        public PolygonBatch(LineBatch lineBatch)
        {
            this.lineBatch = lineBatch;
        }

        public void DrawPolygon(Vector2[] vertex, int count, Color color, int lineWidth, float rotation = 0)
        {
            if (count > 0)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    lineBatch.DrawLine(color, vertex[i], vertex[i + 1]);                    
                }
                lineBatch.DrawLine(color, vertex[count - 1], vertex[0]);                
            }
        }
    }
}
