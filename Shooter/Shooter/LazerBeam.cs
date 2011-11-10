using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;

namespace Shooter
{
    public class LazerBeam : Destroyable, IDynamicGameObject
    {
        private readonly string spriteName;
        private readonly ContentManager contentManager;
        private readonly GraphicsDevice graphicsDevice;
        public int Damage { get; private set; }
        private readonly float speed;
        private static Texture2D texture;

        public LazerBeam(string spriteName, ContentManager contentManager, Vector2 position, GraphicsDevice graphicsDevice) : base(1,position)
        {
            this.spriteName = spriteName;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            Damage = 2;
            speed = 20f;
        }

        protected override int Width
        {
            get { return texture.Width; }
        }

        protected override int Height
        {
            get { return texture.Height; }
        }

        public void Initialize(Vector2 position, Viewport viewport)
        {
            texture = texture ?? contentManager.Load<Texture2D>(spriteName);
            Position = position;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            Position = new Vector2(Position.X+speed,Position.Y);
            if (Position.X + texture.Width / 2 > graphicsDevice.Viewport.Width)
                Die();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                var origin = new Vector2(texture.Width / 2, texture.Height / 2);
                spriteBatch.Draw(texture, Position, null, Color.White, 0f, origin, 1f, SpriteEffects.None,0f);
            }
        }

        protected override void AfterDying()
        {
            
        }
    }
}