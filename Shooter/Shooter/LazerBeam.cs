﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;

namespace Shooter
{
    public class LazerBeam : Destroyable, IDynamicGameObject
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly int zIndex;
        public int Damage { get; private set; }
        private readonly float speed;
        private static Texture2D texture;

        public LazerBeam(string spriteName, ContentManager contentManager, Vector2 position, GraphicsDevice graphicsDevice,int zIndex) : base(1,position)
        {
            texture = contentManager.Load<Texture2D>(spriteName);
            this.graphicsDevice = graphicsDevice;
            this.zIndex = zIndex;
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

        public int ZIndex
        {
            get { return zIndex; }
        }

        protected override void AfterDying()
        {
            
        }
    }
}