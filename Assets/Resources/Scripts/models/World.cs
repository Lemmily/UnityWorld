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


    public enum PlaceType { City, Dungeon, Farm };

    MapTile[,] map;
    private Action<MapTile> cbTileChanged;
    private List<Dungeon> dungeons;
    private List<City> cities;

    private Dictionary<MapTile, IPlace> allPlaces;

    public World (int width, int height) {
        SetupWorld(width, height);
    }

    public void Update(float deltaTime) {

    }


    private void SetupWorld(int width, int height) {
        Width = width;
        Height = height;

        map = new MapTile[Width, Height];
        MapInfo generatedMap = new MapInfo(Width, Height);
        WorldGenerator.seed = 216.724f;//       133.564f;
        generatedMap = WorldGenerator.Start(generatedMap);

        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                map[x, y] = new MapTile(this, x, y, generatedMap.getTile(x, y, "terrain"));
                map[x, y].RegisterTileTypeChangedCallback(OnTileChanged);
            }
        }

        allPlaces = new Dictionary<MapTile, IPlace>();

        dungeons = new List<Dungeon>();
        CreateDungeon();
        CreateDungeon(105, 100, "DUNGEEEEOONNN \n pheeeeer meee");

        cities = new List<City>();
        CreateCity(95, 95, "City phantabulous");
    }

    internal IPlace GetPlace(MapTile t) {
        if (CheckForPlace(t))
            return allPlaces[t];
        return null;
    }

    internal bool CheckForPlace(MapTile t) {
        return allPlaces.ContainsKey(t);
    }
    internal IPlace CheckForPlace(int x, int y)
    {
        if (CheckForPlace(GetTileAt(x, y))) {
            return GetPlace(GetTileAt(x, y));
        }
        return null;
    }


    public List<Dungeon> GetDungeons() {
        return dungeons;
    }
    internal List<City> GetCities() {
        return cities;
    }


    private void CreateCity(int x, int y, string desc) {
        City c = new City(x, y, desc);
        cities.Add(c);
        allPlaces.Add(GetTileAt(x, y), c);
    }
    private void CreateDungeon(int x = 100, int y = 100, string desc = null) {
        Dungeon d = new Dungeon(x, y, desc);
        dungeons.Add(d);
        allPlaces.Add(GetTileAt(x, y), d);
    }
    public Dungeon CheckForDungeon(int x, int y) {
        foreach (Dungeon dungeon in dungeons) {
            if (dungeon.X == x && dungeon.Y == y) {
                Debug.Log("I checked and there's a dungeon here!\n" + dungeon);
                return dungeon;
            }
        }
        return null;
    }

    public void RegisterTileChanged(Action<MapTile> callbackfunc) {
        cbTileChanged += callbackfunc;
    }


    public void UnregisterTileChanged(Action<MapTile> callbackfunc) {
        cbTileChanged -= callbackfunc;
    }



    public MapTile GetTileAt(Vector2 tileCoord) {
        return GetTileAt(
            Mathf.FloorToInt(tileCoord.x),
            Mathf.FloorToInt(tileCoord.y));
    }

    public MapTile GetTileAt(int x, int y) {
        if (x >= Width || x < 0 || y >= Height || y < 0) {
            return null;
        }
        return map[x, y];
    }


    void OnTileChanged(MapTile t) {
        if(cbTileChanged == null) {
            return;
        }
        cbTileChanged(t);
    }
            





    ////// SAVING & LOADING /////////
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
