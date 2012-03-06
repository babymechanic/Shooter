using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter.Utils
{
    public class Animation
    {
        private readonly Texture2D texture;
        private int elapsedTime;
        private readonly int frameTime;
        private readonly int frameCount;
        private int currentFrame;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public bool IsActive { get; private set; }
        private bool looping;
        private readonly float zindex;
        public Vector2 Position;

        public Animation(Texture2D texture, Vector2 position, int frameCount, int frameTime, bool looping,float zindex)
        {
            this.texture = texture;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.looping = looping;
            this.zindex = zindex;
            Position = position;
            FrameHeight = texture.Height;
            FrameWidth = texture.Width / frameCount;
            elapsedTime = 0;
            currentFrame = 0;
            IsActive = true;

        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime > frameTime)
            {
                currentFrame++;
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    if (!looping)
                        IsActive = false;
                }
                elapsedTime = 0;
            }
            sourceRectangle = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            destinationRectangle = new Rectangle((int)Position.X,(int)Position.Y,FrameWidth,FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive) return;
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White,0f,Vector2.Zero,SpriteEffects.None,zindex);
        }
    }
}