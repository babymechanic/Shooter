using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.GameObjects;
using Shooter.Rules;
using Shooter.Types;

namespace Shooter
{
    public class Shooter : Game
    {
        SpriteBatch spriteBatch;
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
            enemyMotherShip = new EnemyMotherShip(Content, GraphicsDevice);
            gameObjects = new List<IDynamicGameObject>();
            gameRules = new List<IGameRule>
                            {
                                new SpaceShipVsEnemyColisionRule(Content),
                                new LazerHittingEnemyRule(Content),
                                new RemoveDeadObjectRule(),
                                new RemoveOutOfScreenEnemiesRule(),
                                new RemoveOutOfScreenLazerBeamsRule()
                            };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameObjects.Add(new Background(Content, "mainBackGround", GraphicsDevice, 0,1f));
            gameObjects.Add(new Background(Content, "bgLayer1", GraphicsDevice, -1,.09f));
            gameObjects.Add(new Background(Content, "bgLayer2", GraphicsDevice, -2,.08f));
            gameObjects.Add(new SpaceShip(Content, "shipAnimation", 8, 8.0f, GraphicsDevice,0f));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            gameObjects.ForEach(x => x.Update(gameTime, keyboardState,gameObjects));
            gameRules.ForEach(x => x.Apply(gameObjects, GraphicsDevice));
            enemyMotherShip.GenerateEnemy(gameTime, gameObjects);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.AlphaBlend);
            gameObjects.ForEach(x=>x.Draw(spriteBatch));
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
