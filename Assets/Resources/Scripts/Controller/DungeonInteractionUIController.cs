using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

<<<<<<< HEAD
public class DungeonInteractionUIController : PlaceInteractionController {

    public static DungeonInteractionUIController Instance;

    public Text description;
    public GameObject ui;
=======
public class DungeonInteractionUIController : PlaceInteractionController
{

    public static DungeonInteractionUIController Instance;
>>>>>>> bcb9448eb21853f8d01a9e1dfaec69e3c19b12db
    public GameObject challenge;
    public GameObject getItems;
    public GameObject exit;

    Dungeon dungeon;

    public Dungeon Dungeon
    {
        get
        {
            return dungeon;
        }
        set
        {
            dungeon = value;

            if (value == null)
                return;

            description.text = dungeon.GetDescriptorText();

            if(dungeon.beaten) {
                getItems.SetActive(true);
                challenge.SetActive(false);
            } else {
                getItems.SetActive(false);
                challenge.SetActive(true);
            }
        }
    }


	void Start () {
        Instance = this;
        description = ui.GetComponentInChildren<Text>();
	}

	void Update () {
	    if (dungeon == null && ui.activeSelf == true) {
            ui.SetActive(false);
        } else if (dungeon != null && ui.activeSelf == false) {
            ui.SetActive(true);
        }
    }


    public void ChallengeDungeon() {
        Debug.Log("Challenged the dungeon!");

        challenge.SetActive(false);
        getItems.SetActive(true);
    }

    public void GetItems() {
        Debug.Log("Gathered up all the items!");
        getItems.SetActive(false);
        //challenge.SetActive(true);
    }

    public void ExitDungeon() {
        Debug.Log("Exited the dungeon");
        PlayerController.Instance.lockActions = false;
        Dungeon = null;
    }


}
