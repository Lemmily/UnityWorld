using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Auction {
    private bool permenant;
    private int timeLeft;
    private Resource resource;

    public List<Request> requests;
    public Dictionary<String, List<Request>> requestsByType;
    public Auction(Resource res, bool permenancy, int timeActive=0) {
        resource = res;
        permenant = permenancy;
        timeLeft = timeActive;
    }

    internal void Update(int points) {
        if ( ! permenant) {
            timeLeft -= points;
        } 
        //do update
    }


    public void addRequest(Request request) {
        if (request.isBuy()) {
            requestsByType["buy"].Add(request);
        } else {
            requestsByType["sell"].Add(request);
        }
    }
}
