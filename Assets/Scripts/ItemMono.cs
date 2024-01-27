using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMono : MonoBehaviour
{
    public Item Item;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void TryCombine(ItemMono item)
    {
        string result;
        if(Item.Combine(item.Item.Name, out result))
        GameManager.instance.CreateItem(result, transform.parent);
    }
}
