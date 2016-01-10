using UnityEngine;

public class WorldTime : MonoBehaviour
{
    float timePassed= 0;
    private bool waitForPlayer;

    void Start() {
        waitForPlayer = false;
    }

    void Update() {
        if ( ! waitForPlayer) {
            timePassed += Time.deltaTime;
            if (timePassed > 1f) {
                timePassed = 0;
                advanceTime(1);
            }
            
        }
    }


    // "time" here is an abstract measure. 
    public void advanceTime(int time) {
        BroadcastMessage("gainActionPoints", time);
    }
}