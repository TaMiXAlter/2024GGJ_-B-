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

    public GameObject draggedItem;

    //Private
    StringItemDictionary _itemPrefab = new StringItemDictionary();
    List<string> _playerHadItme = new List<string>();
    int _createdCount = 0;

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
        foreach (var item in _itemPrefab)
        {
            _playerHadItme.Add(item.Value.Name);
        }

        CreateAllHadItem();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            if (draggedItem != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                draggedItem.transform.position = mousePos;
            }
        }
        else
        {
            if(draggedItem != null)
            {
                draggedItem.GetComponent<Image>().raycastTarget = true;
                draggedItem = null;
            }
        }
    }

    public List<string> GetAllPlayerHadItem() { return  _playerHadItme; }

    public GameObject CreateOnField(string itemName) { return CreateItem(itemName, fieldRoot.transform); }
    public GameObject CreateOnContainer(string itemName) { return CreateItem(itemName, containerRoot.transform); }
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

    public void CreateFromSelection(string itemName)
    {
        var newDrag = CreateOnField(itemName);
        newDrag.GetComponent<ItemMono>().IsSelected = false;
        newDrag.GetComponent<Image>().raycastTarget = false;
        draggedItem = newDrag;
    }

    public void BeginDrag(GameObject itemObj)
    {
        itemObj.GetComponent<Image>().raycastTarget = false;
        itemObj.transform.SetSiblingIndex(itemObj.transform.parent.childCount - 1);
        draggedItem = itemObj;

    }

    private void CreateAllHadItem()
    {
        for(; _createdCount < _playerHadItme.Count; _createdCount++)
        {
            var selection = CreateOnContainer(_playerHadItme[_createdCount]);
            selection.GetComponent<ItemMono>().IsSelected = true;
        }
    }
}
