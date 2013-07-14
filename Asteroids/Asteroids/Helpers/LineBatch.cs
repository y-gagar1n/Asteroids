using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Asteroids.Helpers
{
    /// <summary>
    /// Line Batch
    /// For drawing lines in a spritebatch
    /// </summary>
    public class LineBatch
    {
        static private Texture2D _empty_texture;
        static private bool _set_data = false;
        static float maxLength;

        private SpriteBatch spriteBatch;

        public LineBatch(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            _empty_texture = new Texture2D(graphicsDevice, 1, 1, true, SurfaceFormat.Color);
            maxLength = (float)Math.Sqrt(Math.Pow(graphicsDevice.Viewport.Height, 2) + Math.Pow(graphicsDevice.Viewport.Width, 2));
        }
                
        public void DrawLine(Color color, Vector2 point1,
                                    Vector2 point2)
        {
            DrawLine(color, point1, point2, Vector2.Distance(point1, point2));
        }

        private void DrawLine(Color color, Vector2 point1,
                            Vector2 point2, float length)
        {
            if (!_set_data)
            {
                _empty_texture.SetData(new[] { Color.White });
                _set_data = true;
            }


            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            spriteBatch.Draw(_empty_texture, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, 1),
                       SpriteEffects.None, 0);
        }

        public void DrawInfiniteLine(Color color, Vector2 point1,
                                    Vector2 point2)
        {
            DrawLine(color, point1, point2, maxLength);
        }
    }
}
