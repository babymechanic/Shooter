using System.Collections.Generic;
using Shooter.Types;

namespace Shooter.Rules
{
    internal interface IGameRule
    {
        void Apply(List<IDynamicGameObject> gameObjects);
    }
}