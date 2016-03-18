using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MapInfo))]
public abstract class MapType : MonoBehaviour {

    protected MapInfo mapInfo;
    // 0, 1 or 2
    public abstract void MouseClick(int clickType);

}
