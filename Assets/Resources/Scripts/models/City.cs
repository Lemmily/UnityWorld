using UnityEngine;
using System.Collections;
using System;

public class City : IPlace  {

    public int X { get; protected set; }
    public int Y { get; protected set; }

    public string Description { get; protected set; }
    public string Name { get; protected set; }

    public World.PlaceType Type
    {
        get
        {
            return World.PlaceType.City;
        }
    }

    //temp stuff to prove concept//
    int money = 999999;
    Inventory inventory;
    public City(int x, int y, string desc) {
        this.X = x;
        this.Y = y;
        this.Description = desc;
        this.Name = "City";
        this.inventory = new Inventory();
    }

    /*
        Returns the money "used" to buy items.
    */
    public int BuyAllEquipmentFrom(Inventory pInventory) {

        if (pInventory.isEmpty()) {
            Debug.Log("There are no items in the inventory!");
            return 0;
        }
        int cost = AssessCost(pInventory);
        if (cost < money) {
            inventory.items += pInventory.items;
            pInventory.items = 0;
        }

        Debug.Log(Name + " spent " + cost + " on items!");
        return cost;
    }

    private int  AssessCost(Inventory pInventory) {
        if (pInventory.isEmpty()) {
            return 0;
        } else {
            //for (int i = 0; i < pInventory.items; i++) {
            //    tally += 30; //TODO: make this an actual cost not hard coded.
            //}
            return 30 * pInventory.items;
        }
        
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
