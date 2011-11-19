using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;

namespace Shooter.Rules
{
    class RemoveOutOfScreenEnemies : IGameRule
    {
        private static readonly Type EnemyType = typeof(Enemy);
        
        public void Apply(List<IDynamicGameObject> gameObjects, GraphicsDevice graphicsDevice)
        {
            gameObjects.RemoveAll(x => EnemyType.IsAssignableFrom(x.GetType())
                                    && ((Enemy) x).Position.X < -((Enemy) x).Width);
        }
    }
}