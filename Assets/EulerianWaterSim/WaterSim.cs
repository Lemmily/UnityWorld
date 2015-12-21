// Eulerian Water Sim : converted to Unity by http://unitycoder.com/blog
// Original Source: http://www.youtube.com/watch?v=ZXPdI0WIvw0&feature=youtu.be

using UnityEngine;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerianWaterSim
{
	
    class WaterSim : MonoBehaviour
    {
		
		private Color[] pixels;
		private Color[] clearPixels;
        public Cell[,] Grid;
        public int Height;
        public int Width;
        public static int MaxLevelChange = 15;
	    private Ray ray;
		private RaycastHit hit;
        //private static Random rand = new Random();
		private Texture2D tex;
		
		void Start()
		{
			tex = new Texture2D(Width, Height);
			GetComponent<Renderer>().material.mainTexture = tex;
			GetComponent<Renderer>().material.mainTexture.filterMode = FilterMode.Point;
			WaterSim2();
			pixels=tex.GetPixels();
			clearPixels = pixels;
		}
		
		// mainloop
		void Update()
		{
			xUpdate();
			DebugRender();
			
			if (Input.GetKeyDown("1"))
			{
				GetComponent<Renderer>().material.mainTexture.filterMode = FilterMode.Point;
			}
			
			if (Input.GetKeyDown("2"))
			{
				GetComponent<Renderer>().material.mainTexture.filterMode = FilterMode.Bilinear;
			}
			
			if (Input.GetKeyDown("3"))
			{
				GetComponent<Renderer>().material.mainTexture.filterMode = FilterMode.Trilinear;
			}

			if (Input.GetMouseButton(0))
			{
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, 100))
				{
					Debug.DrawLine(ray.origin, hit.point,Color.yellow,10);
					float hx = Width-(hit.point.x*Width/10);
					float hy = Height-(hit.point.z*Height/10);
					//Debug.Log(hx+","+hy);
				   Grid[(int)hx,(int)hy] =  new Cell(TileType.Dirt, (byte)Cell.MaxLevel);
				}
			}
			
			if (Input.GetMouseButton(1))
			{
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, 100))
				{
					Debug.DrawLine(ray.origin, hit.point,Color.yellow,10);
					float hx = Width-(hit.point.x*Width/10);
					float hy = Height-(hit.point.z*Height/10);
					//Debug.Log(hx+","+hy);
				   //Grid[(int)hx,(int)hy] =  new Cell(TileType.Air, (byte)Cell.MaxLevel);
//					Grid[(int)hx+1,(int)hy].Level = (byte)Cell.MaxLevel;
//					Grid[(int)hx,(int)hy-1].Level = (byte)Cell.MaxLevel;
					Grid[(int)hx,(int)hy].Level = (byte)Cell.MaxLevel;
//					Grid[(int)hx,(int)hy+1].Level = (byte)Cell.MaxLevel;
//					Grid[(int)hx-1,(int)hy].Level = (byte)Cell.MaxLevel;
				   //Grid[(int)hx,(int)hy] =  new Cell(TileType.Air, (byte)Cell.MaxLevel);
				}
			}
			
		}

        void WaterSim2()
        {
			/*
           Grid = new Cell[3, 3]
            {
                {
                    new Cell(TileType.Air, 0), new Cell(TileType.Air, 0), new Cell(TileType.Air, 0), 
                },
                {
                    new Cell(TileType.Air, 7), new Cell(TileType.Air, 0), new Cell(TileType.Air, 0), 
                },
                {
                    new Cell(TileType.Air, 0), new Cell(TileType.Air, 0), new Cell(TileType.Air, 0), 
                },

            };*/
            //Width = 3;
            //Height = 3;

            
            //Width = 64;
            //Height = 64;

            Grid = new Cell[Width, Height];
            currentCellY = Height - 1;

            //Random r = new Random();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                   // if (y < 3)
                   // {
                   //     //Grid[x, y] = new Cell(TileType.Air, r.Next(0, Cell.MaxLevel));
                    //        Grid[x, y] = new Cell(TileType.Air, 0);
                        //Grid[x, y] = new Cell(TileType.Air, Random.Range(0, Cell.MaxLevel));
                  //  }
                   // else
                  //  {
                        //if (r.Next(100) > 5)
                            Grid[x, y] = new Cell(TileType.Air, 0);
                        //else
                            //Grid[x, y] = new Cell(TileType.Dirt, 0);
                   // }
                }
            }
        }

        int lastCellX = 0, lastCellY = 0;
        int currentCellX = 0, currentCellY = 0;

        void UpdateNextCell()
        {
            UpdateFluidCell2(currentCellX, currentCellY);

            // increment to next cell
            currentCellX++;

            if (currentCellX >= Width)
            {
                currentCellY--;
                currentCellX = 0;
            }

            if (currentCellY < 0)
                currentCellY = Height - 1;

            lastCellX = currentCellX;
            lastCellY = currentCellY;
        }

        void xUpdate()
        {
            // loop from bottom to top
            for (int y = Height - 1; y >= 0; y--)
            {
                // loop from right to left
                for (int x = 0; x < Width; x++)
                {
                    UpdateFluidCell2(x, y);
                }
            }
            // loop from bottom to top
            for (int y = Height - 1; y >= 0; y--)
            {
                // loop from right to left
                for (int x = 0; x < Width; x++)
                {
                    Grid[x, y].NoCalc = false;
                }
            }
        }

        //public void DebugRender(SpriteBatch sb, SpriteFont font, Texture2D white)
        void DebugRender()
        {
//            sb.Begin();
			
			// clear array?
			pixels = clearPixels;
//			tex.SetPixels(clearPixels);
//			tex.Apply(false);			

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
					pixels[y*Width + x] = Color.black;
					
                    Cell cell = Grid[x, y];

                    if (cell.Tile == TileType.Dirt)
                    {
                        //sb.Draw(white, new Rectangle(x * Cell.RenderWidth, y * Cell.RenderHeight, Cell.RenderWidth, Cell.RenderHeight), Color.Green);
						//tex.SetPixel(x * Cell.RenderWidth, y * Cell.RenderHeight,Color.green);
						//tex.SetPixel(x, y,Color.green);
						pixels[y*Width + x] = Color.green;
                    }

                    if (cell.Tile == TileType.Air && cell.Level > 0)
                    {
                        var c = ((float)cell.Level / (float)Cell.MaxLevel);
                        //sb.Draw(white, new Rectangle(x * Cell.RenderWidth, y * Cell.RenderHeight, Cell.RenderWidth, Cell.RenderHeight), Color.Blue * c);
						//tex.SetPixel(x * Cell.RenderWidth, y * Cell.RenderHeight,Color.blue);
//						tex.SetPixel(x, y,Color.blue);
						pixels[y*Width + x] = Color.blue;

                    }

                    if (cell.NoCalc)
                    {
                        //sb.Draw(white, new Rectangle(x * Cell.RenderWidth, y * Cell.RenderHeight, Cell.RenderWidth, Cell.RenderHeight), Color.Orange * 0.6f);
//						tex.SetPixel(x, y,Color.red);
						pixels[y*Width + x] = Color.red;
                    }

                    //var size = font.MeasureString(cell.Level.ToString());
                    //sb.DrawString(font, cell.Level.ToString(), new Vector2(x * Cell.RenderWidth + (Cell.RenderWidth * 0.5f), y * Cell.RenderHeight + (Cell.RenderHeight * 0.5f)), Color.Black, 0, size * 0.5f,
                    //    1, SpriteEffects.None, 0);
                }
            }

            //sb.Draw(white, new Rectangle(lastCellX * Cell.RenderWidth, lastCellY * Cell.RenderHeight, Cell.RenderWidth, Cell.RenderHeight), Color.Yellow * 0.5f);
			//tex.SetPixel(lastCellX, lastCellY,Color.gray);
			tex.SetPixels(pixels);
			tex.Apply(false);
			//Debug.Log("asdasdasd");
            //sb.End();
        }

        //[Flags]
        enum FlowDirection
        {
            None,
            Right,
            Left,
            Both = Left | Right,
        }

        enum CellMatrix
        {
            None = 0,
            Bottom = 1,
            Left = 2,
            Right = 4,
            RightLeft = Left | Right,
            All = Bottom | Left | Right,
        }

        void UpdateFluidCell2(int x, int y)
        {
            // get surrounding 3 cells and input into action matrix
            Cell cell, bottomCell = Cell.None, leftCell = Cell.None, rightCell = Cell.None;
            CellMatrix matrix = CellMatrix.None;

            cell = Grid[x, y];

            // if tile type is not == water return or if the no calculate flag is set
            if (cell.Tile != TileType.Air) return;
            // if there is no fluid in this cell return
            if (cell.Level == 0) return;

            // get cell beneath this one
            if (y + 1 < Height)
            {
                bottomCell = Grid[x, y + 1];
                // for this cell to be considered it must...
                //  be of type Air, not be full of fluid, and have less fluid then the cell we are flowing
                if (bottomCell.Tile == TileType.Air && bottomCell.Level < Cell.MaxLevel)
                {
                    matrix |= CellMatrix.Bottom;
                }
            }

            // get cell to the left of this one
            if (x - 1 >= 0)
            {
                leftCell = Grid[x - 1, y];
                // for this cell to be considered it must...
                //  be of type Air, not be full of fluid, and have less fluid then the cell we are flowing
                if (leftCell.Tile == TileType.Air && leftCell.Level < Cell.MaxLevel && leftCell.Level < cell.Level)
                {
                    matrix |= CellMatrix.Left;
                }
            }

            // get cell to the right of this one
            if (x + 1 < Width)
            {
                rightCell = Grid[x + 1, y];
                // for this cell to be considered it must...
                //  be of type Air, not be full of fluid, and have less fluid then the cell we are flowing
                if (rightCell.Tile == TileType.Air && rightCell.Level < Cell.MaxLevel && rightCell.Level < cell.Level)
                {
                    matrix |= CellMatrix.Right;
                }
            }

            // we now know what cells we can check
            switch (matrix)
            {
                case CellMatrix.Bottom:
                    cell.Level = FlowBottom(ref cell, ref bottomCell);
                    break;
                    
                case CellMatrix.Bottom | CellMatrix.Left:
                    var leftOverFluid = FlowBottom(ref cell, ref bottomCell);
                    if (leftOverFluid > 0)
                    {
                        FlowLeft(ref cell, ref leftCell);
                    }
                    break;

                case CellMatrix.Bottom | CellMatrix.Right:
                    leftOverFluid = FlowBottom(ref cell, ref bottomCell);
                    if (leftOverFluid > 0)
                    {
                        FlowRight(ref cell, ref rightCell);
                    }
                    break;

                case CellMatrix.Bottom | CellMatrix.Right | CellMatrix.Left:
                    leftOverFluid = FlowBottom(ref cell, ref bottomCell);
                    if (leftOverFluid > 0)
                    {
                        FlowLeftRight(ref cell, ref leftCell, ref rightCell);
                    }
                    break;

                case CellMatrix.Left:
                    FlowLeft(ref cell, ref leftCell);
                    break;

                case CellMatrix.Right:
                    FlowRight(ref cell, ref rightCell);
                    break;

                case CellMatrix.RightLeft:
                    FlowLeftRight(ref cell, ref leftCell, ref rightCell);
                    break;
            }
        }

        private void FlowRight(ref Cell cell, ref Cell rightCell)
        {
            int amountToSpread = (rightCell.Level + cell.Level) / 2;
            int remainder = (rightCell.Level + cell.Level) % 2;

            rightCell.Level = amountToSpread + remainder;
            rightCell.NoCalc = true;
            rightCell.Direction = Direction.Right;
            cell.Level = amountToSpread;
            cell.NoCalc = true;
        }

        private void FlowLeft(ref Cell cell, ref Cell leftCell)
        {
            int amountToSpread = (leftCell.Level + cell.Level) / 2;
            int remainder = (leftCell.Level + cell.Level) % 2;

            leftCell.Level = amountToSpread + remainder;
            leftCell.NoCalc = true;
            leftCell.Direction = Direction.Left;
            cell.Level = amountToSpread;
            cell.NoCalc = true;
        }

        private void FlowLeftRight(ref Cell cell, ref Cell leftCell, ref Cell rightCell)
        {
            int amountToSpread = (leftCell.Level + rightCell.Level + cell.Level) / 3;
            int remainder = (leftCell.Level + rightCell.Level + cell.Level) % 3;
            // if we have a remainder...
            if (remainder > 0)
            {
                // 
                if (cell.Direction == Direction.Left)
                {
                    leftCell.Level = amountToSpread + remainder;
                    leftCell.Direction = Direction.Left;
                    rightCell.Level = amountToSpread;
                }
                else
                {
                    leftCell.Level = amountToSpread;
                    rightCell.Level = amountToSpread + remainder;
                    rightCell.Direction = Direction.Right;
                }

            }
            else
            {
                // otherwise it's an even split
                leftCell.Level = amountToSpread;
                leftCell.Direction = Direction.None;
                rightCell.Level = amountToSpread;
                rightCell.Direction = Direction.None;
            }

            cell.Level = amountToSpread;
            cell.NoCalc = true;
            cell.Direction = Direction.None;
            leftCell.NoCalc = true;
            rightCell.NoCalc = true;
        }

        private int FlowBottom(ref Cell cell, ref Cell bottomCell)
        {
            // check to see how much fluid can fall down
            var spaceAvailable = Cell.MaxLevel - bottomCell.Level;
            //var amountToMove = (int)MathHelper.Min(spaceAvailable, cell.Level);
            var amountToMove = (int)Mathf.Min(spaceAvailable, cell.Level);

            // move all fluid that can be moved
            bottomCell.Level += amountToMove;
            bottomCell.NoCalc = true;
            bottomCell.Direction = Direction.None;
            cell.Level -= amountToMove;
            cell.NoCalc = true;

            return cell.Level;
        }

        // simple debug method to make sure we aren't gaining or losing total amount of fluid
        public int CountFluid()
        {
            int count = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    count += Grid[x, y].Level;
                }
            }

            return count;
        }
    }
}
