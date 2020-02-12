using UnityEngine;
using System.Collections;
using System;

public class Hero : BaseAgent
{

    public int points;
    private int speed;
    AgentAction action;


    void Start() {

    }

    //overrides agent gain action.
    public new void gainActionPoints(int points) {
        this.points += points;

        //move based on how fast the character is. so higher the dexterity/speed, the fewer points between moves.
        if (this.points > speed) {
            //do decision - am I already busy? if so, skip (or check action still possible?)
            decideAction();


            //do update
            doAction();
        }
    }

    private void doAction() {
        action = new AgentIdle(this);
    }

    private void decideAction() {
        
    }
}
