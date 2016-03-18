using UnityEngine;
using System.Collections;
using System;

public class PlayerHandler : MonoBehaviour {
    private MapInfo mapInfo;
    private WorldTime worldTime;

    // Use this for initialization
    void Start () {
        mapInfo = GetComponent<MapInfo>();
        worldTime = GetComponent<WorldTime>();
        if (worldTime == null) {
            worldTime = GetComponentInParent<WorldTime>();
        }
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
            else {

                mapInfo = (! mapInfo.Equals(meshCollider.gameObject.GetComponent<MapInfo>()) ? meshCollider.gameObject.GetComponent<MapInfo>() : mapInfo );
            }

            Texture tex = renderer.material.mainTexture;
            Vector2 point = mapInfo.findCoord(transform.InverseTransformPoint(hit.point));
            print(hit.textureCoord + " is - " + point + "On map " + mapInfo.GetName());
            if (Input.GetMouseButtonDown(1)) {
                //mapInfo.SetTile(point, 1);
                //mapInfo.ChangeTileBy(point, 1);
                mapInfo.DoMeshUpdate();
            }
        }
        else if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (!Physics.Raycast(FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
                return;
            else
                mapInfo = (!mapInfo.Equals(hit.collider.gameObject.GetComponent<MapInfo>()) ? hit.collider.gameObject.GetComponent<MapInfo>() : mapInfo);


            Vector2 point = mapInfo.findCoord(transform.InverseTransformPoint(hit.point));
            GetComponent<MapType>().MouseClick(0);
        }
        else if (Input.GetMouseButtonDown(2)) {
            //mapInfo.GenerateMap();
            Debug.Log("apply mask!");

            GetComponentInChildren<WorldGenerator>().applyMask();
            GetComponentInChildren<WorldGenerator>().commit();
            mapInfo.DoMeshUpdate();
        }
        /// KEYBOARD HANDLERS
        /// 
        if (Input.GetKeyDown(KeyCode.A) ){
            //do  dummyturn
            worldTime.advanceTime(10);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            worldTime.TogglePause();
        }
    }

    private void handleKey() {
    }
}
