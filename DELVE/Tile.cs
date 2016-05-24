using Microsoft.Xna.Framework;

namespace DELVE
{
    public class Tile
    {
        public Rectangle Rectangle;
        public Vector2 Offset;

        public Tile Copy()
        {
            Tile tc = new Tile();
            tc.Rectangle = Rectangle;
            tc.Offset = Offset;
            return tc;
        }

        public Vector2 Origin(int x, int y)
        {
            return new Vector2(x * Rectangle.Width * 2 + Offset.X, y * Rectangle.Height * 2 + Offset.Y);
        }
    }
}
