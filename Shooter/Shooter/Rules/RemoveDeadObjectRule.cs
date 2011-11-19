using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;

namespace Shooter.Rules
{
    public class RemoveDeadObjectRule : IGameRule
    {
        private static readonly Type DestroyableType = typeof(Destroyable);
        public void Apply(List<IDynamicGameObject> gameObjects, GraphicsDevice graphicsDevice)
        {
            gameObjects.RemoveAll(x => DestroyableType.IsAssignableFrom(x.GetType()) 
                                    && !((Destroyable) x).IsAlive);
        }
    }
}