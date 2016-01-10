using UnityEngine;
using System.Collections.Generic;

public class Resource {
    public string name;
    public float output;
    public float time;
    //public List<Need> needs;
    public Dictionary<string, int> needs;
    
    public Resource(string name, float output,float time, Dictionary<string, int> needs=null) {
        this.name = name;
        this.output = output;
        this.time = time;
        this.needs = needs;
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

}