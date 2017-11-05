using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;
    public Camera cam;
    private bool lockedToPlayer = true;

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
            return PlayerController.Instance.player;
        }
    }

    // Use this for initialization
    void Start () {
        Instance = this;
        cam = Camera.main;

        player.RegisterPlayerMovedCallback(UpdateCamera);
        player.cbPlayerMoved(player);
	}
	
	// Update is called once per frame
	void Update () {
    }


    public void UpdateCamera(Player player) {
        Vector3 start = cam.ScreenToWorldPoint(Vector3.zero);
        Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        int screenWidthInWorld = (int)(end.x - start.x);
        int screenHeightInWorld = (int)(end.y - start.y);


        if (lockedToPlayer) {
            start = new Vector3(player.x - screenWidthInWorld / 2, player.y - screenHeightInWorld / 2);
        }


        if (start.x < 0)
            start.x = 0;
        else if (start.x > world.Width - screenWidthInWorld)
            start.x = world.Width - screenWidthInWorld;

        if (start.y < 0)
            start.y = 0;
        else if (start.y > world.Height - screenHeightInWorld)
            start.y = world.Height - screenHeightInWorld;

        //if (end.x < 0)
        //    end.x = 0;
        //else if (end.x > world.Width - Screen.width)
        //    end.x = world.Width;
        //if (end.y < 0)
        //    end.y = 0;
        //else if (start.y > world.Height - Screen.height)
        //    start.y = world.Height - Screen.height;
        Debug.Log("Camera moved to (start coords)" + start);
        cam.transform.localPosition = start + new Vector3(screenWidthInWorld / 2, screenHeightInWorld / 2, -1);
    }

    public void UpdateCamera(Camera camera) {

    }



}
