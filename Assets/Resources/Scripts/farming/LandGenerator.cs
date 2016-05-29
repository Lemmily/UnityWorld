using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MapInfo))]
public class LandGenerator : MonoBehaviour
{
    public MapInfo mapInfo;
    public float seed;

    public float[,] workingMap;
    public int[,] setMap;

    public float[,] maskMap;
    // Use this for initialization
    void Start() {
        mapInfo = gameObject.GetComponent<MapInfo>();
        workingMap = new float[mapInfo.width, mapInfo.height];
        maskMap = new float[mapInfo.width, mapInfo.height];
        setMap = new int[mapInfo.width, mapInfo.height];
        firstPass();
        workingMap = normalise(workingMap);
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
                if (highest < map[x, y]) {
                    highest = map[x, y];
                }
                else if (lowest > map[x, y]) {
                    lowest = map[x, y];
                }
            }
        }

        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                map[x, y] = 8 + ((map[x, y] - lowest) / (highest - lowest)) * 4;
            }
        }

        return map;
    }

    private void firstPass() {
        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
                float xCoord = (seed + (float)x / 8f);
                float yCoord = (seed + (float)y / 8f);
                workingMap[x, y] += Mathf.PerlinNoise(xCoord, yCoord);
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
                float xCoord = ((1 + seed) * 6.5f + (float)x / (float)mapInfo.width);
                float yCoord = ((1 + seed) * 6.5f + (float)y / (float)mapInfo.height);
                workingMap[x, y] += Mathf.PerlinNoise(xCoord, yCoord);
            }
        }
    }
    // Update is called once per frame
    void Update() {

    }


    
}
