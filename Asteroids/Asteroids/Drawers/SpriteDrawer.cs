using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.GameObjects;

namespace Asteroids.Drawers
{
    public class SpriteDrawer : Drawer
    {
        private SpriteBatch spriteBatch;

        private Dictionary<String, Texture2D> Resources = new Dictionary<string, Texture2D>();

        public SpriteDrawer(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            this.spriteBatch = spriteBatch;

            InitResources(contentManager);
        }

        public override Rectangle GetRectangle(GameObject gameObject)
        {
            var type = gameObject.GetTypeName();
            if (Resources.ContainsKey(type))
            {
                var sprite = Resources[type];
                if (sprite != null)
                {

                    return new Rectangle((int) (gameObject.Position.X - sprite.Width/2),
                                         (int) (gameObject.Position.Y - sprite.Height/2),
                                         (int) sprite.Width,
                                         (int) sprite.Height);
                }
            }

            return default(Rectangle);
        }

        public override void Draw(GameObject gameObject)
        {
            var sprite = Resources[gameObject.GetTypeName()];
            if (sprite != null)
            {                
                spriteBatch.Draw(sprite, gameObject.Position, null, Color.White, 
                    gameObject.Rotation, new Vector2(sprite.Width / 2, sprite.Height / 2), 1, SpriteEffects.None, 0);
            }
        }  
      
        void InitResources(ContentManager contentManager)
        {
            Resources[""] = null;
            Resources[TypeNames.Asteroid] = contentManager.Load<Texture2D>("asteroid");
            Resources[TypeNames.AsteroidPiece] = contentManager.Load<Texture2D>("asteroid_piece");
            Resources[TypeNames.Missile] = contentManager.Load<Texture2D>("missile");
            Resources[TypeNames.Player] = contentManager.Load<Texture2D>("player");
            Resources[TypeNames.Ufo] = contentManager.Load<Texture2D>("ufo");
        }
    }
}
