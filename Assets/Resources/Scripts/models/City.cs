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

        //debuggy
        testAuction();
    }

    /*
        Returns the money "used" to buy items.
    */
    public int BuyAllEquipmentFrom(Inventory pInventory) {

        if (pInventory.isEmpty()) {
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
        return "CITY \n This is a City. Make yourself at home.";
    }

    public string GetName() {
        return Name;
    }




    /* 
     * Ecoonmy testing stuffs.
     * 
     */


    public Auction auction;

    public Trader traderOne = new Trader();
    public Trader traderTwo = new Trader();

    public void testAuction()
    {
        if (auction == null)
        {
            auction = new Auction(new Resource("Goods"), true); //create a permenant auction for goods.
            auction.addRequest(new Request(traderOne, true, new Resource("Goods"), 3));
            auction.addRequest(new Request(traderOne, true, new Resource("Goods"), 3));
            auction.addRequest(new Request(traderOne, true, new Resource("Goods"), 3));
            auction.addRequest(new Request(traderOne, true, new Resource("Goods"), 3));
            auction.addRequest(new Request(traderTwo, false, new Resource("Goods"), 20));
        }

        auction.resolveRequests();
    }







}
