using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class TileSpriteController : MonoBehaviour {


    private Dictionary<Tile, GameObject> tileGameObjectMap;


    private List<GameObject> activeObjects;

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
        activeObjects = new List<GameObject>();

        tileGameObjectMap = new Dictionary<Tile, GameObject>();
        GameObject tile_container = new GameObject();


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
	}
	


    void OnTileChanged(Tile tile_data) {
        //do stuff;
    }

    void OnPlayerMoved(Player player) {

        //camera has already been updated.
        Vector3 start = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));


        //"redraw" tiles.

        List<GameObject> old_active_go = new List<GameObject>(activeObjects);

        activeObjects = new List<GameObject>();
        

        for (int x = (int)start.x; x < end.x ; x++) {
            for (int y = (int)end.y; y < end.y; y++) {
                Tile tile_data = WorldController.Instance.world.GetTileAt(x, y);
                GameObject go;
                if (old_active_go.Count > 0) {
                    go = old_active_go[old_active_go.Count - 1];
                    old_active_go.Remove(gameObject);
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
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "Tiles";
            deactivatedObjects.Push(go);
            go.transform.SetParent(tile_container.transform);
        }
        
    }
}
