using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DELVE
{
    class Room
    {
        private TileSet tileSet;
        private int[,] room = {{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                               { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
                               { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
                               { 5, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 5 },
                               { 5, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 5 },
                               { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
                               { 5, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 5 },
                               { 5, 0, 0, 3, 3, 3, 0, 0, 0, 2, 0, 5 },
                               { 5, 0, 3, 3, 3, 3, 0, 0, 2, 2, 0, 5 },
                               { 5, 0, 0, 3, 3, 0, 0, 0, 2, 2, 0, 5 },
                               { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
                               { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }};
        private Dictionary<int, string> tileType = new Dictionary<int, string>();

        public Room(ContentManager content)
        {
            tileSet = new TileSet(content.Load<Texture2D>("Tiles//Stone"), 11, 6);

            Tile tile = new Tile();
            tileSet.AddAutoTile("Normal", 0, 0, tile.Copy());
            tileType.Add(0, "Normal");
            tileSet.AddAutoTile("Dark", 0, 2, tile.Copy());
            tileType.Add(1, "Dark");
            tileSet.AddAutoTile("Hole", 0, 4, tile.Copy());
            tileType.Add(2, "Hole");
            tileSet.AddAutoTile("Water", 3, 0, tile.Copy());
            tileType.Add(3, "Water");
            tileSet.AddAutoTile("Ledge", 6, 0, tile.Copy());
            tileType.Add(4, "Ledge");
            tileSet.AddAutoTile("Ceiling", 6, 2, tile.Copy());
            tileType.Add(5, "Ceiling");

            tileSet.AddWallTile("Side", 9, 0, tile.Copy());
            tileType.Add(6, "Side");
            tileSet.AddWallTile("SideShadow", 10, 0, tile.Copy());
            tileSet.AddWallTile("Wall", 9, 2, tile.Copy());
            tileType.Add(7, "Wall");
            tileSet.AddWallTile("WallShadow", 10, 2, tile.Copy());
            tileSet.AddWallTile("Stairs", 7, 4, tile.Copy());
            tileSet.AddWallTile("Waterfall", 8, 4, tile.Copy());

            tileSet.AddTile("Floor1", 6, 4, tile.Copy());
            tileSet.AddTile("Floor2", 6, 5, tile.Copy());
        }

        public string[,] convertRoom()
        {
            string[,] layout = new string[room.GetLength(0), room.GetLength(1)];

            for (int i = 0; i < room.GetLength(0); i++)
            {
                for (int j = 0; j < room.GetLength(1); j++)
                {
                    layout[i, j] = tileType[room[i, j]];
                }
            }

            return layout;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tileSet.Draw(spriteBatch, convertRoom());
        }
    }
}
