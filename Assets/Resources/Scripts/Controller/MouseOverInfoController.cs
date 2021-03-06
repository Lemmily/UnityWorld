﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseOverInfoController : MonoBehaviour {


    Text theText;
    MouseController mouseController;
	// Use this for initialization
	void Start () {
        theText = GetComponent<Text>();

        if(theText == null) {
            Debug.LogError("MouseOverTileTypeController has no text component");
            this.enabled = false;
            return;
        }

        mouseController = GameObject.FindObjectOfType<MouseController>();
        if(mouseController == null) {
            Debug.LogError("There's no mouse controller....");
            return;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        OwnTile t = mouseController.GetMouseOverTile();
        if (t == null)
            return;
        
        string text = "Tile type: " + t.tileType +
            "\nLocation: " + t.x + "," + t.y;

        if (WorldController.Instance.World.CheckForPlace(t)) {
            text += "\nPlace: " + WorldController.Instance.World.GetPlace(t).GetName();
        }

        theText.text = text;
	}
}
