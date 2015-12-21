using UnityEngine;
using System.Collections;
using System;

public class VisualTiles : MonoBehaviour {
    private int mapSizeX;
    private int mapSizeY;
    public int resolutionX;
    public int resolutionY;
    private MeshFilter meshFilter;
    private Vector2[] UVArray;
    private Mesh mesh;
    private MapInfo mapInfo;
    private float tileSizeX;
    private float tileSizeY;

    //For this, your GameObject this script is attached to would have a
    //Transform Component, a Mesh Filter Component, and a Mesh Renderer
    //component. You will also need to assign your texture atlas / material
    //to it. 

    void Start() {
        meshFilter = GetComponent<MeshFilter>();
        mapInfo = gameObject.GetComponent<MapInfo>();
        BuildMesh();
    }

    void Update() {
        if (mapInfo.updating) {
            UpdateMesh();
            mapInfo.updating = false;
        }
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (!Physics.Raycast(FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition), out hit)) 
                return;
            
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
                return;

            Texture tex = renderer.material.mainTexture;
            Vector2 point = findCoord(transform.InverseTransformPoint(hit.point));
            print(hit.textureCoord + " is - " + point);
            if (Input.GetMouseButtonDown(1)) {
                //mapInfo.SetTile(point, 1);
                //mapInfo.ChangeTileBy(point, 1);
                UpdateMesh();
            }
        } else if (Input.GetMouseButtonDown(2)) {
            //mapInfo.GenerateMap();
            UpdateMesh();
        }
    }


    private Vector2 findCoord(Vector3 point) {
        return new Vector2((int)(point.x / tileSizeX), (int)(point.y / tileSizeY));
    }

    public void BuildMesh() {
        mapSizeX = mapInfo.width;
        mapSizeY = mapInfo.height;

        tileSizeX = (float)resolutionX / mapSizeX;
        tileSizeY = (float)resolutionY / mapSizeY;
        int numTiles = mapSizeX * mapSizeY;
        int numTriangles = numTiles * 6;
        int numVerts = numTiles * 4;

        Vector3[] vertices = new Vector3[numVerts];
        UVArray = new Vector2[numVerts];

        int x, y, iVertCount = 0;
        for (x = 0; x < mapSizeX; x++) {
            for (y = 0; y < mapSizeY; y++) {
                vertices[iVertCount + 0] = new Vector3(x * tileSizeX, y * tileSizeY, 0);
                vertices[iVertCount + 1] = new Vector3(x * tileSizeX, y * tileSizeY + tileSizeY, 0);
                vertices[iVertCount + 2] = new Vector3(x * tileSizeX + tileSizeX, y * tileSizeY + tileSizeY, 0);
                vertices[iVertCount + 3] = new Vector3(x * tileSizeX + tileSizeX, y * tileSizeY, 0);
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

            iVertCount += 4; iIndexCount += 6;
        }

        mesh = new Mesh();
        //mesh.MarkDynamic(); //if you intend to change the vertices a lot, this will help.
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;
        MeshCollider meshC = gameObject.AddComponent<MeshCollider>();
        //meshC.sharedMesh = null;
        //meshC.sharedMesh = mesh;
        //UpdateMesh(); //I put this in a separate method for my own purposes.
    }

    //Note, the example UV entries I have are assuming a tile atlas 
    //with 16 total tiles in a 4x4 grid.

    public void UpdateMesh() {
        //if (mapInfo.updating) {
        //    return;
        //}
        int iVertCount = 0;
        //tileNum =


        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                int tileType = mapInfo.getTile(x, y);
                Vector2[] UVLocs = GetUVForTile(tileType);
                UVArray[iVertCount + 0] = UVLocs[0]; //Top left of tile in atlas
                UVArray[iVertCount + 1] = UVLocs[1]; //Top right of tile in atlas
                UVArray[iVertCount + 2] = UVLocs[2]; //Bottom right of tile in atlas
                UVArray[iVertCount + 3] = UVLocs[3]; //Bottom left
                iVertCount += 4;
            }
        }

        meshFilter.mesh.uv = UVArray;
    }
    
    private Vector2[] GetUVForTile(int tileType) {
        Vector2[] tempUV = new Vector2[4];
        float tileH = 1f / 4;
        float tileW = 1f / 4;

        //SWITCHED SO THAT IT GOES UP THE COLUMN NOT THE ROW FIRST
        float y = (tileType % 4) * 1f / 4; //TODO: 4 needs to be num plugged in for num of tiles wide tex atlas
        float x = (tileType / 4) * 1f / 4; //TODO: 4 needs to be width in tiles for tile atlass.

        //if (tileType == 2) {
        //    Debug.Log(tileType + ": " + x + "," + y);
        //}
        tempUV[0] = new Vector2(x, y + tileH); //Top left of tile in atlas
        tempUV[1] = new Vector2(x + tileW, y + tileH); //Top right of tile in atlas
        tempUV[2] = new Vector2(x + tileW, y); //Bottom right of tile in atlas
        tempUV[3] = new Vector2(x, y); //Bottom left of tile in atlas

        return tempUV;
    }
}
