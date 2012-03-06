using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;

namespace Shooter.GameObjects
{
    public class LazerCannon
    {
        private readonly ContentManager contentManager;
        private readonly GraphicsDevice graphicsDevice;
        private readonly int spaceShipWidth;
        private readonly int spaceShipHeight;
        private readonly SoundEffect lazerSound;
        private readonly TimeSpan timeBtwEachShot;
        private TimeSpan projectileLastFiredTime;
        private Texture2D lazerBeamTexture;

        public LazerCannon(ContentManager contentManager, GraphicsDevice graphicsDevice,int spaceShipWidth,int spaceShipHeight)
        {
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.spaceShipWidth = spaceShipWidth;
            this.spaceShipHeight = spaceShipHeight;
            lazerSound = contentManager.Load<SoundEffect>("sound/laserFire");
            lazerBeamTexture = contentManager.Load<Texture2D>("laser");
            timeBtwEachShot = TimeSpan.FromSeconds(.15f);
        }

        public void Fire(List<IDynamicGameObject> gameObjects,GameTime gameTime,Vector2 spaceShipOriginPosition)
        {
            if (gameTime.TotalGameTime - projectileLastFiredTime <= timeBtwEachShot) return;
            var lazerBeamXCoordinate = spaceShipOriginPosition.X;
            var lazerBeamYCoordinate = spaceShipOriginPosition.Y - (lazerBeamTexture.Height /2);
            gameObjects.Add(new LazerBeam(lazerBeamTexture, new Vector2(lazerBeamXCoordinate,lazerBeamYCoordinate), graphicsDevice, .01f));
            lazerSound.Play();
            projectileLastFiredTime = gameTime.TotalGameTime;
        }
    }
}