using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;

namespace Shooter.GameObjects
{
    public class EnemyMotherShip
    {
        private const string ENEMY_SPRITE = "mineAnimation";
        private readonly ContentManager contentManager;
        private readonly GraphicsDevice graphicsDevice;
        private Random random;
        private readonly TimeSpan enemySpawnTime = TimeSpan.FromSeconds(1.0f);
        private TimeSpan previousSpwanTime;
        private Texture2D enemyTexture;

        public EnemyMotherShip(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            random = new Random();
        }

        public void GenerateEnemy(GameTime gameTime, List<IDynamicGameObject> gameObjects)
        {
            enemyTexture = contentManager.Load<Texture2D>(ENEMY_SPRITE);
            if (gameTime.TotalGameTime - previousSpwanTime > enemySpawnTime)
            {
                previousSpwanTime = gameTime.TotalGameTime;
                var position = new Vector2(graphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, graphicsDevice.Viewport.Height - 100));
                var enemy = new Enemy(enemyTexture, 8, position,6);
                gameObjects.Add(enemy);
            }
        }
    }
}