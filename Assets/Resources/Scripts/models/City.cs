using UnityEngine;
using System.Collections;

public class City : IPlace  {

    public int x { get; protected set; }
    public int y { get; protected set; }

    public string description { get; protected set; }
    public string name { get; protected set; }

    //temp stuff to prove concept//
    int money = 999999;

    public City(int x, int y, string desc) {
        this.x = x;
        this.y = y;
        this.description = desc;
        this.name = "City";
    }

    public int buyAllEquipmentFrom(Inventory inventory) {

        if (inventory.isEmpty()) {
            return 0;
        }


        return 0;
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
