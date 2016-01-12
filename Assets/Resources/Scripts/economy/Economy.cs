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
    private Dictionary<String, Resource> resources;
    // Use this for initialization
    void Start () {
        points = 0;

        resources = new Dictionary<string, Resource>();
        parseResources();

        auctionByType = new Dictionary<Resource, Auction>();
        auctions = new List<Auction>();
        foreach (Resource res in resources.Values) {
            Auction auc = new Auction(res, true);
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
            if (xmlNode == null) {
                continue;
            }
            Resource res = new Resource(xmlNode.Name);
            if (xmlNode.Attributes.Count > 0) {

            }
            Debug.Log(xmlNode.Name + "=======================");
            foreach (XmlNode n in xmlNode.ChildNodes) {
                Debug.Log(n.Name);
                Debug.Log(n.Attributes.ToString());
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
            Debug.Log(res.ToString());
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
