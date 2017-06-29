using UnityEngine;
using System.Collections;
using System;

public class Inventory {

    public int items = 0;



    public void addItem(Item item) {
        items += 1;
    }
        
    
    public bool isEmpty() {
        return true;
    }
}
