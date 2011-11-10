using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;

namespace Shooter
{
    public class Explosion : IDynamicGameObject
    {
        private readonly Animation animation;
        
        public Explosion(string spriteName, ContentManager contentManager, int frameCount, Vector2 position)
        {
            animation = new Animation(contentManager.Load<Texture2D>(spriteName), position, frameCount, 45, Color.White, 1f, false);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                animation.Draw(spriteBatch);
            }
        }

        public bool IsActive
        {
            get { return animation.IsActive; }
        }
    }
}