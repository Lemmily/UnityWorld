using UnityEngine;
using System.Collections;
using System;

public class Request  {

    private bool  buy;
    private int quantity;
    private Resource resource;

    public Request(bool buy, Resource res, int quantity) {
        this.buy = buy;
        this.resource = res;
        this.quantity = quantity;

    }

    internal bool isBuy() {
        return buy;
    }
}
