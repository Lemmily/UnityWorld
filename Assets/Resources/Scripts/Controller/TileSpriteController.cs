using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class TileSpriteController : MonoBehaviour {


    private Dictionary<Tile, GameObject> tileGameObjectMap;


    private Stack<GameObject> activeObjects;

    private Stack<GameObject> deactivatedObjects;
    GameObject tile_container;


    public World World
    {
        get
        {
            return WorldController.Instance.world;
        }
    }

	// Use this for initialization
	void Start () {
	    if( ! ResourceLoader.instance.doneLoading) {
            Debug.LogError("resource loader did not finish? .");
        }

        deactivatedObjects = new Stack<GameObject>();
        activeObjects = new Stack<GameObject>();

        tileGameObjectMap = new Dictionary<Tile, GameObject>();

        tile_container = new GameObject();
        tile_container.transform.SetParent(this.transform);
        tile_container.name = "Tiles";

        //for (int x = 0; x < World.Width; x++) {
        //    for (int y = 0; y < World.Height; y++) {
        //        Tile tile_data = World.GetTileAt(x, y);
        //        GameObject tile_go = new GameObject();
        //        tile_go.name = "Tile_" + x + "_" + y;
        //        tile_go.transform.position = new Vector3(tile_data.x, tile_data.y, 0);

        //        SpriteRenderer sr = tile_go.AddComponent<SpriteRenderer>();
        //        sr.sprite = ResourceLoader.GetTileSprite(tile_data);
        //        sr.sortingLayerName = "Tiles";
        //    }
        //}

        World.RegisterTileChanged(OnTileChanged);
        PlayerController.Instance.player.RegisterPlayerMovedCallback(OnPlayerMoved);


        // does this need to be called here?

        OnPlayerMoved(PlayerController.Instance.player);
	}


    void OnTileChanged(Tile tile_data) {
        //do stuff;
    }

    void OnPlayerMoved(Player player) {
        //Is this actually bing called?
        //are there tiles being generated?
        //are they in the wrong place?
        //is the camera start/end points correct?

        //camera has already been updated.
        Vector3 start = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Debug.Log(start + ", " + end);
        //"redraw" tiles.

        Stack<GameObject> old_active_go = new Stack<GameObject>(activeObjects);

        activeObjects = new Stack<GameObject>();

        for (int x = (int)start.x - 1; x < end.x + 1 ; x++) {
            for (int y = (int)start.y - 1; y < end.y + 1; y++) {
                Tile tile_data = WorldController.Instance.world.GetTileAt(x, y);
                if (tile_data == null)
                    continue;
                GameObject go;
                if (old_active_go.Count > 0) {
                    go = old_active_go.Pop();
                } else {
                    if (deactivatedObjects.Count == 0) {
                        CreateNewBatch();
                    }
                    go = deactivatedObjects.Pop();
                }
                go.SetActive(true);
                go.name = "Tile_" + x + "_" + y;
                go.transform.position = new Vector3(tile_data.x, tile_data.y, 0);

                SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                sr.sprite = ResourceLoader.GetTileSprite(tile_data);
                activeObjects.Push(go);
            }
        }

        foreach (GameObject go in old_active_go) {
            go.SetActive(false);
            deactivatedObjects.Push(go);
        }
    }

    private void CreateNewBatch() {
        for (int i = 0; i < 10; i++) {
            GameObject go = new GameObject();
            go.SetActive(false);
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "Tiles";
            deactivatedObjects.Push(go);
            go.transform.SetParent(tile_container.transform);
            go.name = "unused_tile";
        }

    }
}
