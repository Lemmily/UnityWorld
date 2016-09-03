using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MapInfo))]
public abstract class MapType : MonoBehaviour {

    protected MapInfo mapInfo;

    // 0, 1 or 2
    public void MouseClick(int clickType, Vector2 tileCoord) {
        Debug.Log(mapInfo.mapName + " got cliked with " + clickType + " at " + tileCoord);
    }
    public void MouseClick(int clickType) {
        Debug.Log(mapInfo.mapName + " got cliked with " + clickType);
    }

}
