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
    [SerializeField]
    private GameObject ItemParent;

    //Private
    StringItemDictionary _itemPrefab = new StringItemDictionary();
    List<string> _playerHadItem = new List<string>();
    List<string> _initialItem;
    int _createdCount = 0;

    // read init_list.txt to _initialItem
    void ReadInitList()
    {
        _initialItem = new List<string>();
        TextAsset initList = Resources.Load<TextAsset>("init_list");
        string[] lines = initList.text.Split(' ');
        foreach (string line in lines)
        {
            _initialItem.Add(line);
        }
        
        Debug.Log(initList);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;

        ReadInitList();

        var items = Resources.LoadAll<Item>("Item");

        foreach (var item in items)
        {
            _itemPrefab.Add(item.Name, item);
        }


        //Init _playerHadItem, have basic elemental
        foreach (var item in _initialItem)
        {
            _playerHadItem.Add(item);
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

    public List<string> GetAllPlayerHadItem() { return  _playerHadItem; }

    public GameObject CreateOnField(string itemName) { 
        GameObject g = CreateItem(itemName, fieldRoot.transform); 
        g.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        return g;
    }
    public GameObject CreateOnContainer(string itemName) { return CreateItem(itemName, containerRoot.transform); }
    private GameObject CreateItem(string itemName, Transform parent)
    {
        if(itemName == null) return null;
        if(!_playerHadItem.Contains(itemName))
            _playerHadItem.Add(itemName);

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
        for(; _createdCount < _playerHadItem.Count; _createdCount++)
        {
            var selection = CreateOnContainer(_playerHadItem[_createdCount]);
            selection.GetComponent<ItemMono>().IsSelected = true;
        }
    }
    public void DeleteAllItem(){
        foreach (Transform child in ItemParent.transform){
            Destroy(child.gameObject);
        }
    }
}
