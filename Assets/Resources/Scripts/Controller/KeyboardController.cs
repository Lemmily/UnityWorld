using UnityEngine;
using System.Collections;
using System;

public class KeyboardController : MonoBehaviour {


    public static KeyboardController Instance;
    PlayerController p_controller
    {
        get
        {
            return PlayerController.Instance;
        }
    }


    World world
    {
        get
        {
            return WorldController.Instance.World;
        }
    }
    Player player
    {
        get
        {
            return p_controller.player;
        }
    }
	// Use this for initialization
	void Start () {
        Instance = this;
	}

	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) {
            //check valid move
            if (p_controller.IsValidMove(Direction.North)) {
                DoMove(Direction.North);
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
            if (p_controller.IsValidMove(Direction.East)) {
                DoMove(Direction.East);
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
            if (p_controller.IsValidMove(Direction.South)) {
                DoMove(Direction.South);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
            if (p_controller.IsValidMove(Direction.West)) {
                DoMove(Direction.West);
            }

        } 
        else if ( Input.GetKeyUp(KeyCode.G)) {
            if (p_controller.IsValidMove(Direction.Down)) {
                Debug.Log("WEeeeee");
                //told the ui to do stuff in player controller.
            }
            Debug.Log("tried to go down");
        }
        else if (Input.GetKeyUp(KeyCode.Less)) {
            if (p_controller.IsValidMove(Direction.Up)) {

            }
            Debug.Log("tried to go down");
        }

    }

    public void DoMove(Direction dir) {
        //tell camera to move.
        //tellPlayer to move
        int dx, dy;
        switch (dir) {
            case Direction.North:
                dx = 0;
                dy = 1;
                break;
            case Direction.South:
                dx = 0;
                dy = -1;
                break;
            case Direction.West:
                dx = -1;
                dy = 0;
                break;
            case Direction.East:
                dx = 1;
                dy = 0;
                break;
            default:
                dx = 0;
                dy = 0;
                break;
        }
        p_controller.Move(dx, dy);
        //Camera.main.transform.Translate(dx, dy, 0);
        //TODO: constrain the camera to map. (need to think about ..
        // ..map potentially being smaller that the screen)
        Debug.Log("tried to move " + dir);
    }




}
