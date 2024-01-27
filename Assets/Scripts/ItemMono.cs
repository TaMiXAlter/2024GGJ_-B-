using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMono : MonoBehaviour, IPointerDownHandler
{
    public Item Item;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.CreateOnContainer(Item.Name);
    }

    public void TryCombine(ItemMono item)
    {
        string result;
        if(Item.Combine(item.Item.Name, out result))
        GameManager.instance.CreateOnField(result);
    }
}
