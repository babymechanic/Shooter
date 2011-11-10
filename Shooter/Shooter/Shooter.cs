using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Types;
using System.Linq;

namespace Shooter
{
    public class Shooter : Game
    {
        SpriteBatch spriteBatch;
        private List<Enemy> enemies;
        private List<LazerBeam> projectiles;
        private List<Explosion> explosions;
        private EnemyMotherShip enemyMotherShip;
        private GraphicsDeviceManager graphicsDeviceManager;
        private DynamicGameObjects dynamicGameObjects;

        public Shooter()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            dynamicGameObjects = new DynamicGameObjects();
            enemies = new List<Enemy>();
            projectiles = new List<LazerBeam>();
            explosions = new List<Explosion>();
            enemyMotherShip = new EnemyMotherShip(Content, GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            dynamicGameObjects.Add(0, new Background(Content, "mainBackGround", GraphicsDevice, 0));
            dynamicGameObjects.Add(1, new Background(Content, "bgLayer1", GraphicsDevice, -1));
            dynamicGameObjects.Add(2, new Background(Content, "bgLayer2", GraphicsDevice, -2));
            dynamicGameObjects.Add(9, new SpaceShip(Content, "shipAnimation", 8, 8.0f, enemies, projectiles, explosions, GraphicsDevice));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            dynamicGameObjects.Values
                              .ToList()
                              .ForEach(x => x.Update(gameTime, keyboardState));

            enemies.ForEach(x => x.Update(gameTime, keyboardState));
            projectiles.ForEach(x => x.Update(gameTime, keyboardState));
            explosions.ForEach(x => x.Update(gameTime, keyboardState));

            enemies.RemoveAll(x => !x.IsAlive);
            enemyMotherShip.GenerateEnemy(gameTime, dynamicGameObjects);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            enemies.ForEach(x => x.Draw(spriteBatch));
            projectiles.ForEach(x=>x.Draw(spriteBatch));
            explosions.ForEach(x=>x.Draw(spriteBatch));
            var keyValuePairs = dynamicGameObjects.OrderBy(x=>x.Key);
            foreach (var pair in keyValuePairs)
            {
                pair.Value.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
