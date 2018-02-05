using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System;

public class Tile : IXmlSerializable
{
    private float baseTileMovementCost;
    private World world;
    private Action<Tile> cbTileChanged;
    public int tileType;
    public float movementCost
    {
        get
        {
            return baseTileMovementCost; //todo: add in things that can be in the tile that alter movement cost.
        }
    }



    public int X
    {
        get;

        protected set;
    }

    public int Y
    {
        get;

        protected set;
    }

    public Tile(World world) {
        this.world = world;
    }

    public Tile(World world, int x, int y, int tileType) : this(world) {
        this.X = x;
        this.Y = y;
        this.tileType = tileType;
    }

    internal void RegisterTileTypeChangedCallback(Action<Tile> callback) {
        cbTileChanged += callback;
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

    public bool IsEnterable() {
        switch (tileType) {
            default:
                return true; //everything is enterable unless a specific case.
        }
    }
}
