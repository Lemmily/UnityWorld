using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

/// <summary>
///  This controls the little pop up window when you "enter" a 
///  place on the world map.
/// </summary>
public class PlaceInteractionController : MonoBehaviour
{

    public static PlaceInteractionController Instance;
    
    public string description;
    public Font font;
    public GameObject ui;

    public GameObject pf_panel;
    public GameObject pf_button;



    private IPlace place;
    public IPlace Place
    {
        get
        {
            return place;
        }
        set
        {
            place = value;

            if (place == null) {
                HideUI();
                return;
            }

            description = place.GetDescriptorText();

            if (place.Type == World.PlaceType.City) {
                City city = (City)place;
                LoadCityInteractions(city);
                Debug.Log("PlaceInteractionController:- Set the place to a city: " + city);
            } else if (place.Type == World.PlaceType.Dungeon) {
                Dungeon dungeon = (Dungeon)place;
                LoadDungeonInteractions(dungeon);
                Debug.Log("PlaceInteractionController:- Set the place to a dungeon: " + dungeon);

            } else if (place.Type == World.PlaceType.Farm) {
                Farm farm = (Farm)Place;
                LoadFarmInteractions(farm);
                Debug.Log("PlaceInteractionController:- Set the place to a farm: " + farm);
            } else {
                Debug.LogError("Somehow got a place type that don't exist.");
            }


        }
    }

    private void HideUI()
    {
        Debug.Log("Hiding the UI");
        if (ui != null)
            ui.SetActive(false);
    }

    public Dictionary<World.PlaceType, List<Button>> buttons;
    public List<Button> activeButtons;

    public void Start()
    {
        Instance = this;
        buttons = new Dictionary<World.PlaceType, List<Button>>();
        activeButtons = new List<Button>();
    }


    public void Update()
    {
        //is this an appropriate place to keep checking stuff?
    }


    private void LoadCityInteractions(City city)
    {
        throw new NotImplementedException();
    }

    private void LoadFarmInteractions(Farm farm)
    {
        throw new NotImplementedException();
    }

    private void LoadDungeonInteractions(Dungeon dungeon)
    {
        // store these "getItems" and "challenge" as buttons somewhere, 
        // maybe like the furniture functions in 2d_simple_game

        if (buttons.ContainsKey(World.PlaceType.Dungeon)) {
            //I think I'm going to recreate the buttons everytime atm.



        }
        else {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();

            ui = GameObject.Instantiate(pf_panel);
            ui.gameObject.transform.SetParent(canvas.transform);
            RectTransform rTransform = ui.GetComponent<RectTransform>();
            //rTransform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0,0,0));
            rTransform.localPosition = new Vector3(rTransform.localPosition.x, 0, 0);

            //Text text = ui.AddComponent<Text>();
            //text.text = description;
            GameObject btn_go = GameObject.Instantiate(pf_button);

            btn_go.transform.SetParent(ui.transform);

            Button btn_challenge = btn_go.GetComponentInChildren<Button>();
            Text text = btn_challenge.GetComponentInChildren<Text>();
            text.text = "Challenge Dungeon";
            text.font = font;
            btn_challenge.onClick.AddListener(() => dungeon.ChallengeDungeon(WorldController.Instance.Player));

            btn_go = GameObject.Instantiate(pf_button);
            btn_go.transform.SetParent(ui.transform);
            Button btn_get_items = btn_go.GetComponentInChildren<Button>();
            text = btn_get_items.GetComponentInChildren<Text>();
            text.text = "Get Items";
            text.font = font;
            btn_get_items.onClick.AddListener(() => dungeon.GetItems(WorldController.Instance.Player, null));

            btn_go = GameObject.Instantiate(pf_button);
            btn_go.transform.SetParent(ui.transform);
            Button btn_exit = btn_go.GetComponentInChildren<Button>();
            text = btn_exit.GetComponentInChildren<Text>();
            text.text = "Exit Dungeon";
            text.font = font;
            btn_exit.onClick.AddListener(() =>
            {
                Debug.Log("Exited the dungeon");
                PlayerController.Instance.lockActions = false;
                place = null;
            });

            //go.transform.SetParent(ui.transform);
            //Button btn_challenge = go.AddComponent<Button>();
            //Text text = btn_challenge.gameObject.AddComponent<Text>();
            //text.text = "Challenge Dungeon";
            //text.font = font;



            buttons.Add(World.PlaceType.Dungeon, new List<Button> { btn_challenge, btn_get_items, btn_exit});
        }
        if (dungeon.beaten) {
        //    getItems.SetActive(true);
        //    challenge.SetActive(false);
        //} else {
        //    getItems.SetActive(false);
        //    challenge.SetActive(true);
        }
    }
    
}