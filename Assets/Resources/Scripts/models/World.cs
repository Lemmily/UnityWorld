using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class World : IXmlSerializable {
    public int Width;
    public int Height;



    Tile[,] map;
    private Action<Tile> cbTileChanged;

    public World (int width, int height) {
        SetupWorld(width, height);
    }

    private void SetupWorld(int width, int height) {
        Width = width;
        Height = height;

        map = new Tile[Width, Height];
        MapInfo generatedMap = new MapInfo(Width, Height);
        WorldGenerator.seed = 133.564f;
        generatedMap = WorldGenerator.Start(generatedMap);

        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                map[x, y] = new Tile(this, x, y, generatedMap.getTile(x, y, "terrain"));
                map[x, y].RegisterTileTypeChangedCallback(OnTileChanged);
            }
        }
    }

    public void RegisterTileChanged(Action<Tile> callbackfunc) {
        cbTileChanged += callbackfunc;
    }

    public void UnregisterTileChanged(Action<Tile> callbackfunc) {
        cbTileChanged -= callbackfunc;
    }


    public void Update (float deltaTime) {

    }

    public Tile GetTileAt(Vector2 tileCoord) {
        return GetTileAt(
            Mathf.FloorToInt(tileCoord.x),
            Mathf.FloorToInt(tileCoord.y));
    }

    public Tile GetTileAt(int x, int y) {
        if (x >= Width || x < 0 || y >= Height || y < 0) {
            return null;
        }
        return map[x, y];
    }

    void OnTileChanged(Tile t) {
        if(cbTileChanged == null) {
            return;
        }
        cbTileChanged(t);
    }
            

    public XmlSchema GetSchema() {
        throw new NotImplementedException();
    }

    public void ReadXml(XmlReader reader) {
        throw new NotImplementedException();
    }

    public void WriteXml(XmlWriter writer) {
        throw new NotImplementedException();
    }

}
