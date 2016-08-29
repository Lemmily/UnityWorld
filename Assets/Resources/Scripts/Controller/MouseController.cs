using UnityEngine;
using System.Collections;
using System;

public class MouseController : MonoBehaviour
{

    // The world-position of the mouse last frame.
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    void Start() {
        //Camera.main.transform.localPosition = new Vector3(
        //    WorldController.Instance.world.Width / 2,
        //    WorldController.Instance.world.Height / 2,
        //    0);
    }



    void Update() {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        UpdateCameraMovement();


        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    private void UpdateCameraMovement() {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) { //right or middle mouse.
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 50f);
    }


    public Vector3 GetMousePosition() {
        return currFramePosition;
    }

    public Tile GetMouseOverTile() {
        /*		return WorldController.Instance.world.GetTileAt(
                    Mathf.FloorToInt(currFramePosition.x), 
                    Mathf.FloorToInt(currFramePosition.y)
                );*/

        return WorldController.Instance.GetTileAtWorldCoord(currFramePosition);
    }
}
