using UnityEngine;
using System.Collections.Generic;

public class Economy : MonoBehaviour {
    private int points;
    private List<Auction> auctions;
    private Dictionary<Resource, Auction> auctionByType;
    private int updateSpeed = 5;

    private List<Business> businessList = null;

    // Use this for initialization
    void Start () {
        points = 0;
        auctionByType = new Dictionary<Resource, Auction>();
        auctions = new List<Auction>();


        //Create Businesses
        businessList = new List<Business>();
        businessList.Add(new Business("Emily's Ripe Peaches", Resource.POTTERY));
        businessList.Add(new Business("Emily's Sour Plums", Resource.IRON));
        businessList.Add(new Business("Emily's Spicy Nachos", Resource.UNKNOWN));

    }
	
	// Update is called once per frame
	void Update () {
        if (points > updateSpeed) {
            int count = auctions.Count;
            for (var i = 0; i < count; i++) auctions[i].Update(points);
        }
    }

    public void gainActionPoints(int points) {
        Debug.Log("Economy gained points");
        this.points += points;
        foreach(Business b in businessList)
        {
            b.gainActionPoints(points);
        }
    }



    public enum Resource
    {
        UNKNOWN,
        IRON_ORE,
        COPPER_ORE,
        COAL,
        WOOD,
        CLAY,
        IRON,
        COPPER,
        COPPER_SWORD,
        IRON_SWORD,
        POTTERY,
        CLAY_BRICK
    }
}
