using UnityEngine;
using System.Collections.Generic;
using System;

public class Dungeon : IPlace
{
    public int x { get; protected set; }
    public int y { get; protected set; }

    public string description { get; protected set; }

    public string name { get; protected set; }

    public bool beaten;

    public Dungeon(int x, int y) {
        this.x = x;
        this.y = y;
        beaten = false;
        description = "";
        name = "Dungeon";
    }

    public Dungeon(int x, int y, string description) {
        this.x = x;
        this.y = y;
        beaten = false;
        this.description = description;
        this.name = "Dungeon";
    }

    public List<Item> getItems(Agent agent, Inventory inventory) {


        return null;
    }


    public bool challengeDungeon(Agent agent) {


        //success
        beaten = true;
        return true;
    }

    public string GetDescriptorText() {
        if (description != null) {
            return description;
        }
        return "DUNGEON \n This is a Dungeon. Make yourself at home.";
    }

    public string GetName() {
        return name;
    }
}
