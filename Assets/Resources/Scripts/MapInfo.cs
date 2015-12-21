using UnityEngine;
using System.Collections;
using System;

public class MapInfo : MonoBehaviour {
    public int width;
    public int height;
    public bool updating;

    public int[,] map;

    // Use this for initialization
    void Start () {
        map = new int[width,height];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    

    internal int getTile(int x, int y) {
        return map[x, y];
    }

    internal void SetMap(int[,] setMap) {
        map = setMap;
        updating = true;
    }
}
