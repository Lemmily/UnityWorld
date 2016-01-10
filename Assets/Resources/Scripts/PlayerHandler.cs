using UnityEngine;
using System.Collections;
using System;

public class PlayerHandler : MonoBehaviour {
    private MapInfo mapInfo;
    private VisualTiles visualTiles;
    private WorldTime worldTime;

    // Use this for initialization
    void Start () {
        mapInfo = GetComponent<MapInfo>();
        worldTime = GetComponent<WorldTime>();
        visualTiles = GetComponent<VisualTiles>();
    }

    // Update is called once per frame

    void Update() {


        /// MOUSE HANDLER
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            if (!Physics.Raycast(FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
                return;

            Renderer renderer = hit.collider.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
                return;

            Texture tex = renderer.material.mainTexture;
            Vector2 point = visualTiles.findCoord(transform.InverseTransformPoint(hit.point));
            print(hit.textureCoord + " is - " + point);
            if (Input.GetMouseButtonDown(1)) {
                //mapInfo.SetTile(point, 1);
                //mapInfo.ChangeTileBy(point, 1);
                visualTiles.UpdateMesh();
            }
        }
        else if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (!Physics.Raycast(FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
                return;

            Vector2 point = visualTiles.findCoord(transform.InverseTransformPoint(hit.point));
            GetComponent<WorldGenerator>().addHill(30, 30, (int)point.x, (int)point.y);
        }
        else if (Input.GetMouseButtonDown(2)) {
            //mapInfo.GenerateMap();
            Debug.Log("apply mask!");

            GetComponentInChildren<WorldGenerator>().applyMask();
            GetComponentInChildren<WorldGenerator>().commit();
            visualTiles.UpdateMesh();
        }
        /// KEYBOARD HANDLERS
        /// 
        if (Input.GetKeyDown(KeyCode.A) ){
            //do  dummyturn
            worldTime.advanceTime(10);
        }
    }

    private void handleKey() {
    }
}
