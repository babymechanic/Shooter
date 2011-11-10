using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;

namespace Shooter
{
    public class SpaceShip : Destroyable ,IDynamicGameObject
    {
        private readonly ContentManager contentManager;
        private readonly float speed;
        private readonly List<LazerBeam> projectiles;
        private readonly GraphicsDevice graphicsDevice;
        private readonly int zIndex;
        private readonly Animation animation;
        private readonly TimeSpan projectileFiringInterval;
        private TimeSpan projectileLastFiredTime;
        private readonly SoundEffect lazerSound;

        public SpaceShip(ContentManager contentManager, string spriteName, int numberOfFramesInSprite, float speed,List<LazerBeam> projectiles, GraphicsDevice graphicsDevice, int zIndex)
            : base(100, new Vector2(graphicsDevice.Viewport.TitleSafeArea.X + ((contentManager.Load<Texture2D>(spriteName).Width / 8) / 2), graphicsDevice.Viewport.TitleSafeArea.Y + graphicsDevice.Viewport.Height / 2))
        {
            this.contentManager = contentManager;
            this.speed = speed;
            this.projectiles = projectiles;
            this.graphicsDevice = graphicsDevice;
            this.zIndex = zIndex;
            var texture = contentManager.Load<Texture2D>(spriteName);
            lazerSound = contentManager.Load<SoundEffect>("sound/laserFire");
            animation = new Animation(texture, Vector2.Zero, numberOfFramesInSprite, 30, Color.White, 1f, true);
            Position = new Vector2(graphicsDevice.Viewport.TitleSafeArea.X + ((texture.Width / 8) / 2), graphicsDevice.Viewport.TitleSafeArea.Y + graphicsDevice.Viewport.Height / 2);
            projectileFiringInterval = TimeSpan.FromSeconds(.15f);
            
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            FireWeapon(gameTime,graphicsDevice);
            animation.Position = Position;
            animation.Update(gameTime);
            if (keyboardState.IsKeyDown(Keys.Left))
                Position = new Vector2(Position.X -  speed,Position.Y);
            if (keyboardState.IsKeyDown(Keys.Right))
                Position = new Vector2(Position.X + speed, Position.Y);
            if (keyboardState.IsKeyDown(Keys.Up))
                Position = new Vector2(Position.X, Position.Y - speed);
            if (keyboardState.IsKeyDown(Keys.Down))
                Position = new Vector2(Position.X, Position.Y + speed);
            var clampedX = MathHelper.Clamp(Position.X, Width / 2, graphicsDevice.Viewport.Width - Width / 2);
            var clampedY = MathHelper.Clamp(Position.Y, Height / 2, graphicsDevice.Viewport.Height - Height / 2);
            Position = new Vector2(clampedX,clampedY);
        }

        private void FireWeapon(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (gameTime.TotalGameTime - projectileLastFiredTime > projectileFiringInterval)
            {
                projectiles.Add(new LazerBeam("laser", contentManager, Position + new Vector2(Width / 2, 0), graphicsDevice,6));
                lazerSound.Play();
                projectileLastFiredTime = gameTime.TotalGameTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }

        public int ZIndex
        {
            get { return zIndex; }
        }

        protected override int Width
        {
            get { return animation.FrameWidth; }
        }

        protected override int Height
        {
            get { return animation.FrameHeight; }
        }

        protected override void AfterDying()
        {

        }
    }
}