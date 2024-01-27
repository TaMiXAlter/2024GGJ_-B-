using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMono : MonoBehaviour, IPointerDownHandler
{
    public Item Item;
    public bool IsSelected = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(IsSelected)
        {
            GameManager.instance.CreateFromSelection(Item.Name);
        }
        else
        {
            GameManager.instance.BeginDrag(gameObject);
        }
    }

    public void TryCombine(ItemMono item)
    {
        string result;
        if(Item.Combine(item.Item.Name, out result))
        GameManager.instance.CreateOnField(result);
    }
}
