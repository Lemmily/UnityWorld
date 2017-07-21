using UnityEngine;
using System.Collections;
using System;

public class Farm : IPlace
{

    public int X { get; protected set; }
    public int Y { get; protected set; }

    public string Description { get; protected set; }

    public string Name { get; protected set; }

    public World.PlaceType Type
    {
        get
        {
            return World.PlaceType.Farm;
        }
    }

    public string GetDescriptorText()
    {
        if (Description != null) {
            return Description;
        }
        return "DUNGEON \n This is a Dungeon. Make yourself at home.";
    }

    public string GetName()
    {
        return Name;
    }
}

//void Start() {
//    mapInfo = GetComponent<MapInfo>();
//}

//void Update() {

//}
//public void gainActionPoints(int points) {

//}

////public new void MouseClick(int clickType, Vector2 tileCoord) {
////    Debug.Log(mapInfo.mapName + " got cliked with " + clickType);
////}