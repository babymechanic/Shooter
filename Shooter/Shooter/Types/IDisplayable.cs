using Microsoft.Xna.Framework.Graphics;

namespace Shooter.Types
{
    public interface IDisplayable
    {
        void Draw(SpriteBatch spriteBatch);
        int ZIndex { get; }
    }
}