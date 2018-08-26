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
                Debug.Log("PlaceInteractionController:- Setting place to null, adn trying to act on it!");
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
        if (ui != null) {
            HideUI();
        }
        CreateCityPanel(city);
        Debug.Log("Creating City Panel");
    }

    private void LoadFarmInteractions(Farm farm)
    {
        throw new NotImplementedException();
    }

    private void LoadDungeonInteractions(Dungeon dungeon)
    {
        // store these "getItems" and "challenge" as buttons somewhere, 
        // maybe like the furniture functions in 2d_simple_game
        if (ui != null) {
            HideUI();
        }
        
        CreateDungeonPanel(dungeon);


        if (dungeon.beaten) {
            //Get each of the buttons.

            foreach (Button btn in ui.GetComponentsInChildren<Button>()) {
                switch (btn.gameObject.name) {
                    case "Challenge Dungeon":
                        btn.gameObject.SetActive(false);
                        break;
                    case "Get Items":
                        btn.gameObject.SetActive(true);
                        break;
                    case "Exit Dungeon":
                        btn.gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        } else {
            foreach (Button btn in ui.GetComponentsInChildren<Button>()) {
                switch (btn.gameObject.name) {
                    case "Challenge Dungeon":
                        btn.gameObject.SetActive(true);
                        break;
                    case "Get Items":
                        btn.gameObject.SetActive(false);
                        break;
                    case "Exit Dungeon":
                        btn.gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            }

        }
    }


    private void CreateDungeonPanel(Dungeon dungeon)
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();

        ui = GameObject.Instantiate(pf_panel);
        ui.gameObject.transform.SetParent(canvas.transform);
        RectTransform rTransform = ui.GetComponent<RectTransform>();
        //rTransform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0,0,0));
        rTransform.localPosition = new Vector3(rTransform.localPosition.x, 0, 0);

        //Text text = ui.AddComponent<Text>();
        //text.text = description;
        GameObject btn_go = GameObject.Instantiate(pf_button);
        btn_go.name = "Challenge Dungeon";
        btn_go.transform.SetParent(ui.transform);

        Button btn_challenge = btn_go.GetComponentInChildren<Button>();
        Text text = btn_challenge.GetComponentInChildren<Text>();
        text.text = "Challenge Dungeon";
        text.font = font;
        btn_challenge.onClick.AddListener(() => {
            dungeon.ChallengeDungeon(WorldController.Instance.Player);
            LoadDungeonInteractions(dungeon);
        });

        btn_go = GameObject.Instantiate(pf_button);
        btn_go.transform.SetParent(ui.transform);
        btn_go.name = "Get Items";
        Button btn_get_items = btn_go.GetComponentInChildren<Button>();
        text = btn_get_items.GetComponentInChildren<Text>();
        text.text = "Get Items";
        text.font = font;
        btn_get_items.onClick.AddListener(() => {
            dungeon.GetItems(PlayerController.Instance.player, PlayerController.Instance.inventory);
            //TODO: make a pop up for when allthe items have been picked up and there are none left?
            Debug.Log("Got those items!");
        }); 

        btn_go = GameObject.Instantiate(pf_button);
        btn_go.transform.SetParent(ui.transform);
        btn_go.name = "Exit Dungeon";
        Button btn_exit = btn_go.GetComponentInChildren<Button>();
        text = btn_exit.GetComponentInChildren<Text>();
        text.text = "Exit Dungeon";
        text.font = font;
        btn_exit.onClick.AddListener(() =>
        {
            Debug.Log("Exited the dungeon:- Hiiiii");
            PlayerController.Instance.lockActions = false;
            place = null;
            HideUI();
        });
    }


    private void CreateCityPanel(City city)
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();

        ui = GameObject.Instantiate(pf_panel);
        ui.gameObject.transform.SetParent(canvas.transform);
        RectTransform rTransform = ui.GetComponent<RectTransform>();
        //rTransform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0,0,0));
        rTransform.localPosition = new Vector3(rTransform.localPosition.x, 0, 0);

        //Text text = ui.AddComponent<Text>();
        //text.text = description;
        GameObject btn_go = GameObject.Instantiate(pf_button);
        btn_go.name = "Sell All Items";
        btn_go.transform.SetParent(ui.transform);

        Button btn_sell_items = btn_go.GetComponentInChildren<Button>();
        Text text = btn_sell_items.GetComponentInChildren<Text>();
        text.text = "Sell All Items";
        text.font = font;
        btn_sell_items.onClick.AddListener(() => {
            int monies = city.BuyAllEquipmentFrom(PlayerController.Instance.inventory);
            PlayerController.Instance.inventory.money += monies;
            //LoadCityInteractions(city);
        });
        

        btn_go = GameObject.Instantiate(pf_button);
        btn_go.transform.SetParent(ui.transform);
        btn_go.name = "Exit City";
        Button btn_exit = btn_go.GetComponentInChildren<Button>();
        text = btn_exit.GetComponentInChildren<Text>();
        text.text = "Exit City";
        text.font = font;
        btn_exit.onClick.AddListener(() =>
        {
            Debug.Log("Exited the City:- weee");
            PlayerController.Instance.lockActions = false;
            place = null;
            HideUI();
        });
    }

    /// <summary>
    /// "Hide" the UI by completely destroying it.
    /// </summary>
     
    private void HideUI()
    {
        if (ui != null) {
            Debug.Log("Hiding the UI");
            ui.SetActive(false);
            GameObject.Destroy(ui);
        }

    }

}