using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace DELVE
{
    class Player
    {
        private AnimationSet animations;

        public Player(ContentManager content)
        {
            animations = new AnimationSet(content.Load<Texture2D>("Sprites//Player"), 8, 12);
            animations.Position = new Vector2(100, 100);

            Animation animation = new Animation();
            animations.AddAnimation("Down", 0, 0, 3, animation.Copy());
            animations.AddAnimation("Down_Idle", 0, 0, 1, animation.Copy());
            animations.AddAnimation("Left", 1, 0, 3, animation.Copy());
            animations.AddAnimation("Left_Idle", 1, 0, 1, animation.Copy());
            animations.AddAnimation("Right", 2, 0, 3, animation.Copy());
            animations.AddAnimation("Right_Idle", 2, 0, 1, animation.Copy());
            animations.AddAnimation("Up", 3, 0, 3, animation.Copy());
            animations.AddAnimation("Up_Idle", 3, 0, 1, animation.Copy());
            animations.Animation = "Right";
            animations.FramesPerSecond = 15;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                animations.Position.Y += 2;

                if (animations.Animation != "Down")
                {
                    animations.Animation = "Down";
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                animations.Position.X -= 2;

                if (animations.Animation != "Left")
                {
                    animations.Animation = "Left";
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                animations.Position.X += 2;

                if (animations.Animation != "Right")
                {
                    animations.Animation = "Right";
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                animations.Position.Y -= 2;

                if (animations.Animation != "Up")
                {
                    animations.Animation = "Up";
                }
            }
            else if ((keyboardState.IsKeyUp(Keys.Down)) && (keyboardState.IsKeyUp(Keys.Left)) && (keyboardState.IsKeyUp(Keys.Right)) && (keyboardState.IsKeyUp(Keys.Up)))
            {
                if (!animations.Animation.Contains("_Idle"))
                {
                    animations.Animation += "_Idle";
                }
            }

            animations.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animations.Draw(spriteBatch);
        }
    }
}
