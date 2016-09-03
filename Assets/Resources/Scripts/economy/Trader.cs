using UnityEngine;
using System.Collections.Generic;
using System;

public class Trader  : MonoBehaviour {


    public Dictionary<Resource, int> inventory;
    public int money;

    void Start() {
        inventory = new Dictionary<Resource, int>();
        money = 2000;
    }


    void Update() {

    }


    /*
     request: this is the sell request you're asking to buy from.
    */
    internal bool AskToBuy(Request request) {
        //goes to owner of goods in sell request and negotiates
        float i = UnityEngine.Random.value;

        if ( i < 0.5f) {
            //request accepted
            return true;
        } //else rejected..

        return false; // not traded
    }

    internal bool aqcuire(Request sell, Request buy, int price) {

        //some of this needs to go back into the request object.
        //the request can then ask if the trader is able to 

        int quantity = Mathf.Min(buy.quantity, sell.quantity);
        
        if (quantity != 0 && money > quantity * price) {
            sell.owner.giveMoney(quantity * price);
            sell.quantity -= quantity;
            sell.checkValid();

            this.money -= quantity * price;
            aquireResource(buy.resource, quantity);
            return true;
        }

        return false;
    }

    private void aquireResource(Resource resource, int quantity) {
        if(inventory.ContainsKey(resource)) {
            inventory[resource] += quantity;
        } else {
            inventory.Add(resource, quantity);
        }
    }

    private void giveMoney(int money) {
        this.money += money;
    }
}
