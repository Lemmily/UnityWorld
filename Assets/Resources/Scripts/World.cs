using UnityEngine;
using System.Collections;
using System;

public class World : MapType {
    
    void Start () {
        mapInfo = GetComponent<MapInfo>();
    }
	
	void Update () {

    }

    public override void MouseClick(int clickType) {
        Debug.Log(mapInfo.mapName + " got cliked with " + clickType);
    }
}
