using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Asteroids.GameObjects;
using Asteroids.Helpers;

namespace Asteroids.Drawers
{
    public class PolygonDrawer : Drawer
    {
        private PolygonBatch polygonBatch;

        private Dictionary<string, List<Vector2>> Resources = new Dictionary<string, List<Vector2>>();
        private Dictionary<string, Rectangle> ObjectBounds = new Dictionary<string, Rectangle>();

        public PolygonDrawer(PolygonBatch polygonBatch)
        {
            this.polygonBatch = polygonBatch;

            InitResources();
            InitObjectBounds();
        }

        public override Rectangle GetRectangle(GameObject gameObject)
        {
            var objectBounds = ObjectBounds[gameObject.GetTypeName()];

            var absoluteRectangle = new Rectangle(
                (int)(objectBounds.X + gameObject.Position.X),
                (int)(objectBounds.Y + gameObject.Position.Y),
                objectBounds.Width,
                objectBounds.Height
                );
            return absoluteRectangle;
        }

        public override void Draw(GameObject gameObject)
        {
            var type = gameObject.GetTypeName();
            if (Resources.ContainsKey(type))
            {
                var relativeVertexes = Resources[type];

                var absoluteVertexes = new List<Vector2>();

                foreach (var vertex in relativeVertexes)
                {
                    var rotatedVertex = RotateVertex(vertex, gameObject);
                    var movedRotatedVertex = MoveVertex(rotatedVertex, gameObject);
                    absoluteVertexes.Add(movedRotatedVertex);
                }

                polygonBatch.DrawPolygon(absoluteVertexes.ToArray(), absoluteVertexes.Count(), Color.White, 10,
                                         gameObject.Rotation);
            }
        }

        Vector2 RotateVertex(Vector2 source, GameObject gameObject)
        {
            var angle = gameObject.Rotation;

            return new Vector2(
                (float)(source.X * Math.Cos(angle) + source.Y * (-Math.Sin(angle))),
                (float)(source.X * Math.Sin(angle) + source.Y * Math.Cos(angle))
            );            
        }

        Vector2 MoveVertex(Vector2 source, GameObject gameObject)
        {
            return new Vector2(source.X + gameObject.Position.X, source.Y + gameObject.Position.Y);
        }

        void InitResources()
        {
            Resources[""] = new List<Vector2>();

            Resources[TypeNames.Asteroid] = new List<Vector2> {
                new Vector2(-50, 0),
                new Vector2(-32, -50),
                new Vector2(32, -50),
                new Vector2(50, 0),
                new Vector2(32, 50),
                new Vector2(-32, 50)
            };

            Resources[TypeNames.AsteroidPiece] = new List<Vector2> {
                new Vector2(-25, 0),
                new Vector2(-16, -25),
                new Vector2(16, -25),
                new Vector2(25, 0),
                new Vector2(16, 25),
                new Vector2(-16, 25)
            };

            Resources[TypeNames.Missile] = new List<Vector2> {
                new Vector2(-4, 12),
                new Vector2(-4, -6),
                new Vector2(0, -12),
                new Vector2(4, -6),
                new Vector2(4, 12),
            };

            Resources[TypeNames.Player] = new List<Vector2> {
                new Vector2(0, -32),
                new Vector2(25, 32),
                new Vector2(0, 10),
                new Vector2(-25,32)
            };

            Resources[TypeNames.Ufo] = new List<Vector2> {
                new Vector2(-26, 0),
                new Vector2(-13, -7),
                new Vector2(-7, -15),
                new Vector2(7,-15),
                new Vector2(13,-7),
                new Vector2(26,0),
                new Vector2(9, 7),
                new Vector2(7, 15),
                new Vector2(5, 8),
                new Vector2(2, 8),
                new Vector2(0, 15),
                new Vector2(-2, 8),
                new Vector2(-5, 8),
                new Vector2(-7, 15),
                new Vector2(-9, 7)
            };
        }

        void InitObjectBounds()
        {
            ObjectBounds[""] = default(Rectangle);
            ObjectBounds[TypeNames.Asteroid] = new Rectangle(-50, -50, 100, 100);
            ObjectBounds[TypeNames.AsteroidPiece] = new Rectangle(-25, -25, 50, 50);
            ObjectBounds[TypeNames.Missile] = new Rectangle(-32, -32, 64, 64);
            ObjectBounds[TypeNames.Player] = new Rectangle(-32, -32, 64, 64);
            ObjectBounds[TypeNames.Ufo] = new Rectangle(-26, -26, 52, 52);
        }
    }
}
