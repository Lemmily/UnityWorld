using UnityEngine;
using System.Collections.Generic;

public class Business{
    private string businessName = "";
    private Economy.Resource productionResource;
    private int points;
    private int updateSpeed = 5;
    private Dictionary<Economy.Resource,double> resources = null;   //Dictionary of Resources held by business, <name,quantity>

    public Business(string name, Economy.Resource res)
    {
        create(name, res);
    }

    // Use this for initialization
    //void Start()
    //{
    //    points = 0;
    //    create();
    //}

    // Creation of a Business entity - default params
    void create(string name, Economy.Resource res)
    {
        resources = new Dictionary<Economy.Resource, double>();
        businessName = name;
        productionResource = res;
        resources.Add(res,0.0);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}



    public void gainActionPoints(int points)
    {
        Debug.Log(businessName+ " gained points");
        this.points += points;
    }
}

