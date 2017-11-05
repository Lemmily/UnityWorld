using UnityEngine;
using System.Collections;
using System;

public class Player : Agent {

    public int x
    {
        get;
        set;
    }
    public int y
    {
        get;
        set;
    }

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
        x = 0;
        y = 0;
    }

    public Player(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public void Move(int dx, int dy) {
        //no checks - they are done PRIOR to this!
        Debug.Log("Player moved from location: " + x + "," + y);
        x += dx;
        y += dy;
        if(cbPlayerMoved != null)
            cbPlayerMoved(this);
        Debug.Log("Player moved to location: " + x + "," + y);
    }

    
    public void RegisterPlayerMovedCallback(Action<Player> callback) {
        cbPlayerMoved += callback;
    }

    public void UnRegisterPlayerMovedCallback(Action<Player> callback) {
        cbPlayerMoved -= callback;
    }


}
