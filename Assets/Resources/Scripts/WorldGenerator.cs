using UnityEngine;
using System.Collections;
using System;

public class WorldGenerator : MonoBehaviour {


    //public VisualTiles tiles;
    public MapInfo mapInfo;
    public float seed;

    public float[,] workingMap;
    public int[,] setMap;
    // Use this for initialization
    void Start () {
        mapInfo = gameObject.GetComponent<MapInfo>();
        workingMap = new float[mapInfo.width, mapInfo.height];
        setMap = new int[mapInfo.width, mapInfo.height];
        firstPass();
        //secondPass();
        //seed += seed;
        //secondPass();
        commit();

	}

    private void commit() {
        normalise(workingMap);
        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                setMap[x, y] = (int)workingMap[x, y];
            }
        }
        mapInfo.SetMap(setMap);
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
                map[x, y] = ((map[x, y] - lowest) / (highest - lowest)) * 2;
            }
        }

        return map;
    }

    private void firstPass() {
        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                float xCoord = (seed + (float)x / 8f);
                float yCoord = (seed + (float)y / 8f);
                workingMap[x,y] = Mathf.PerlinNoise(xCoord, yCoord);
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

        //NoiseMethod method = Noise.methods[(int)type][dimensions - 1];
        //float stepSize = 1f / resolution;

        //float w, h = resolution;
        //w = resolution;

        //float ellipseW = w;
        //float ellipseH = h - 10;

        //float ellipseX = w * 0.5f;
        //float ellipseY = h * 0.5f;

        //for (int y = 0; y < resolution; y++) {
        //    Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
        //    Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
        //    for (int x = 0; x < resolution; x++) {
        //        Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
        //        float sample = Noise.Sum(method, point, frequency, octaves, lacunarity, persistence).value;
        //        sample = sample * 0.5f + 0.5f;

        //        float distance_x = Mathf.Abs(x - ellipseX);
        //        float distance_y = Mathf.Abs(y - ellipseY);
        //        float distance = Mathf.Sqrt((distance_x * distance_x) / (ellipseW / 2 * ellipseW / 2) +
        //            (distance_y * distance_y) / (ellipseH / 2 * ellipseH / 2)); // out of 1

        //        float max_width = Mathf.Sqrt((x - ellipseX) * (x - ellipseX) +
        //            (y - ellipseY) * (y - ellipseY));
        //        distance = max_width * distance; //gets the actual distance. 
        //        float delta = distance / max_width;
        //        float gradient = delta * delta;

        //        sample *= Mathf.Max(0.0f, 1f - gradient);
        //        texture.SetPixel(x, y, coloring.Evaluate(sample));
        //    }
        //}
    }
}
