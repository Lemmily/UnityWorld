﻿using UnityEngine;
using System.Collections;

public class BaseAgent {
    int points = 0;
    public string name = "Player";
    //TODO: this doesn't need to be a monobehaviour.
    // will split this into an "Agent" who represents the physical "body" which is disconnected from 
    // it's actual "Brain" eg, Trader, Hero etc. 
    // think of how things are done in procupin project (Quill18) will have a character controller etc.


    public void gainActionPoints(int points) {
        Debug.Log("Agent:- I gained points");
        this.points += points;
    }
}
