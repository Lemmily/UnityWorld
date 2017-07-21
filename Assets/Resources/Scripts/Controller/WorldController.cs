using UnityEngine;
using System.Collections;
using System;

public class WorldController : MonoBehaviour {
    /*
        This is going to be in control interface with the world.
        
    */

    public static WorldController Instance { get; protected set; }

    public World World { get; protected set; }

    
    public Agent Player { get; protected set; }

    static bool loadWorld = false;

    void OnEnable () {
        if (Instance != null)
            Debug.LogError("WorldCOntroller - somehow there was already a worldcontroller.");
        Instance = this;

        if (loadWorld == false) {
            CreateNewWorld();
        } else {
            loadWorld = false;
            CreateWorldFromSave();
        }
    }

    // Update is called once per frame
    void Update() {

        //TODO: Time is challenging.
        if (World != null) 
            World.Update(Time.deltaTime);
    }

    private void CreateWorldFromSave() {
        throw new NotImplementedException();
    }

    private void CreateNewWorld() {
        World = new World(200, 200);
        Player = new Agent();
    }
    

    internal Tile GetTileAtWorldCoord(Vector3 coord) {
        int x = Mathf.FloorToInt(coord.x + 0.5f);
        int y = Mathf.FloorToInt(coord.y + 0.5f);

        return World.GetTileAt(x, y);
    }
}
