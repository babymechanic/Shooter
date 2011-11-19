using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;

namespace Shooter
{
    public class SpaceShip : Destroyable ,IDynamicGameObject
    {
        private readonly float speed;
        private readonly GraphicsDevice graphicsDevice;
        private readonly Animation animation;
        private LazerCannon lazerCannon;

        public SpaceShip(ContentManager contentManager, string spriteName, int numberOfFramesInSprite, float speed,GraphicsDevice graphicsDevice, float zIndex)
            : base(100, new Vector2(graphicsDevice.Viewport.TitleSafeArea.X + ((contentManager.Load<Texture2D>(spriteName).Width / 8) / 2), 
                                    graphicsDevice.Viewport.TitleSafeArea.Y + graphicsDevice.Viewport.Height / 2))
        {
            this.speed = speed;
            this.graphicsDevice = graphicsDevice;
            var texture = contentManager.Load<Texture2D>(spriteName);
            animation = new Animation(texture, Vector2.Zero, numberOfFramesInSprite, 30, 1f, true,zIndex);
            Position = new Vector2(graphicsDevice.Viewport.TitleSafeArea.X + ((texture.Width / 8) / 2), 
                                   graphicsDevice.Viewport.TitleSafeArea.Y + graphicsDevice.Viewport.Height / 2);
            lazerCannon = new LazerCannon(contentManager, graphicsDevice, Width);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, List<IDynamicGameObject> gameObjects)
        {
            
            animation.Position = Position;
            animation.Update(gameTime);
            Move(keyboardState);
            lazerCannon.Fire(gameObjects, gameTime, Position);
        }

        private void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
                Position = new Vector2(Position.X - speed, Position.Y);
            if (keyboardState.IsKeyDown(Keys.Right))
                Position = new Vector2(Position.X + speed, Position.Y);
            if (keyboardState.IsKeyDown(Keys.Up))
                Position = new Vector2(Position.X, Position.Y - speed);
            if (keyboardState.IsKeyDown(Keys.Down))
                Position = new Vector2(Position.X, Position.Y + speed);
            var clampedX = MathHelper.Clamp(Position.X, Width/2, graphicsDevice.Viewport.Width - Width/2);
            var clampedY = MathHelper.Clamp(Position.Y, Height/2, graphicsDevice.Viewport.Height - Height/2);
            Position = new Vector2(clampedX, clampedY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
 
        public override int Width
        {
            get { return animation.FrameWidth; }
        }

        public override int Height
        {
            get { return animation.FrameHeight; }
        }

        protected override void AfterDying()
        {

        }
    }
}