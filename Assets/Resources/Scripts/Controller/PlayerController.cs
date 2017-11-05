using UnityEngine;
using System.Collections;
using System;

public enum Direction
{
    North,
    East,
    South,
    West,
    Down,
    Up
}
public class PlayerController : MonoBehaviour {

    World world
    {
        get
        {
            return WorldController.Instance.World;
        }
    }

    public static PlayerController Instance;
    public Player player;
    public Inventory inventory;
    public GameObject player_go;
    public bool lockActions;

    void Start () {
        Instance = this;

        player = new Player();
        player.Move(world.Width / 2, world.Height / 2);
        player_go = new GameObject();
        SpriteRenderer sr = player_go.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Tiles";
        sr.sprite = ResourceLoader.GetPlayerSprite(player);
        inventory = new Inventory();
        player.RegisterPlayerMovedCallback(DrawPlayer);
        DrawPlayer(player);
	}

    // Update is called once per frame
    void Update () {

    }

    private void DrawPlayer(Player player) {
        //drew player
        player_go.transform.localPosition = new Vector3(player.x, player.y, 0);
        player_go.name = "Player";
        //not sure this is really needed unless there's a sprite change??
        SpriteRenderer sr = player_go.GetComponent<SpriteRenderer>();
        sr.sprite = ResourceLoader.GetPlayerSprite(player);
        sr.sortingLayerName = "Characters";
    }

    public bool IsValidMove(Direction dir) {
        if (lockActions) {
            return false; //TODO: make this less gimpy. got to include ALL actions. not just move.
        }
        switch (dir) {

            //orthogonal
            case Direction.North:
                if (world.GetTileAt(player.x, player.y + 1).IsEnterable()) {
                    return true;
                }
                break;
            case Direction.South:
                if (world.GetTileAt(player.x, player.y - 1).IsEnterable()) {
                    return true;
                }
                break;
            case Direction.West:
                if (world.GetTileAt(player.x - 1, player.y).IsEnterable()) {
                    return true;
                }
                break;
            case Direction.East:
                if (world.GetTileAt(player.x + 1, player.y).IsEnterable()) {
                    return true;
                }
                break;
            //TODO: diagonals

            //3D planes.
            case Direction.Down:
                IPlace place = world.CheckForPlace(player.x, player.y);
                if (place != null) {
                    PlaceInteractionController.Instance.Place = place;
                    lockActions = true;
                    return true;
                }

                //switch (place.Type) {
                //    case World.PlaceType.City:
                //        break;
                //    case World.PlaceType.Dungeon:
                //        Dungeon dun = world.CheckForDungeon(player.x, player.y);
                //        if (dun != null) {
                //            //DungeonInteractionUIController.Instance.Dungeon = dun;
                //            PlaceInteractionController.Instance.Place = dun;
                //        }
                //        break;
                //    case World.PlaceType.Farm:
                //        break;
                //    default:
                //        break;
                //}

                break;
            //case Direction.Up:

            //    break;

            default:
                return false;
        }
        return false;
    }

    public bool Move(int dx, int dy) {

        player.Move(dx, dy);

        return false;
    }


    public string GetPlayerInfo()
    {
        string msg = "";

        msg += "Location: - " + player.x + "," + player.y;
        msg += "\nSkill Level:- " + player.Skill;

        msg += "\n";

        msg += "Inventory:- " + inventory.items;
        msg += "Monies:- " + inventory.money;


        return msg;
    }
    
}
