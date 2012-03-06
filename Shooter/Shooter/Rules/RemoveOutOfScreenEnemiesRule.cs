using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Shooter.GameObjects;
using Shooter.Types;

namespace Shooter.Rules
{
    class RemoveOutOfScreenEnemiesRule : IGameRule
    {
        private static readonly Type EnemyType = typeof(Enemy);
        
        public void Apply(List<IDynamicGameObject> gameObjects, GraphicsDevice graphicsDevice)
        {
            gameObjects.RemoveAll(x => EnemyType.IsInstanceOfType(x)
                                    && ((Enemy) x).Position.X < -((Enemy) x).Width);
        }
    }
}