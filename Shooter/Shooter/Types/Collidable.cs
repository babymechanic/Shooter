using Microsoft.Xna.Framework;

namespace Shooter.Types
{
    public abstract class Collidable
    {
        protected Collidable(Vector2 position)
        {
            Position = position;
        }

        public Vector2 Position { get; set; }
        public abstract int Width { get; }
        public abstract int Height { get; }

        public bool ColidesWith(Collidable other)
        {
            var location1 = other.Location();
            var location2 = Location();
            return location1.Intersects(location2);
        }

        private Rectangle Location()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Width,Height);
        }
    }
}