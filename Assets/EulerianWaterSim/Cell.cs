// Eulerian Water Sim : converted to Unity by http://unitycoder.com/blog
// Original Source: http://www.youtube.com/watch?v=ZXPdI0WIvw0&feature=youtu.be

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerianWaterSim
{
    [Flags]
    public enum Direction
	{
        None,
        Left,
        Right,
	}
	
    public enum TileType
    {
        Air = 0,
        Dirt = 1,
    }

    class Cell
    {
        public TileType Tile;

        public int Level;

        public Direction Direction;

        public bool NoCalc;

        public static int MaxLevel = 8;
        public static int RenderWidth = 1;
        public static int RenderHeight = 1;
        public static Cell None = new Cell(TileType.Air, 0);

        public Cell(TileType type, int level)
        {
            Tile = type;
            Level = level;
            NoCalc = false;
        }
    }
}
