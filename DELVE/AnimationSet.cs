using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DELVE
{
    public class AnimationSet
    {
        private Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        private Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();
        private int FrameIndex = 0;
        private string animation;
        private Vector2 Origin;
        private int height;
        private int width;
        private float timeElapsed;
        private float timeToUpdate = 0.05f;
        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }
        public string Animation
        {
            get { return animation; }
            set
            {
                animation = value;
                FrameIndex = 0;
            }
        }

        public AnimationSet(Texture2D Texture, int rows, int columns)
        {
            this.Texture = Texture;
            width = Texture.Width / columns;
            height = Texture.Height / rows;
            Origin = new Vector2(width / 2, height / 2);
        }

        public void AddAnimation(string name, int row, int column, int frames, Animation animation)
        {
            Rectangle[] recs = new Rectangle[frames];
            for (int i = column; i < frames; i++)
            {
                recs[i] = new Rectangle(i * width, row * height, width, height);
            }
            animation.Frames = frames;
            animation.Rectangles = recs;
            Animations.Add(name, animation);
        }

        public void SetFrame(int frame)
        {
            if (frame < Animations[Animation].Frames)
            {
                FrameIndex = frame;
            }
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Animations[Animation].Frames - 1)
                {
                    FrameIndex++;
                }
                else if (Animations[Animation].IsLooping)
                {
                    FrameIndex = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Animations[Animation].Rectangles[FrameIndex], Animations[Animation].Color, Animations[Animation].Rotation, Origin, Animations[Animation].Scale, Animations[Animation].SpriteEffect, 0f);
        }
    }
}
