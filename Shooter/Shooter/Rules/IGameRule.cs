using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;

namespace Shooter.Rules
{
    internal interface IGameRule
    {
        void Apply(List<IDynamicGameObject> gameObjects, GraphicsDevice graphicsDevice);
    }
}