using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
    int points = 0;
    Trader trader;
	// Use this for initialization
	void Start () {
        trader = this.gameObject.AddComponent<Trader>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void gainActionPoints(int points) {
        Debug.Log("I gained points");
        this.points += points;
    }
}
