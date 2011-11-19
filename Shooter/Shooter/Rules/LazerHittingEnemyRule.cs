using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;
using System.Linq;

namespace Shooter.Rules
{
    class LazerHittingEnemyRule : IGameRule
    {
        private readonly ContentManager contentManager;
        private static readonly Type LazerBeamType = typeof(LazerBeam);
        private static readonly Type EnemyType = typeof(Enemy);
        private readonly SoundEffect explosionSound;

        public LazerHittingEnemyRule(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            explosionSound = contentManager.Load<SoundEffect>("sound/explosion");
        }

        public void Apply(List<IDynamicGameObject> gameObjects, GraphicsDevice graphicsDevice)
        {
            var enemies = gameObjects.FindAll(x=>EnemyType.IsAssignableFrom(x.GetType())).Select(x=>(Enemy)x).ToList();
            var lazerBeams = gameObjects.FindAll(x => LazerBeamType.IsAssignableFrom(x.GetType())).Select(x => (LazerBeam)x).ToList();
            var liveLazerBeams = lazerBeams.FindAll(x=>x.IsAlive);
            foreach (var lazerBeam in liveLazerBeams)
            {
                var collidingLiveEnemies = enemies.Where(x=>x.IsAlive && x.ColidesWith(lazerBeam)).ToList();
                foreach (var collidingEnemy in collidingLiveEnemies)
                {
                    collidingEnemy.ApplyDamage(lazerBeam.Damage);
                    lazerBeam.Die();
                    if (collidingEnemy.IsAlive) continue;
                    gameObjects.Add(new Explosion("explosion", contentManager, 12, collidingEnemy.Position, .06f));
                    explosionSound.Play();
                }
            }
        }
    }
}