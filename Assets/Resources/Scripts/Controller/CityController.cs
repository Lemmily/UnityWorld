using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityController : MonoBehaviour {

    public static CityController Instance;

    private List<City> cities;


    World world
    {
        get
        {
            return WorldController.Instance.World;
        }
    }
    
    void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
