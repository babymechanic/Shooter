using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;

namespace Shooter
{
    public class Enemy : Destroyable, IDynamicGameObject
    {
        private readonly int zIndex;
        private readonly Animation animation;
        private int value;
        private readonly float speed;

        public Enemy(Texture2D texture, int numberOfFrames,Vector2 position,int zIndex) : base(10,position)
        {
            this.zIndex = zIndex;
            Damage = 10;
            speed = 6f;
            value = 100;
            animation = new Animation(texture, Vector2.Zero, numberOfFrames, 30, 1f, true,.05f);
        }

        protected override void AfterDying()
        {
        }

        public int Damage
        {
            get; private set;
        }

        public override int Width
        {
            get { return animation.FrameWidth; }
        }

        public override int Height
        {
            get { return animation.FrameHeight; }
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, List<IDynamicGameObject> gameObjects)
        {
            Position = new Vector2(Position.X-speed,Position.Y);
            animation.Position = Position;
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }

        public int ZIndex
        {
            get { return zIndex; }
        }
    }
}