using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringItemDictionary : SerializableDictionary<string, Item> { }

public class GameManager : MonoBehaviour
{
    //Public
    public ItemMono prefab;
    public GameObject fieldRoot;
    public GameObject containerRoot;
    public static GameManager instance;

    //Private
    StringItemDictionary _itemPrefab = new StringItemDictionary();
    List<string> _playerHadItme = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;


        var items = Resources.LoadAll<Item>("Item");

        foreach (var item in items)
        {
            _itemPrefab.Add(item.Name, item);
        }

        //Init _playerHadItme, have basic elemental

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string> GetAllPlayerHadItem() { return  _playerHadItme; }

    public void CreateOnField(string itemName) { CreateItem(itemName, fieldRoot.transform); }
    public void CreateOnContainer(string itemName) { CreateItem(itemName, containerRoot.transform); }
    private GameObject CreateItem(string itemName, Transform parent)
    {
        if(itemName == null) return null;
        if(!_playerHadItme.Contains(itemName))
            _playerHadItme.Add(itemName);

        var obj = Instantiate<ItemMono>(prefab, parent);
        obj.name = itemName;
        _itemPrefab.TryGetValue(itemName, out obj.Item);
        obj.gameObject.GetComponent<Image>().sprite = obj.Item.Texture;
        return obj.gameObject;
    }
}
