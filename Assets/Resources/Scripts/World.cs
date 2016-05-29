using UnityEngine;
using System.Collections;
using System;

public class World : MapType {
    
    void Start () {
        mapInfo = GetComponent<MapInfo>();
    }
	
	void Update () {

    }

    public new void MouseClick(int clickType, Vector2 tileCoord) {
        Debug.Log(mapInfo.mapName + " got cliked with " + clickType);
    }
}
