using UnityEngine;
using System.Collections;
using System;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}
public class PlayerController : MonoBehaviour {

    World world
    {
        get
        {
            return WorldController.Instance.world;
        }
    }

    public static PlayerController Instance;
    public Player player;
    public GameObject player_go;

	void Start () {
        Instance = this;

        player = new Player();

        player_go = new GameObject();
        SpriteRenderer sr = player_go.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Tiles";
        sr.sprite = ResourceLoader.GetPlayerSprite(player);

        player.RegisterPlayerMovedCallback(DrawPlayer);
	}

    // Update is called once per frame
    void Update () {

    }

    private void DrawPlayer(Player player) {
        //drew player
        player_go.transform.localPosition = new Vector3(player.x, player.y, 0);

        //not sure this is really needed unless there's a sprite change??
        SpriteRenderer sr = player_go.GetComponent<SpriteRenderer>();
        sr.sprite = ResourceLoader.GetPlayerSprite(player);
        sr.sortingLayerName = "Characters";
    }

    public bool IsValidMove(Direction dir) {
        switch (dir) {
            case Direction.Up:
                if (world.GetTileAt(player.x, player.y + 1).IsEnterable()) {
                    return true;
                }
                break;
            case Direction.Down:
                if (world.GetTileAt(player.x, player.y - 1).IsEnterable()) {
                    return true;
                }
                break;
            case Direction.Left:
                if (world.GetTileAt(player.x - 1, player.y).IsEnterable()) {
                    return true;
                }
                break;
            case Direction.Right:
                if (world.GetTileAt(player.x + 1, player.y).IsEnterable()) {
                    return true;
                }
                break;
            default:
                return false;
        }
        return false;
    }
    
}
