using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Rules;
using Shooter.Types;
using System.Linq;

namespace Shooter
{
    public class Shooter : Game
    {
        SpriteBatch spriteBatch;
        private List<LazerBeam> projectiles;
        private EnemyMotherShip enemyMotherShip;
        private GraphicsDeviceManager graphicsDeviceManager;
        private List<IDynamicGameObject> gameObjects;
        private List<IGameRule> gameRules;

        public Shooter()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            gameObjects = new List<IDynamicGameObject>();
            gameRules = new List<IGameRule>
                            {
                                new SpaceShipVsEnemyColisionRule(Content),
                                new LazerHittingEnemyRule(Content),
                                new RemoveDeadObjectRule()
                            };
            projectiles = new List<LazerBeam>();
            enemyMotherShip = new EnemyMotherShip(Content, GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameObjects.Add(new Background(Content, "mainBackGround", GraphicsDevice, 0,0));
            gameObjects.Add(new Background(Content, "bgLayer1", GraphicsDevice, -1,1));
            gameObjects.Add(new Background(Content, "bgLayer2", GraphicsDevice, -2,2));
            gameObjects.Add(new SpaceShip(Content, "shipAnimation", 8, 8.0f, projectiles, GraphicsDevice,9));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            gameObjects.ForEach(x => x.Update(gameTime, keyboardState));

            projectiles.ForEach(x => x.Update(gameTime, keyboardState));

            gameRules.ForEach(x => x.Apply(gameObjects));
            enemyMotherShip.GenerateEnemy(gameTime, gameObjects);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            projectiles.ForEach(x=>x.Draw(spriteBatch));
            foreach (var dynamicGameObject in gameObjects.OrderBy(x => x.ZIndex))
            {
                dynamicGameObject.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
