using UnityEngine;
using System.Collections;
using System;

public class Request  {

    private bool  buy;
    private Resource resource;

    public Request(bool buy, Resource res) {
        this.buy = buy;
        resource = res;
    }

    internal bool isBuy() {
        return buy;
    }
}
