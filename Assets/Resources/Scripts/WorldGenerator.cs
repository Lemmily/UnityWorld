using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MapInfo))]
public class WorldGenerator : MonoBehaviour {


    //public VisualTiles tiles;
    public MapInfo mapInfo;
    public float seed;

    public float[,] workingMap;
    public int[,] setMap;

    public float[,] maskMap;
    // Use this for initialization
    void Start () {
        mapInfo = gameObject.GetComponent<MapInfo>();
        workingMap = new float[mapInfo.width, mapInfo.height];
        maskMap = new float[mapInfo.width, mapInfo.height];
        setMap = new int[mapInfo.width, mapInfo.height];
        firstPass();
        generateMapMask();
        normalise(maskMap);
        applyMask();
        normalise(workingMap);
        commit();

    }

    public void applyMask() {
        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                workingMap[x, y] *= maskMap[x, y];
            }
        }
        //clean mask now.
        maskMap = new float[mapInfo.width, mapInfo.height];
    }

    public void commit() {
        commit("terrain");
    }

    public void commit(string layer) {
        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                setMap[x, y] = (int)workingMap[x, y];
            }
        }
        mapInfo.setLayer(layer, setMap);
    }

    private float[,] normalise(float[,] map) {
        float highest = 0;
        float lowest = 999999999;
        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                if (highest < map[x,y]) {
                    highest = map[x, y];
                } else if (lowest > map[x, y ]) {
                    lowest = map[x, y];
                }
            }
        }

        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                map[x, y] = ((map[x, y] - lowest) / (highest - lowest)) * 4;
            }
        }

        return map;
    }

    private void firstPass() {
        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                float xCoord = (seed + (float)x / 8f);
                float yCoord = (seed + (float)y / 8f);
                workingMap[x,y] += Mathf.PerlinNoise(xCoord, yCoord);
            }
        }
    }
    private void solidColour() {
        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                float xCoord = (seed + (float)x / 8f);
                float yCoord = (seed + (float)y / 8f);
                workingMap[x, y] += 2;
            }
        }
    }

    private void secondPass() {
        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                float xCoord = ((1 + seed) * 6.5f  + (float)x / (float)mapInfo.width);
                float yCoord = ((1 + seed) * 6.5f+ (float)y / (float)mapInfo.height);
                workingMap[x, y] += Mathf.PerlinNoise(xCoord, yCoord);
            }
        }
    }
    // Update is called once per frame
    void Update () {
	
	}




    public void thing() {

        float w, h;
        w = mapInfo.width;
        h = mapInfo.height;

        float stepSizeW = 1f / w;
        float stepSizeH = 1f / h;
        float ellipseW = w;
        float ellipseH = h - 10;

        float ellipseX = w * 0.5f;
        float ellipseY = h * 0.5f;

        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w; x++) {
                //Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                // sample = Mathf.PerlinNoise((x * 2.5f) + seed, (y * 2.5f) + seed);
                //sample = sample * 0.5f + 0.5f;
                float sample = 1f;
                float distance_x = Mathf.Abs(x - ellipseX);
                float distance_y = Mathf.Abs(y - ellipseY);
                float distance = Mathf.Sqrt((distance_x * distance_x) / (ellipseW / 2 * ellipseW / 2) +
                    (distance_y * distance_y) / (ellipseH / 2 * ellipseH / 2)); // out of 1

                float max_width = Mathf.Sqrt((x - ellipseX) * (x - ellipseX) + (y - ellipseY) * (y - ellipseY));
                distance = max_width * distance; //gets the actual distance. 
                float delta = distance / max_width;
                float gradient = delta * delta;

                sample *= Mathf.Max(0.0f, 1f - gradient);
                maskMap[x,y] += sample;
            }
        }
    }


    public void generateMapMask() {
        UnityEngine.Random.seed = (int)seed;
        for (int i = 0; i < 100; i++) {
            int x = (int) (Mathf.PerlinNoise(seed + UnityEngine.Random.value, seed + UnityEngine.Random.value) * mapInfo.width);
            int y = (int) (Mathf.PerlinNoise(seed + UnityEngine.Random.value, seed + UnityEngine.Random.value) * mapInfo.height);

            int w = (int)(UnityEngine.Random.value * 40f);
            int h = (int)(UnityEngine.Random.value * 40f);

            addHill(w, h, x, y);
        }
    }

    public void addHill(float wC, float hC, int cenX, int cenY) {
        int minX = (int)(cenX - (wC / 2));
        int minY = (int)(cenY - (hC / 2));
        for (int y = 0; y < hC; y++) {
            for (int x = 0; x < wC; x++) {
                float value = ((float)Mathf.Sin(((float)x / (float)wC) * Mathf.PI)) * ((float)Mathf.Sin(((float)y / (float)hC) * Mathf.PI));
                if (0 > (minX + x) || (minX + x) >= mapInfo.width || 0 > (minY + y) || (minY + y) >= mapInfo.height) {
                    continue;
                }
                maskMap[minX + x, minY + y] += value;
            }
        }
    }
}
