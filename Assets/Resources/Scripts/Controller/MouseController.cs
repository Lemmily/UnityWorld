using UnityEngine;
using System.Collections;
using System;

public class MouseController : MonoBehaviour
{

    public static MouseController Instance;

    // The world-position of the mouse last frame.
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    float lastOrthoSize;
    private Action cbMouseScrolled;

    public void RegisterMouseScrolledCallback(Action callback) {
        cbMouseScrolled += callback;
    }

    public void UnRegisterPlayerMovedCallback(Action callback) {
        cbMouseScrolled -= callback;
    }


    void Start() {
        //Camera.main.transform.localPosition = new Vector3(
        //    WorldController.Instance.world.Width / 2,
        //    WorldController.Instance.world.Height / 2,
        //    0);
        Instance = this;
    }



    void Update() {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        UpdateCameraMovement();


        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
        lastOrthoSize = Camera.main.orthographicSize;
    }

    private void UpdateCameraMovement() {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) { //right or middle mouse.
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
            cbMouseScrolled();
        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 50f);
        if (lastOrthoSize != Camera.main.orthographicSize) {
            Debug.Log("Mouse Scrolled enough to redraw!");
            cbMouseScrolled();
        }
    }


    public Vector3 GetMousePosition() {
        return currFramePosition;
    }

    public MapTile GetMouseOverTile() {
        /*		return WorldController.Instance.world.GetTileAt(
                    Mathf.FloorToInt(currFramePosition.x), 
                    Mathf.FloorToInt(currFramePosition.y)
                );*/

        return WorldController.Instance.GetTileAtWorldCoord(currFramePosition);
    }
}
