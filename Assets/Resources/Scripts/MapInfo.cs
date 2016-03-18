using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapInfo : MonoBehaviour {
    public int width;
    public int height;
    public bool updating;
    public String mapName;

    public int[,] map;

    public Dictionary<string, int[,]> layers;
    public int resolutionX;
    public int resolutionY;
    public float tileSizeX;
    public float tileSizeY;
    public Dictionary<string, VisualTiles> visualLayers;
    internal bool finished = false;


    public Material terrainMat;
    public Material objectMat;
    public Material itemMat;
    public Material entityMat;

    //private List<VisualTiles> visualLayers;

    // Use this for initialization
    void Start () {
        tileSizeX =  resolutionX / width;
        tileSizeY = resolutionY / height;

        map = new int[width,height];
        layers = new Dictionary<string, int[,]>();

        visualLayers = new Dictionary < string, VisualTiles>();
        GameObject gObj = new GameObject("TerrainVisualTiles");
        gObj.transform.parent = gameObject.transform;
        VisualTiles vTiles = gObj.AddComponent<VisualTiles>();
        vTiles.SetMapInfo(this);
        vTiles.BuildTerrainMesh();
        visualLayers.Add("Terrain", vTiles);
        gObj.GetComponent<MeshRenderer>().sharedMaterial = terrainMat;

        gObj = new GameObject("EntityVisualTiles");
        gObj.transform.parent = gameObject.transform;
        gObj.transform.localPosition = new Vector3(0, 0, -0.1f);
        vTiles = gObj.AddComponent<VisualTiles>();
        vTiles.SetMapInfo(this);
        vTiles.BuildTerrainMesh();
        visualLayers.Add("Entities", vTiles);
        gObj.GetComponent<MeshRenderer>().sharedMaterial = entityMat;


        finished = true;
        DoMeshUpdate();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector2 findCoord(Vector3 point) {
        float x = point.x - this.gameObject.transform.localPosition.x;
        float y = point.y - this.gameObject.transform.localPosition.y;
        return new Vector2((int)(x / tileSizeX), (int)(y / tileSizeY));
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

    internal void DoMeshUpdate() {
        if (!finished) {
            return;
        }
        foreach (KeyValuePair<string, VisualTiles> pair in visualLayers) {
            pair.Value.UpdateMesh();
        }
    }

    internal string GetName() {
        return mapName;
    }
}
