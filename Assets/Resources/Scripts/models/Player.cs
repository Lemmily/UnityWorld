using UnityEngine;
using System.Collections;
using System;

public class Player : Agent {


    private int skill = 2; //TODO: un-hardcode this.
    public int Skill
    {
        get
        {
            return skill;
        }

        set
        {
            skill = value;
        }
    }

    public Action<Player> cbPlayerMoved;
    public Player() {
        X = 0;
        Y = 0;
    }

    public Player(int x, int y) {
        this.X = x;
        this.Y = y;
    }

    public void Move(int dx, int dy) {
        //no checks - they are done PRIOR to this!
        Debug.Log("Player moved from location: " + X + "," + Y);
        X += dx;
        Y += dy;
        if(cbPlayerMoved != null)
            cbPlayerMoved(this);
        Debug.Log("Player moved to location: " + X + "," + Y);
    }

    
    public void RegisterPlayerMovedCallback(Action<Player> callback) {
        cbPlayerMoved += callback;
    }

    public void UnRegisterPlayerMovedCallback(Action<Player> callback) {
        cbPlayerMoved -= callback;
    }


}
