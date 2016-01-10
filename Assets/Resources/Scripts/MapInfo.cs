using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapInfo : MonoBehaviour {
    public int width;
    public int height;
    public bool updating;

    public int[,] map;

    public Dictionary<string, int[,]> layers;

    // Use this for initialization
    void Start () {
        map = new int[width,height];

        layers = new Dictionary<string, int[,]>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
        public void setLayer(string name, int[,] layer) {
        if (layers.ContainsKey(name)) {
            layers[name] = layer;
        } else {
            layers.Add(name, layer);
        }
    }

    internal int getTile(int x, int y) {
        return map[x, y];
    }

    internal void setMap(int[,] setMap) {
        map = setMap;
        updating = true;
    }
}
