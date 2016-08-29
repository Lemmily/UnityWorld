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
    private Action<Tile> cbTileTypeChanged;
    public int tileType;
    public float movementCost
    {
        get
        {
            return baseTileMovementCost; //todo: add in things that can be in the tile.
        }
    }



    public int x
    {
        get;

        protected set;
    }

    public int y
    {
        get;

        protected set;
    }

    public Tile(World world) {
        this.world = world;
    }

    public Tile(World world, int x, int y, int tileType) : this(world) {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
    }

    internal void RegisterTileTypeChangedCallback(Action<Tile> callback) {
        cbTileTypeChanged += callback;
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
            case 0:
                return true;

            default:
                return false;
        }
    }
}
