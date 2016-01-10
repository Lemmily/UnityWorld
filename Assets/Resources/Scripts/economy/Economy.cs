using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;

public class Economy : MonoBehaviour {
    private int points;
    private List<Auction> auctions;
    private Dictionary<Resource, Auction> auctionByType;
    private int updateSpeed = 5;

    private List<Business> businessList = null;
    private Dictionary<String, Resource> resources;

    // Use this for initialization
    void Start () {
        parseResources();
        points = 0;
        auctionByType = new Dictionary<Resource, Auction>();
        auctions = new List<Auction>();

        //Create Businesses
        businessList = new List<Business>();
        businessList.Add(new Business("Emily's Ripe Peaches", Resource.POTTERY));
        businessList.Add(new Business("Emily's Sour Plums", Resource.IRON));
        businessList.Add(new Business("Emily's Spicy Nachos", Resource.UNKNOWN));

    }
	

    private void parseResources() {
        // Get the file into the document.
        //XmlDocument root = new XmlDocument();
        //root.Load();

        //language = root.SelectSingleNode("localizableStrings/meta/language").InnerText;
        //grouping = root.SelectSingleNode("localizableStrings/meta/grouping").InnerText;

        //"resources.xml"
    }

    // Update is called once per frame
    void Update () {
        //by default only updates every 5 time units.

        if (points > updateSpeed) {
            int count = auctions.Count;
            for (var i = 0; i < count; i++) auctions[i].Update(points);
        }
        foreach (Resource res in Enum.GetValues(typeof(Resource)))
        {
            Auction auc = new Auction(res, true);
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


    ////temp I think.
    //public enum Resource
    //{
    //    UNKNOWN,
    //    IRON_ORE,
    //    COPPER_ORE,
    //    COAL,
    //    WOOD,
    //    CLAY,
    //    IRON,
    //    COPPER,
    //    COPPER_SWORD,
    //    IRON_SWORD,
    //    POTTERY,
    //    CLAY_BRICK
    //}
}
