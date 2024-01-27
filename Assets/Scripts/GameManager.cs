using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringItemDictionary : SerializableDictionary<string, Item> { }

public class GameManager : MonoBehaviour
{
    public ItemMono prefab;

    public static GameManager instance;

    StringItemDictionary _itemPrefab = new StringItemDictionary();

    // Start is called before the first frame update
    void Start()
    {
        var items = Resources.LoadAll<Item>("Item");

        foreach (var item in items)
        {
            _itemPrefab.Add(item.Name, item);
        }

        if(instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateItem(string itemName)
    {
        if(itemName == null) return null;
        var obj = Instantiate<ItemMono>(prefab);
        obj.name = itemName;
        _itemPrefab.TryGetValue(itemName, out obj.Item);
        obj.gameObject.GetComponent<Image>().sprite = obj.Item.Texture;
        return obj.gameObject;
    }
}
