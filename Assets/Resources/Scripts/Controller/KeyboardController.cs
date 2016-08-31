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
            return WorldController.Instance.world;
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
            if (p_controller.IsValidMove(Direction.Up)) {
                DoMove(Direction.Up);
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
            if (p_controller.IsValidMove(Direction.Right)) {
                DoMove(Direction.Right);
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
            if (p_controller.IsValidMove(Direction.Down)) {
                DoMove(Direction.Down);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
            if (p_controller.IsValidMove(Direction.Left)) {
                DoMove(Direction.Left);
            }

        }
    }

    public void DoMove(Direction dir) {
        //tell camera to move.
        //tellPlayer to move
        int dx, dy;
        switch (dir) {
            case Direction.Up:
                dx = 0;
                dy = 1;
                break;
            case Direction.Down:
                dx = 0;
                dy = -1;
                break;
            case Direction.Left:
                dx = -1;
                dy = 0;
                break;
            case Direction.Right:
                dx = 1;
                dy = 0;
                break;
            default:
                break;
        }
        player.Move(dx, dy);
        Camera.main.transform.Translate(dx, dy, 0);
        //TODO: constrain the camera to map. (need to think about ..
        // ..map potentially being smaller that the screen)
        Debug.Log("tried to move " + dir);
    }


}
