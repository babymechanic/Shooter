using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Shooter.Types;
using System.Linq;

namespace Shooter.Rules
{
    class LazerHittingEnemyRule : IGameRule
    {
        private readonly ContentManager contentManager;
        private static readonly Type LazerBeamType = typeof(LazerBeam);
        private static readonly Type EnemyType = typeof(Enemy);
        private SoundEffect explosionSound;

        public LazerHittingEnemyRule(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            explosionSound = contentManager.Load<SoundEffect>("sound/explosion");
        }

        public void Apply(List<IDynamicGameObject> gameObjects)
        {
            var enemies = gameObjects.Where(x=>EnemyType.IsAssignableFrom(x.GetType())).Select(x=>(Enemy)x);
            var lazerBeams = gameObjects.Where(x=>LazerBeamType.IsAssignableFrom(x.GetType())).Select(x=>(LazerBeam)x);

            foreach (var lazerBeam in lazerBeams.Where(x=>x.IsAlive))
            {
                var collidingLiveEnemies = enemies.Where(x=>x.IsAlive && x.ColidesWith(lazerBeam));
                foreach (var collidingEnemy in collidingLiveEnemies)
                {
                    collidingEnemy.ApplyDamage(lazerBeam.Damage);
                    lazerBeam.Die();
                    if (!collidingEnemy.IsAlive)
                    {
                        gameObjects.Add(new Explosion("explosion", contentManager, 12, collidingEnemy.Position, 6));
                        explosionSound.Play();
                    }
                }
            }
        }
    }
}