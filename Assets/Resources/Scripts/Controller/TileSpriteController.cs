using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class TileSpriteController : MonoBehaviour {


    private Dictionary<Tile, GameObject> tileGameObjectMap;


    private Stack<GameObject> activeObjects;

    private Stack<GameObject> deactivatedObjects;

    public bool pooling;
    GameObject tile_container;

    private MeshFilter meshFilter;

    public int tileSize;
    public bool setup;

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

        tile_container = new GameObject();
        tile_container.transform.SetParent(this.transform);
        tile_container.name = "Tiles";
        pooling = true;
        setup = false;
        tileSize = 1;

        World.RegisterTileChanged(OnTileChanged);

        PlayerController.Instance.player.RegisterPlayerMovedCallback(OnPlayerMoved);

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


        if (pooling) {
          DrawWithPooling();
        } else {
          DrawWithMeshes();
        }
    }


    private void DrawWithPooling() {
        if (! setup) {
            deactivatedObjects = new Stack<GameObject>();
            activeObjects = new Stack<GameObject>();
            tileGameObjectMap = new Dictionary<Tile, GameObject>();
            setup = true;
        }
        //camera has already been updated.
        Vector3 start = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Debug.Log(start + ", " + end);

        //"redraw" tiles.
        Stack<GameObject> old_active_go = new Stack<GameObject>(activeObjects);

        activeObjects = new Stack<GameObject>();

        for (int x = (int)start.x - 1; x < end.x + 2 ; x++) {
            for (int y = (int)start.y - 1; y < end.y + 2; y++) {
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
            go.name = "unused_tile";
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


    private void DrawWithMeshes() {
        Vector3 start = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        Vector2[] UVArray = new Vector2[0];
        int width = (int)end.x - (int)start.x;
        int height = (int)end.y - (int)start.y;
        if (! setup) {
            GameObject go = new GameObject();
            go.transform.SetParent(PlayerController.Instance.player_go.transform);
            go.transform.localPosition = new Vector3(-width / 2 + 0.5f, -height / 2 + 0.5f, 0);
            go.AddComponent<MeshRenderer>();
            meshFilter = go.AddComponent<MeshFilter>();
            meshFilter.mesh = BuildMesh(meshFilter.mesh, width, height, out UVArray);
            setup = true;
        }

        int iVertCount = 0;

        for (int x = (int)start.x - 1; x < width + 1; x++) {
            for (int y = (int) start.y - 1; y < height + 1; y++) {
                Tile t = World.GetTileAt(x, y);
                if (t == null)
                    continue;
                int tileType = t.tileType;
                try {
                    Vector2[] UVLocs = GetUVForTile(tileType);
                    UVArray[iVertCount + 0] = UVLocs[0];
                    UVArray[iVertCount + 1] = UVLocs[1];
                    UVArray[iVertCount + 2] = UVLocs[2];
                    UVArray[iVertCount + 3] = UVLocs[3];
                    iVertCount += 4;
                } catch (IndexOutOfRangeException ex) {
                    Debug.LogError("out of bounds -" + iVertCount + "\n" + ex.Message);
                }
            }
        }

        meshFilter.mesh.uv = UVArray;

    }

    private Vector2[] GetUVForTile(int tileType) {
      Vector2[] tempUV = new Vector2[4];


      float tileH = 1f / 4; //FIXME: this "4" is hardcoded - think it applies to the number of rows in the texture.
      float tileW = 1f / 4; //FIXME: same as above, applies to number of columns.


      float y = (tileType % 4) * 1f / 4; //FIXME: "4" is hardcoded - based on texture sheet. 
      float x = (tileType / 4) * 1f / 4; //FIXME: "4" is hardcoded - based on texture sheet.


      tempUV[0] = new Vector2(x, y + tileH);
      tempUV[1] = new Vector2(x + tileW, y + tileH);
      tempUV[2] = new Vector2(x + tileW, y);
      tempUV[3] = new Vector2(x, y);

      return tempUV;
    }


    private Mesh BuildMesh(Mesh mesh, int width, int height, out Vector2[] UVArray ) {
        int numTiles = (width + 2) * (height + 2);
        int numTriangles = numTiles * 6;
        int numVerts = numTiles * 4;

        Vector3[] vertices = new Vector3[numVerts];
        UVArray = new Vector2[numVerts];

        int x, y, iVertCount = 0;
        for (x=0; x < width; x++) {
          for (y=0; y < height ; y++) {
            vertices[iVertCount + 0] = new Vector3(x * tileSize, y * tileSize, 0);
            vertices[iVertCount + 1] = new Vector3(x * tileSize, y * tileSize + tileSize, 0);
            vertices[iVertCount + 2] = new Vector3(x * tileSize + tileSize, y * tileSize + tileSize, 0);
            vertices[iVertCount + 3] = new Vector3(x * tileSize + tileSize, y * tileSize, 0);
            iVertCount += 4;
          }
        }
        int[] triangles = new int[numTriangles];

        int iIndexCount = 0; iVertCount = 0;

        for (int i = 0; i < numTiles; i++) {
          triangles[iIndexCount + 0] += (iVertCount + 0);
          triangles[iIndexCount + 1] += (iVertCount + 1);
          triangles[iIndexCount + 2] += (iVertCount + 2);
          triangles[iIndexCount + 3] += (iVertCount + 0);
          triangles[iIndexCount + 4] += (iVertCount + 2);
          triangles[iIndexCount + 5] += (iVertCount + 3);

          iVertCount += 4;
          iIndexCount += 6;
        }
        mesh = new Mesh();
        mesh.MarkDynamic();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();

        return mesh;
    }

}
