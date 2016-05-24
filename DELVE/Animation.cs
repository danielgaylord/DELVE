using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DELVE
{
    public class Animation
    {
        public Rectangle[] Rectangles;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        public bool IsLooping = true;
        public int Frames;

        public Animation Copy()
        {
            Animation ac = new Animation();
            ac.Rectangles = Rectangles;
            ac.Color = Color;
            ac.Origin = Origin;
            ac.Rotation = Rotation;
            ac.Scale = Scale;
            ac.SpriteEffect = SpriteEffect;
            ac.IsLooping = IsLooping;
            ac.Frames = Frames;
            return ac;
        }
    }
}
