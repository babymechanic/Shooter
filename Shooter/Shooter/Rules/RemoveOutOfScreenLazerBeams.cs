using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;

namespace Shooter.Rules
{
    class RemoveOutOfScreenLazerBeams : IGameRule
    {
        private static readonly Type LazerBeamType = typeof(LazerBeam);
        
        public void Apply(List<IDynamicGameObject> gameObjects, GraphicsDevice graphicsDevice)
        {
            gameObjects.FindAll(x=>x.GetType() == LazerBeamType)
                .Select(x=>(LazerBeam) x)
                .ToList()
                .FindAll(x => (x.Position.X + x.Width/2) > graphicsDevice.Viewport.Width)
                .ForEach(x=>x.Die());
        }
    }
}