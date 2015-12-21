using UnityEngine;
using System.Collections.Generic;
using System;

public class Cellular : MonoBehaviour
{

    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    public float timeScale = 1f; 
    public float updateSpeed = .5f; //in secs
    private float timeSinceUpdate;

    [Range(0, 100)]
    public int randomFillPercent;
    int[,] currentMap;
    int[,] map;
    Square[,] squares;
    public bool updating;

    void Start() {
        GenerateMap();
    }

    void Update() {
        //if (updating < 10) {
        //    MeshGenerator meshGen = GetComponent<MeshGenerator>();
        //    meshGen.GenerateMesh(map, 1);
        //    updating++;
        //}
        timeSinceUpdate += Time.deltaTime;

        if (timeSinceUpdate > updateSpeed * timeScale) {
            UpdateCellsList();
            timeSinceUpdate = 0;
            GetComponent<Script>().UpdateMesh();
        }
    }

    public void GenerateMap() {
        updating = true;
        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        map = new int[width, height];
        currentMap = new int[width, height];
        squares = new Square[width, height];
        RandomFillMap();

        //for (int i = 0; i < 5; i++) {
        //    SmoothMap();
        //    //meshGen.GenerateMesh(map, 1);
        //}

        updating = false;
    }


    void RandomFillMap() {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                //if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
                //    map[x, y] = 1;
                //}
                //else {
                squares[x, y] = new Square(x, y, 3);
                int ranNum = pseudoRandom.Next(0, 100) ;
                if( ranNum < randomFillPercent - randomFillPercent / 2) {
                    currentMap[x, y] = 2;
                } else if ( ranNum < randomFillPercent) {
                    currentMap[x, y] = 1;
                }
                else {
                    currentMap[x, y] = 0;
                }
                //}
            }
        }
    }

    internal void ChangeTileBy(Vector2 point, int v) {
        currentMap[(int)point.x, (int)point.y] = changeTileBy(currentMap[(int)point.x, (int)point.y], v);
    }

    internal void SetTile(Vector2 point, int v) {
        currentMap[(int)point.x, (int)point.y] = v;
    }

    void SmoothMap() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }
        }
    }
    
    private void UpdateCells() {
        map = (int[,])currentMap.Clone();

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);
                //        Debug.Log(neighbourWallTiles);
                //        if (neighbourWallTiles < 2) {
                //            map[x, y] = 0;
                //        } else if (neighbourWallTiles == 3) { 
                //            if (map[x,y] != 1) {
                //                map[x, y] = 1;
                //            }
                //        } else if (neighbourWallTiles > 3) {
                //            map[x, y] = 0;
                //        }
                //else if (neighbourWallTiles % 2 != 1) {
                //    map[x, y] = 1;
                //}
                map[x, y] = RuleSetOne(neighbourWallTiles) == -1 ? currentMap[x,y] : RuleSetOne(neighbourWallTiles);

            }
        }
        currentMap = (int[,])map.Clone();
    }

    public void UpdateCellsList() {
        map = (int[,])currentMap.Clone();

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                squares[x, y].setNeighbours(GetNeighbours(x, y));
                int neighbourWallTiles = GetSurroundingWallCount(x, y);
                map[x, y] = changeTileBy(currentMap[x, y], RuleSetFour(squares[x, y])); // == -1 ? currentMap[x, y] : RuleSetOne(neighbourWallTiles));

            }
        }
        currentMap = (int[,])map.Clone();
    }

    private int changeTileBy(int current, int valueChange) {
        int numValues = 3; //TODO: LOOKUP for this. like, how many varients?
        int value = current + valueChange;
        if (value < 0)
            value = 0;
        else if (value >= numValues)
            value = numValues - 1;
        return value;
    }

    private int changeTileTo(int current, int valueChange) {
        return 0;
    }

    private int RuleSetOne(int neighbours) {
        if (neighbours == 3) { 
            return 1;
        }
        else if (neighbours < 2 || neighbours > 3) {
            return 0;
        }
        return -1;
    }
    private int RuleSetTwo(int neighbours) {
        if (neighbours == 1 || neighbours == 3 ) {
            return 1;
        } else if (neighbours < 2 || neighbours > 4) {
            return 0;
        }

        return -1;
    }
    private int RuleSetThree(int neighbours) {
        if (neighbours == 3 || neighbours == 6) {
            return 1;
        }
        else if (neighbours < 2 || neighbours > 3) {
            return 0;
        }
        return -1; //here -1 means no change. old
    }

    private int RuleSetFour(Square grid) {
        int tileType = currentMap[grid.x, grid.y];       
        int numOfSame = grid.GetNeighboursOfType(tileType);

        int dominantType = grid.getDominantType();
        int numOfDom = grid.GetNeighboursOfType(dominantType);

        if (tileType != dominantType) {
            if (tileType < dominantType) {
                if (numOfDom > 3 || numOfSame >= 5) {
                    return 1;
                }
                else if (numOfDom < 2 || numOfSame <= 1) {
                    return -1;
                }
                else {
                    return 0; // here 0 means no change . new
                }
            } else {
                if (numOfDom > 5) {
                    return 0;
                }
                else if (numOfDom < 2) {
                    return -1;
                }
                else {
                    return 1; // here 0 means no change . new
                }
            }
        } else {

            if (numOfSame == 3) {
                return 1;
            }
            else if (numOfSame < 2) {
                return -1;
            }
            else {
                return 0; // here 0 means no change . new
            }
        }
    }
    int GetOrthogonalWallCount(int gridX, int gridY) {
        int wallCount = 0;
        for (int x = gridX - 1; x <= gridX + 1; x++) {
            for (int y = gridY - 1; y <= gridY + 1; y++) {
                int neighbourX = x;
                int neighbourY = y;
                if (neighbourX < 0) {
                    neighbourX = width - 1;
                } else if  (neighbourX >= width) {
                    neighbourX = 0;
                }
                if (neighbourY < 0) {
                    neighbourY = width - 1;
                } else if (neighbourY >= height) {
                    neighbourY = 0;
                }
                if (gridX == neighbourX && gridY == neighbourY) {
                    continue;
                } else if (neighbourX == gridX || neighbourY == gridY) {
                    wallCount += currentMap[neighbourX, neighbourY];
                }
            }
        }

        return wallCount;
    }

    int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                int x = neighbourX;
                int y = neighbourY;
                if (x < 0) {
                    x = width - 1;
                }
                else if (x >= width) {
                    x = 0;
                }
                if (y < 0) {
                    y = height - 1;
                }
                else if (y >= height) {
                    y = 0;
                }

                if (x != gridX || y != gridY) {
                        wallCount += currentMap[x, y];
                }
                //else {
                //    wallCount++;
                //}
            }
        }

        return wallCount;
    }

    public int[] GetNeighbours(int gridX, int gridY) {
        int[] neighbours = new int[8];
        int i = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                int x = neighbourX;
                int y = neighbourY;
                if (x < 0) {
                    x = width - 1;
                }
                else if (x >= width) {
                    x = 0;
                }
                if (y < 0) {
                    y = height - 1;
                }
                else if (y >= height) {
                    y = 0;
                }
                neighbours[i] = findTile(x, y);
            }
        }

        return neighbours;
    }


    internal int findTile() {
        return 1;
    }

    internal int findTile(int x, int y) {
        return currentMap[x, y];
    }


    //void OnDrawGizmos() {
    //    if (map != null) {
    //        for (int x = 0; x < width; x++) {
    //            for (int y = 0; y < height; y++) {
    //                Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
    //                Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
    //                Gizmos.DrawCube(pos, Vector3.one);
    //            }
    //        }
    //    }
    //}

    class Square
    {
        Dictionary<int, int> neighbs = new Dictionary<int, int>();
        int[] neighbours;
        public int x;
        public int y;
        
        public Square(int x, int y, int variants) {
            this.x = x;
            this.y = y;
            neighbours = new int[8];
            for (int i = 0; i < variants; i++) {
                neighbs.Add(i, 0);
            }
        }

        public void setNeighbour(int neighbour, int value) {
            neighbours[neighbour] = value;
            
        } 

        public void setNeighbours(int[] new_neighbours) {
            for (int i = 0; i < new_neighbours.Length; i++) {
                setNeighbour(i, new_neighbours[i]);
            }
            tallyNeighbours();
        }

        private void tallyNeighbours() {
            List<int> keys = new List<int>(neighbs.Keys);
            foreach (int item in keys) {
                neighbs[item] = 0;
            }
            for (int i = 0; i < neighbours.Length; i++) {
                if (!neighbs.ContainsKey(neighbours[i])) {
                    Debug.Log("This key is nto present: " + neighbours[i]);
                }
                else {
                    neighbs[neighbours[i]] += 1;
                }
            }
        }

        public int getNeighbour(int neighbour) {
            return neighbours[neighbour];
        }

        internal int GetNeighboursOfType(int tileType) {
            return neighbs[tileType];
        }

        internal int getDominantType() {
            int highestVal = 0;
            int tileType = -1;

            foreach (KeyValuePair<int, int> pair in neighbs) {
                if (pair.Value > highestVal) tileType = pair.Key;
            }

            return tileType;
        }
    }

}