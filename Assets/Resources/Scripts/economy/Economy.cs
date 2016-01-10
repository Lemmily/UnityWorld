using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;

public class Economy : MonoBehaviour {
    private int points;
    private List<Auction> auctions;
    private Dictionary<Resource, Auction> auctionByType;
    private int updateSpeed = 5;
    private Dictionary<String, Resource> resources;
    // Use this for initialization
    void Start () {
        parseResources();
        points = 0;
        auctionByType = new Dictionary<Resource, Auction>();
        auctions = new List<Auction>();


        foreach (Resource res in Enum.GetValues(typeof(Resource))) {
            Auction auc = new Auction(res, true);
        }
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
}

    public void gainActionPoints(int points) {
        Debug.Log("Economy gained points");
        this.points += points;
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
