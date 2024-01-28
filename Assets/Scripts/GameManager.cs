using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    List<string> _initialItem = new List<string> { "cilantro", "fungus", "kimchi", "sashimi", "tomatoes", "blood", "soy sauce", "butter", "cheese", "red meat", "milk tea", "stinky tofu", "curry", "bread", "coke", "alcohol", "rice", "tofu", "noodles", "vinegar rice", "coffee", "pearls", "pineapple" };

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
        foreach (var item in _initialItem)
        {
            _playerHadItme.Add(item);
        }


        ReCreateAllHadItem();
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
                //Overlap Detect
                Vector2 orig = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hits = Physics2D.RaycastAll(orig, Vector2.zero);
                foreach(var hit in hits)
                {
                    var hitIitem = hit.transform.GetComponent<ItemMono>();
                    string result;

                    //Combine
                    if (hitIitem == null) continue;
                    if (draggedItem.GetComponent<ItemMono>().Item.Combine(hitIitem.Item.Name, out result) && !hitIitem.IsSelected)
                    {
                        Vector2 middle = (draggedItem.transform.position + hit.transform.position) / 2;
                        var newItem = CreateOnField(result);
                        newItem.transform.position = middle;
                        Destroy(draggedItem);
                        Destroy(hit.transform.gameObject);

                        ReCreateAllHadItem();

                        Debug.Log("Combine: " + result);
                        AudioSystem.instance.PlaySoundEffect("combine");
                    }
                }
                draggedItem.GetComponent<ItemMono>().OnPointerUp(new PointerEventData(EventSystem.current));

                if(draggedItem != null)
                    draggedItem.GetComponent<BoxCollider2D>().enabled = true;
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
        if(_itemPrefab.TryGetValue(itemName, out obj.Item))
        {
            if(obj.Item.Texture != null)
                obj.gameObject.GetComponent<Image>().sprite = obj.Item.Texture;
        }
        else
        {
            Debug.Log("No " + itemName);
        }
        return obj.gameObject;
    }

    public void CreateFromSelection(string itemName)
    {
        var newDrag = CreateOnField(itemName);
        newDrag.GetComponent<ItemMono>().IsSelected = false;
        //newDrag.GetComponent<Image>().raycastTarget = false;
        newDrag.GetComponent<BoxCollider2D>().enabled = false;
        draggedItem = newDrag;
    }

    public void BeginDrag(GameObject itemObj)
    {
        //itemObj.GetComponent<Image>().raycastTarget = false;
        itemObj.GetComponent<BoxCollider2D>().enabled = false;
        itemObj.transform.SetSiblingIndex(itemObj.transform.parent.childCount - 1);
        draggedItem = itemObj;

    }

    private void ReCreateAllHadItem()
    {
        for(; _createdCount < _playerHadItme.Count; _createdCount++)
        {
            var selection = CreateOnContainer(_playerHadItme[_createdCount]);
            selection.GetComponent<ItemMono>().IsSelected = true;
        }
    }
}
