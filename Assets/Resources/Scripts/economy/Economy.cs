using UnityEngine;
using System.Collections.Generic;

public class Economy : MonoBehaviour {
    private int points;
    private List<Auction> auctions;
    private Dictionary<Resource, Auction> auctionByType;
    private int updateSpeed = 5;

    // Use this for initialization
    void Start () {
        points = 0;
        auctionByType = new Dictionary<Resource, Auction>();
        auctions = new List<Auction>();
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
