using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Linq;

public class Economy : MonoBehaviour {
    private int points;
    private List<Auction> auctions;
    private Dictionary<Resource, Auction> auctionByType;
    private int updateSpeed = 5;

    private List<Business> businessList = null;
    private Dictionary<String, Resource> resources;

    public Trader traderOne;
    public Trader traderTwo;
    // Use this for initialization
    void Start () {
        points = 0;

 
        resources = new Dictionary<string, Resource>();
        parseResources();

        auctionByType = new Dictionary<Resource, Auction>();
        auctions = new List<Auction>();
        foreach (Resource res in resources.Values) {
            Auction auc = new Auction(res, true);
            auctionByType.Add(res, auc);
            auctions.Add(auc);
        }

        foreach (Resource res in resources.Values) {
            for (int i = 0; i < UnityEngine.Random.value * 10; i++) {
                int price = UnityEngine.Random.Range(1, 100); //(int)UnityEngine.Random.value * 100;
                int quantity = UnityEngine.Random.Range(1, 10); // (int)UnityEngine.Random.value * 10;
                bool buy = (UnityEngine.Random.Range(0, 10) < 5);

                if (UnityEngine.Random.Range(0,10) < 5) {
                    auctionByType[res].addRequest(new Request(traderOne, buy, res, quantity, price));
                } else {
                    auctionByType[res].addRequest(new Request(traderTwo, buy, res, quantity, price));
                }
            }
        }
	}

    private void parseResources() {
        // Get the file into the document.
        TextAsset _xml = Resources.Load<TextAsset>("Scripts/resources.xml");

        //XDocument root = XDocument.Load("Scripts/resources.xml");
        XmlDocument xmDoc = new XmlDocument();
        xmDoc.Load("Assets/Resources/Scripts/resources.xml");

        var rootXml = xmDoc.DocumentElement.GetEnumerator();
        foreach(XmlNode xmlNode in xmDoc.DocumentElement) {
            if (xmlNode == null || xmlNode.Attributes == null) {
                break;
            }
            Resource res = new Resource(xmlNode.Name);
            if (xmlNode.Attributes.Count > 0) {
                
            }
            //Debug.Log(xmlNode.Name + "=======================");
            foreach (XmlNode n in xmlNode.ChildNodes) {
                //Debug.Log(n.Name);
                //Debug.Log(n.Attributes.ToString());
                switch (n.Name) {
                    case "location":
                        res.location = n.InnerText;
                        break;
                    case "need":
                        res.needs.Add(n.Attributes["type"].Value, int.Parse(n.Attributes["amount"].Value));
                        break;
                    case "output":
                        res.output = float.Parse(n.InnerText);
                        break;
                    case "time":
                        res.time = float.Parse(n.InnerText);
                        break;
                    case "byproduct":
                        res.byproducts.Add(n.Attributes["type"].Value, new Dictionary<string, float>());
                        res.byproducts[n.Attributes["type"].Value].Add("chance", float.Parse(n.Attributes["chance"].Value));
                        res.byproducts[n.Attributes["type"].Value].Add("amount", float.Parse(n.Attributes["amount"].Value));
                        break;
                    default:
                        break;
                }
            }
            //Debug.Log(res.ToString());
            resources.Add(res.name, res);
            
            Debug.Log("=============================");
        }
        
    }

    // Update is called once per frame
    void Update () {
        //by default only updates every 5 time units.

        if (points > updateSpeed) {
            int count = auctions.Count;
            for (var i = 0; i < count; i++) auctions[i].Update(points);
        }
        foreach (Resource res in resources.Values)
        {
            Auction auc = new Auction(res, true);
        }
    }

    public void gainActionPoints(int points) {
        Debug.Log("Economy gained points");
        this.points += points;
        //by default only updates every 5 time units.
        if (this.points > updateSpeed) {
            int count = auctions.Count;
            for (var i = 0; i < count; i++)
                auctions[i].Update(1);
            this.points -= updateSpeed;
        }
    }
        foreach(Business b in businessList)
        {
            b.gainActionPoints(points);
        }
    }

    private void createBusinesses()
    {
        //Create Businesses
        businessList = new List<Business>();
        businessList.Add(new Business("Emily's Ripe Peaches", resources["raw"]));
        //businessList.Add(new Business("Emily's Sour Plums", resources["luxury"]));
        //businessList.Add(new Business("Emily's Spicy Nachos", resources["processed"]));
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
