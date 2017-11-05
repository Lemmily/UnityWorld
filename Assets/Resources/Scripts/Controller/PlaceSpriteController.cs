using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSpriteController : MonoBehaviour {


    Dictionary<IPlace, GameObject> placeToGameObject;
    public Sprite citySprite;
    public Sprite dungeonSprite;
    void Start () {
        placeToGameObject = new Dictionary<IPlace, GameObject>();
        citySprite = ResourceLoader.GetCitySprite();
        dungeonSprite = ResourceLoader.GetDungeonSprite();
    }


    public void Update()
    {
        foreach (Dungeon dun in WorldController.Instance.World.GetDungeons()) {
            if (placeToGameObject.ContainsKey(dun)) {
                GameObject go = placeToGameObject[dun];
                SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                sr.sprite = dungeonSprite;
                go.transform.localPosition = new Vector3(dun.X, dun.Y, 0);
                sr.sortingLayerName = "TerrainObjects";
            }
            else {
                GameObject go = new GameObject();
                go.transform.SetParent(this.transform);
                SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = dungeonSprite;
                go.transform.localPosition = new Vector3(dun.X, dun.Y, 0);
                sr.sortingLayerName = "TerrainObjects";
                placeToGameObject.Add(dun, go);
            }
        }
        foreach (City city in WorldController.Instance.World.GetCities()) {
            if (placeToGameObject.ContainsKey(city)) {
                GameObject go = placeToGameObject[city];
                SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                sr.sprite = citySprite;
                go.transform.localPosition = new Vector3(city.X, city.Y, 0);
                sr.sortingLayerName = "TerrainObjects";
            }
            else {
                GameObject go = new GameObject();
                go.transform.SetParent(this.transform);
                SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = citySprite;
                go.transform.localPosition = new Vector3(city.X, city.Y, 0);
                sr.sortingLayerName = "TerrainObjects";
                placeToGameObject.Add(city, go);
            }
        }
    }
}
