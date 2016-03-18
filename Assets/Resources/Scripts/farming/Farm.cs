using UnityEngine;
using System.Collections;
using System;

public class Farm : MapType
{
    
    void Start() {
        mapInfo = GetComponent<MapInfo>();
    }
    
    void Update() {

    }
    public void gainActionPoints(int points) {
        //cascade through all tiles that need to be told about actionpoints.
        
    }

    public override void MouseClick(int clickType) {
        Debug.Log(mapInfo.mapName + " got cliked with " + clickType);
    }
}
