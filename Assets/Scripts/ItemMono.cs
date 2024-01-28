using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMono : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Item Item;
    public bool IsSelected = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsSelected)
        {
            GameManager.instance.CreateFromSelection(Item.Name);
        }
        else
        {
            GameManager.instance.BeginDrag(gameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!IsSelected)
        {
            // Delete if drop on panel
            RaycastHit2D[] hits = Physics2D.RaycastAll(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero
            );
            if (hits.Any(x => x.collider.gameObject.name == "Panel"))
            {
                GameManager.instance.draggedItem = null;
                Destroy(gameObject);
            }
        }
    }

    public void TryCombine(ItemMono item)
    {
        string result;
        if (Item.Combine(item.Item.Name, out result))
            GameManager.instance.CreateOnField(result);
    }
}
