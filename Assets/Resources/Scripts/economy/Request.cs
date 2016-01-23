using UnityEngine;
using System.Collections;
using System;

public class Request  {

    public bool  buy;
    public Trader owner;
    public int price;
    public int quantity;
    public Resource resource;
    public bool active;

    public Request(Trader owner, bool buy, Resource res, int quantity, int price=-1) {
        this.buy = buy;
        this.resource = res;
        this.quantity = quantity;
        this.price = price;
        this.owner = owner;
        this.active = true;
    }

    internal bool isBuy() {
        return buy;
    }

    internal bool sellTo(Request buy) {
        //temp i imagine...


        //"negotiate" price. really crude. max should be seller price
        //min should be buyer price.
        int lPrice = (buy.price + this.price) / 2;
        Debug.Log("Negotiating for " + resource + ", starting at " + lPrice);
        for (int i = 0; i < 3; i++) {
            if (UnityEngine.Random.value < 0.5f) {
                //seller wins
                lPrice += this.price;
                lPrice /= 2;
                Debug.Log("round " + i + " seller won, price now: " + lPrice);
            } else {
                //buyer wins
                lPrice += buy.price;
                lPrice /= 2;
                Debug.Log("round " + i + " buyer won, price now: " + lPrice);
            }

        }

        Debug.Log("final price is " + lPrice);

        if (buy.owner.aqcuire(this, buy, lPrice)) return true;
        else return false;


    }

    internal void checkValid() {
        if (quantity <= 0) {
            //dieeeeeee request.
            active = false;
        }
    }


    public override string ToString() {
        return resource.name + " x " + quantity + " at " + price;
    }
}
