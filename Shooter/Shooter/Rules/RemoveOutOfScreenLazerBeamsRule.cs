using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Types;

namespace Shooter.Rules
{
    class RemoveOutOfScreenLazerBeamsRule : IGameRule
    {
        private static readonly Type LazerBeamType = typeof(LazerBeam);
        
        public void Apply(List<IDynamicGameObject> gameObjects, GraphicsDevice graphicsDevice)
        {
            gameObjects.RemoveAll(x => LazerBeamType.IsInstanceOfType(x) 
                                    && (((LazerBeam) x).Position.X + ((LazerBeam) x).Width/2) > graphicsDevice.Viewport.Width);
        }
    }
}