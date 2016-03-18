using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VisualTiles : MonoBehaviour {
    public MeshFilter meshFilter;
    private Vector2[] UVArray;
    public Mesh terrainMesh;
    public MapInfo mapInfo;
    private float tileSizeX;
    private float tileSizeY;
       

    void Start() {
        meshFilter = GetComponent<MeshFilter>();
    }


    void Update() {
        if (mapInfo.finished && mapInfo.updating) {
            UpdateMesh();
            mapInfo.updating = false;
        }
    }

    internal void SetMapInfo(MapInfo mapInfo) {
        this.mapInfo = mapInfo;
        tileSizeX = mapInfo.tileSizeX;
        tileSizeY = mapInfo.tileSizeY;
    }

    private void BuildMesh(Mesh mesh) {
        if (mapInfo == null) {
            Debug.Log("MapInfo is null for " + gameObject.name);
        }
        if (meshFilter == null) {
            meshFilter = GetComponent<MeshFilter>();
            if (meshFilter == null) {
                meshFilter = gameObject.AddComponent<MeshFilter>();
            }
        }
        int numTiles = mapInfo.width * mapInfo.height;
        int numTriangles = numTiles * 6;
        int numVerts = numTiles * 4;

        Vector3[] vertices = new Vector3[numVerts];
        UVArray = new Vector2[numVerts];

        int x, y, iVertCount = 0;
        for (x = 0; x < mapInfo.width; x++) {
            for (y = 0; y < mapInfo.height; y++) {
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
        terrainMesh = mesh;
    }

    public void BuildTerrainMesh() {
        BuildMesh(terrainMesh);
        MeshCollider meshC = gameObject.AddComponent<MeshCollider>();
        //meshC.sharedMesh = null;
        //meshC.sharedMesh = mesh;
        //UpdateMesh(); //I put this in a separate method for my own purposes.
    }

    public string GetName() {
        return mapInfo.mapName;
    }

    //Note, assuming a tile atlas with 16 total tiles in a 4x4 grid.

    public void UpdateMesh() {
        //if (mapInfo.updating) {
        //    return;
        //}
        int iVertCount = 0;
        //tileNum =


        for (int x = 0; x < mapInfo.width; x++) {
            for (int y = 0; y < mapInfo.height; y++) {
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
