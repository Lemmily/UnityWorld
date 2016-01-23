using UnityEngine;
using System.Collections.Generic;

public class Resource {
    public string name;
    public float output;
    public float time;
    //public List<Need> needs;
    public Dictionary<string, int> needs;
    public string location;
    public Dictionary<string, Dictionary<string, float>> byproducts;

    public Resource(string name,float output,float time, Dictionary<string, int> needs=null) {
        this.name = name;
        this.output = output;
        this.time = time;
        if (needs == null) this.needs = new Dictionary<string, int>();
        else this.needs = needs;
        this.byproducts = new Dictionary<string, Dictionary<string, float>>();
    }

    public Resource (string name) {
        this.name = name;
        this.byproducts = new Dictionary<string, Dictionary<string, float>>();
        this.needs = new Dictionary<string, int>();
    }

    //public class Need
    //{
    //    private int amount;
    //    private string type;

    //    public Need (string type, int amount) {
    //        this.type = type;
    //        this.amount = amount;
    //    }
    //}
    
    override public string ToString() {
        return name + ": " + output + " in " + time + " using: " + needs;
    }
}