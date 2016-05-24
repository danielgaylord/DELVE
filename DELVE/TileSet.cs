using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DELVE
{
    public class TileSet
    {
        private Texture2D Texture;
        private Dictionary<string, Tile> Tiles = new Dictionary<string, Tile>();
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        private int height;
        private int width;

        public TileSet(Texture2D Texture, int rows, int columns)
        {
            this.Texture = Texture;
            width = Texture.Width / columns;
            height = Texture.Height / rows;
        }

        public void AddAutoTile(string name, int row, int column, Tile tile)
        {
            AddMiniTiles(name, row, column, "DCTSB", tile);
        }

        public void AddWallTile(string name, int row, int column, Tile tile)
        {
            AddMiniTiles(name, row, column, "EW", tile);
        }

        public void AddTile(string name, int row, int column, Tile tile)
        {
            Rectangle recs = new Rectangle();
            recs = new Rectangle(column * width, row * height, width, height);
            tile.Rectangle = recs;
            Tiles.Add(name, tile);
        }

        public void AddMiniTiles(string name, int row, int column, string types, Tile tile)
        {
            int subRow;
            int subColumn;
            int subWidth = width / 2;
            int subHeight = height / 2;
            string pos = "NSWE";
            string suffix;
            Rectangle recs = new Rectangle();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < types.Length; k++)
                    {
                        tile = tile.Copy();
                        suffix = "" + pos[i] + pos[j + 2] + types[k];
                        subColumn = (column * 2) + adjustColumn(suffix);
                        subRow = (row * 2) + adjustRow(suffix);
                        recs = new Rectangle(subColumn * subWidth, subRow * subHeight, subWidth, subHeight);
                        tile.Rectangle = recs;
                        tile.Offset = new Vector2(j * subWidth, i * subHeight);
                        Tiles.Add(name + "_" + suffix, tile);
                    }
                }
            }
        }

        public int adjustColumn(string suffix)
        {
            int adjusted = 0;

            if (suffix[1] == 'E')
            {
                adjusted++;
            }
            if (suffix[2] == 'D' || suffix[2] == 'W')
            {
                adjusted += 2;
            }
            if (suffix[2] == 'C' || suffix[2] == 'S')
            {
                if (suffix[1] == 'E')
                {
                    adjusted += 2;
                }
            }
            if (suffix[2] == 'T' || suffix[2] == 'B')
            {
                if (suffix[1] == 'W')
                {
                    adjusted += 2;
                }
            }

            return adjusted;
        }

        public int adjustRow(string suffix)
        {
            int adjusted = 0;

            if (suffix[0] == 'S')
            {
                adjusted++;
            }
            if (suffix[2] == 'C' || suffix[2] == 'T')
            {
                adjusted += 2;

                if (suffix[0] == 'S')
                {
                    adjusted += 2;
                }
            }
            if (suffix[2] == 'S' || suffix[2] == 'B')
            {
                adjusted += 2;

                if (suffix[0] == 'N')
                {
                    adjusted += 2;
                }
            }

            return adjusted;
        }

        public void Draw(SpriteBatch spriteBatch, string[,] layout)
        {
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    if (!layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]) && layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NWT"].Origin(j, i), Tiles[layout[i, j] + "_NWT"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (!layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]) && layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NWS"].Origin(j, i), Tiles[layout[i, j] + "_NWS"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (!layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]) && !layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NWC"].Origin(j, i), Tiles[layout[i, j] + "_NWC"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]) && layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]) && !layout[i, j].Equals(layout[Math.Max(i - 1, 0), Math.Max(j - 1, 0)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NWD"].Origin(j, i), Tiles[layout[i, j] + "_NWD"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]) && layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]) && layout[i, j].Equals(layout[Math.Max(i - 1, 0), Math.Max(j - 1, 0)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NWB"].Origin(j, i), Tiles[layout[i, j] + "_NWB"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }

                    if (!layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]) && layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SWT"].Origin(j, i), Tiles[layout[i, j] + "_SWT"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (!layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]) && layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SWS"].Origin(j, i), Tiles[layout[i, j] + "_SWS"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (!layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]) && !layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SWC"].Origin(j, i), Tiles[layout[i, j] + "_SWC"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]) && layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]) && !layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), Math.Max(j - 1, 0)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SWD"].Origin(j, i), Tiles[layout[i, j] + "_SWD"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]) && layout[i, j].Equals(layout[i, Math.Max(j - 1, 0)]) && layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), Math.Max(j - 1, 0)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SWB"].Origin(j, i), Tiles[layout[i, j] + "_SWB"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }

                    if (!layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]) && layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NET"].Origin(j, i), Tiles[layout[i, j] + "_NET"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (!layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]) && layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NES"].Origin(j, i), Tiles[layout[i, j] + "_NES"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (!layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]) && !layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NEC"].Origin(j, i), Tiles[layout[i, j] + "_NEC"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]) && layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]) && !layout[i, j].Equals(layout[Math.Max(i - 1, 0), Math.Min(j + 1, layout.GetLength(1) - 1)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NED"].Origin(j, i), Tiles[layout[i, j] + "_NED"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (layout[i, j].Equals(layout[Math.Max(i - 1, 0), j]) && layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]) && layout[i, j].Equals(layout[Math.Max(i - 1, 0), Math.Min(j + 1, layout.GetLength(1) - 1)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_NEB"].Origin(j, i), Tiles[layout[i, j] + "_NEB"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }

                    if (!layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]) && layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SET"].Origin(j, i), Tiles[layout[i, j] + "_SET"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (!layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]) && layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SES"].Origin(j, i), Tiles[layout[i, j] + "_SES"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (!layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]) && !layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SEC"].Origin(j, i), Tiles[layout[i, j] + "_SEC"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]) && layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]) && !layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), Math.Min(j + 1, layout.GetLength(1) - 1)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SED"].Origin(j, i), Tiles[layout[i, j] + "_SED"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                    if (layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), j]) && layout[i, j].Equals(layout[i, Math.Min(j + 1, layout.GetLength(1) - 1)]) && layout[i, j].Equals(layout[Math.Min(i + 1, layout.GetLength(0) - 1), Math.Min(j + 1, layout.GetLength(1) - 1)]))
                    {
                        spriteBatch.Draw(Texture, Tiles[layout[i, j] + "_SEB"].Origin(j, i), Tiles[layout[i, j] + "_SEB"].Rectangle, Color, Rotation, Origin, Scale, SpriteEffect, 0f);
                    }
                }
            }
        }
    }
}
