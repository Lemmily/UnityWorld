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
    
    public Text description;
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

            if (value == null)
                return;

            description.text = place.GetDescriptorText();

            if (place.Type == World.PlaceType.City) {
                City city = (City)place;
                LoadCityInteractions(city);
            } else if (place.Type == World.PlaceType.Dungeon) {
                Dungeon dungeon = (Dungeon)place;
                LoadDungeonInteractions(dungeon);

            } else if (place.Type == World.PlaceType.Farm) {
                Farm farm = (Farm)Place;
                LoadFarmInteractions(farm);
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
        description = ui.GetComponentInChildren<Text>();
        buttons = new Dictionary<World.PlaceType, List<Button>>();
        activeButtons = new List<Button>();
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

        }
        else {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            

            ui = GameObject.Instantiate(pf_panel);
            ui.gameObject.transform.SetParent(canvas.transform);
            GameObject btn_go = GameObject.Instantiate(pf_button);

            btn_go.transform.SetParent(ui.transform);

            Button btn_challenge = btn_go.GetComponentInChildren<Button>();
            Text text = btn_challenge.GetComponentInChildren<Text>();
            text.text = "Challenge Dungeon";
            text.font = font;
            btn_challenge.onClick.AddListener(() => dungeon.challengeDungeon(WorldController.Instance.Player));

            //go.transform.SetParent(ui.transform);
            //Button btn_challenge = go.AddComponent<Button>();
            //Text text = btn_challenge.gameObject.AddComponent<Text>();
            //text.text = "Challenge Dungeon";
            //text.font = font;
            //btn_challenge.onClick.AddListener(() => dungeon.challengeDungeon(WorldController.Instance.Player));
            //go.SetActive(true);
            //go.transform.localPosition = new Vector3(30, 30);
            //activeButtons.Add(btn_challenge);

            //GameObject getItemsGo = new GameObject();
            //getItemsGo.transform.SetParent(ui.transform);
            //Button btn_items = getItemsGo.AddComponent<Button>();
            //text = btn_items.gameObject.AddComponent<Text>();
            //text.text = "get items";
            //text.font = font;
            //btn_challenge.onClick.AddListener(() => dungeon.GetItems(WorldController.Instance.Player, null));
            //activeButtons.Add(btn_items);



            buttons.Add(World.PlaceType.Dungeon, new List<Button> { btn_challenge, });
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