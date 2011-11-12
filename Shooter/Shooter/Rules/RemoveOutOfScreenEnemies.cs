using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;
using System.Linq;

namespace Shooter.Rules
{
    class RemoveOutOfScreenEnemies : IGameRule
    {
        private static readonly Type EnemyType = typeof(Enemy);
        
        public void Apply(List<IDynamicGameObject> gameObjects, GraphicsDevice graphicsDevice)
        {
            var allEnemies = gameObjects.FindAll(x => x.GetType() == EnemyType).Select(x=>(Enemy) x).ToList();
            allEnemies.FindAll(x=>x.Position.X < -x.Width)
                   .ForEach(x=>gameObjects.Remove(x));
        }
    }
}