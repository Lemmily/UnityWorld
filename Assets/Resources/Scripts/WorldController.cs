using UnityEngine;
using System.Collections;
using System;

public class WorldController : MonoBehaviour {
    public static WorldController Instance
    {
        get;
        protected set;
    }

    internal static WorldController instance;
    public MapInfo mapInfo;

    // Use this for initialization
    void OnEnable () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal MapInfo GetCurrentMapInfo() {
        return mapInfo;
    }

    public void OnMapChange() {

    }
}
