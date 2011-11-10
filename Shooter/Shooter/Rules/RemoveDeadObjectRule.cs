using System;
using System.Collections.Generic;
using Shooter.Types;

namespace Shooter.Rules
{
    public class RemoveDeadObjectRule : IGameRule
    {
        private static readonly Type DestroyableType = typeof(Destroyable);
        public void Apply(List<IDynamicGameObject> gameObjects)
        {
            var destroyableObjects = gameObjects.FindAll(x=>DestroyableType.IsAssignableFrom(x.GetType()));
            foreach(var gameObject in destroyableObjects )
            {
                if (!((Destroyable)gameObject).IsAlive)
                    gameObjects.Remove(gameObject);
            }
        }
    }
}