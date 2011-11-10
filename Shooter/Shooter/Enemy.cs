using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;

namespace Shooter
{
    public class Enemy : Destroyable, IDynamicGameObject
    {
        private readonly Animation animation;
        private int value;
        private readonly float speed;

        public Enemy(Texture2D texture, int numberOfFrames,Vector2 position) : base(10,position)
        {
            Damage = 10;
            speed = 6f;
            value = 100;
            animation = new Animation(texture, Vector2.Zero, numberOfFrames, 30, Color.White, 1f, true);
        }

        protected override void AfterDying()
        {
        }

        public int Damage
        {
            get; private set;
        }

        protected override int Width
        {
            get { return animation.FrameWidth; }
        }

        protected override int Height
        {
            get { return animation.FrameHeight; }
        }

        public void Update(GameTime gameTime,KeyboardState keyboardState)
        {
            Position = new Vector2(Position.X-speed,Position.Y);
            animation.Position = Position;
            animation.Update(gameTime);
            if (Position.X < -Width || Health <= 0)
            {
                Die();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }


        public void ApplyDamage(IEnumerable<LazerBeam> projectilesHittingEnemy)
        {
            foreach (var projectile in projectilesHittingEnemy)
                ApplyDamage(projectile.Damage);
        }
    }
}