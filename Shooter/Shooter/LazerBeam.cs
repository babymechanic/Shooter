using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;

namespace Shooter
{
    public class LazerBeam : Destroyable, IDynamicGameObject
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly float zIndex;
        private readonly float speed;
        private readonly Texture2D texture;
        public int Damage { get; private set; }

        public LazerBeam(Texture2D texture, Vector2 position, GraphicsDevice graphicsDevice,float zIndex) : 
        base(1,position)
        {
            this.texture = texture;
            this.graphicsDevice = graphicsDevice;
            this.zIndex = zIndex;
            Damage = 2;
            speed = 20f;
        }

        public override int Width
        {
            get { return texture.Width; }
        }

        public override int Height
        {
            get { return texture.Height; }
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, List<IDynamicGameObject> gameObjects)
        {
            Position = new Vector2(Position.X + speed, Position.Y);
            if (Position.X + texture.Width / 2 > graphicsDevice.Viewport.Width)
                Die();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None,zIndex);
            }
        }

        protected override void AfterDying()
        {
            
        }
    }
}