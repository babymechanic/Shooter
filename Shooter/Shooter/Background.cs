using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;

namespace Shooter
{
    public class Background : IDynamicGameObject
    {
        private readonly Texture2D sprite;
        private readonly Vector2[] positions;
        private readonly int speed;
        private readonly float zIndex;

        public Background(ContentManager contentManager, string spriteName, GraphicsDevice graphicsDevice, int speed,float zIndex)
        {
            this.speed = speed;
            this.zIndex = zIndex;
            sprite = contentManager.Load<Texture2D>(spriteName);
            positions = new Vector2[graphicsDevice.Viewport.Width / sprite.Width + 1];
            for (var i = 0; i < positions.Length; i++)
            {
                positions[i]= new Vector2(i*sprite.Width,0);
            }
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, List<IDynamicGameObject> gameObjects)
        {
            for (var i = 0; i < positions.Length; i++)
            {
                positions[i].X += speed;
                if (speed<=0)
                {
                    if (positions[i].X <= -sprite.Width)
                        positions[i].X = sprite.Width*(positions.Length - 1);
                }
                else
                {
                    if (positions[i].X >= sprite.Width*(positions.Length - 1))
                        positions[i].X = -sprite.Width;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            positions.ToList().ForEach(x => spriteBatch.Draw(sprite, x,null,Color.White,0f,Vector2.Zero,1f,SpriteEffects.None,zIndex));
        }
    }
}