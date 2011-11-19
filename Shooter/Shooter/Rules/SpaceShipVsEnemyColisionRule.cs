using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;
using System.Linq;

namespace Shooter.Rules
{
    class SpaceShipVsEnemyColisionRule : IGameRule
    {
        private readonly ContentManager contentManager;
        private static readonly Type SpaceShipType = typeof(SpaceShip);
        private static readonly Type EnemyType = typeof(Enemy);
        private readonly SoundEffect explosionSound;

        public SpaceShipVsEnemyColisionRule(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            explosionSound = contentManager.Load<SoundEffect>("sound/explosion");
        }

        public void Apply(List<IDynamicGameObject> gameObjects, GraphicsDevice graphicsDevice)
        {
            var spaceShip = (SpaceShip)gameObjects.Find(x => SpaceShipType.IsAssignableFrom(x.GetType()));
            var enemies = gameObjects.Where(x=>EnemyType.IsAssignableFrom(x.GetType()))
                                     .Select(x=>x as Enemy);
            var colidingEnemies = enemies.ToList().FindAll(x =>x.IsAlive && x.ColidesWith(spaceShip));
            foreach (var enemy in colidingEnemies)
            {
                enemy.Die();
                gameObjects.Add(new Explosion("explosion", contentManager, 12, enemy.Position,.06f));
                explosionSound.Play();
            }
        }
    }
}