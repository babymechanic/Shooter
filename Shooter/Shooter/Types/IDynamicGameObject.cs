using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Shooter.Types
{
    public interface IDynamicGameObject : IDisplayable
    {
        void Update(GameTime gameTime, KeyboardState keyboardState);
    }
}