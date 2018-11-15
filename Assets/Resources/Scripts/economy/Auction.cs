using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

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
        requests = new List<Request>();
        requestsByType = new Dictionary<string, List<Request>>();
        requestsByType.Add("buy", new List<Request>());
        requestsByType.Add("sell", new List<Request>());
    }

    public void Update(int points) {
        if ( ! permenant) {
            timeLeft -= points;
        }
        //do update
        Debug.Log("Auction for " + resource.ToString());
        resolveRequests();

        checkForDeadRequests();
    }

    private void checkForDeadRequests() {
        List<Request> dead = new List<Request>();
        foreach (Request request in requests) {
            if ( ! request.active ) {
                dead.Add(request);
            }
        }

        for (int i = 0; i < dead.Count; i++) {
            requests.Remove(dead[i]);
            if(dead[i].buy) {
                requestsByType["buy"].Remove(dead[i]);
            } else {
                requestsByType["sell"].Remove(dead[i]);
            }

        }
        
    }

    public void addRequest(Request request) {
        if (! request.CheckValid()) {
            Debug.LogError("Auction.AddRequest, tried to add an invalid request to auction. " + request.owner + " " + request.quantity + "," + request.resource);
            return;
        }
        requests.Add(request);
        if (request.IsBuy()) {
            requestsByType["buy"].Add(request);
        } else {
            requestsByType["sell"].Add(request);
        }
    }

    public void resolveRequests() {
        List<Request> buys = requestsByType["buy"].OrderByDescending(o => o.price).ToList();
        List<Request> sells = requestsByType["sell"].OrderBy(o => o.price).ToList();


        foreach (Request buy in buys) {
            int sellIndex = 0;
            bool traded = false;
            while ( ! traded && sellIndex < sells.Count) {
                traded = buy.owner.AskToBuy(sells[sellIndex]);
                
                if ( ! traded) {
                    //try next one.
                    sellIndex ++;
                } else {
                    if(sells[sellIndex].Negotiate(buy)) {
                        Debug.Log("Successful Trade of " + buy.ToString());
                    }
                }
            }
        }

    }
}
