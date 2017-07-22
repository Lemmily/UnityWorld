using UnityEngine;
using System.Collections.Generic;
using System;

public class Dungeon : IPlace
{
    public int X { get; protected set; }
    public int Y { get; protected set; }

    public string Description { get; protected set; }

    public string Name { get; protected set; }

    public World.PlaceType Type
    {
        get
        {
            return World.PlaceType.Dungeon;
        }
    }

    public bool beaten;

    public Dungeon(int x, int y) {
        this.X = x;
        this.Y = y;
        beaten = false;
        Description = "";
        Name = "Dungeon";
    }

    public Dungeon(int x, int y, string description) {
        this.X = x;
        this.Y = y;
        beaten = false;
        this.Description = description;
        this.Name = "Dungeon";
    }

    public List<Item> GetItems(Agent agent, Inventory inventory) {


        return null;
    }


    public bool ChallengeDungeon(Agent agent) {

        Debug.Log("Dungeon:- " + agent.name + " challenged the dungeon!");
        //success
        beaten = true;
        return true;
    }

    public string GetDescriptorText() {
        if (Description != null) {
            return Description;
        }
        return "DUNGEON \n This is a Dungeon. Make yourself at home.";
    }

    public string GetName() {
        return Name;
    }
}
