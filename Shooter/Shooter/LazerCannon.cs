using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;

namespace Shooter
{
    public class LazerCannon
    {
        private readonly ContentManager contentManager;
        private readonly GraphicsDevice graphicsDevice;
        private readonly int spaceShipWidth;
        private readonly SoundEffect lazerSound;
        private readonly TimeSpan timeBtwEachShot;
        private TimeSpan projectileLastFiredTime;

        public LazerCannon(ContentManager contentManager, GraphicsDevice graphicsDevice,int spaceShipWidth)
        {
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.spaceShipWidth = spaceShipWidth;
            lazerSound = contentManager.Load<SoundEffect>("sound/laserFire");
            timeBtwEachShot = TimeSpan.FromSeconds(.15f);
        }

        public void Fire(List<IDynamicGameObject> gameObjects,GameTime gameTime,Vector2 spaceShipPosition)
        {
            if (gameTime.TotalGameTime - projectileLastFiredTime > timeBtwEachShot)
            {
                gameObjects.Add(new LazerBeam("laser", contentManager, new Vector2(spaceShipPosition.X + spaceShipWidth / 2,spaceShipPosition.Y),graphicsDevice, .01f));
                lazerSound.Play();
                projectileLastFiredTime = gameTime.TotalGameTime;
            }
        }
    }
}