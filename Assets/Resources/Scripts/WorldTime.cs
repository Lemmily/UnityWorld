using UnityEngine;

public class WorldTime : MonoBehaviour
{
    int turnsFromBeginning = 0;
    float timePassed= 0;
    private bool waitForPlayer;
    private float updateSpeed = 1f;

    void Start() {
        waitForPlayer = false;
    }

    void Update() {
        if ( ! waitForPlayer) {
            timePassed += Time.deltaTime;
            if (timePassed > updateSpeed) {
                timePassed = 0;
                advanceTime(1);
            }
            
        }
    }

    //date tracking need to be in here too.



    // "time" here is an abstract measure. maybe "turn" is a better word. 
    public void advanceTime(int time) {
        BroadcastMessage("gainActionPoints", time);
    }
}