using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private SelectionElement prefeb;

    [SerializeField] private GameObject SelectionParent;

    private void SpawnSelection(Transform spawnParent,string elementName,Sprite elementSprite)
    {
        SelectionElement selection = Instantiate<SelectionElement>(prefeb,spawnParent);
        selection.Name = elementName;
        selection.Img = elementSprite;
        selection.BindActionOnButton(delegate { SpawnElement(elementName); });
    }
    
    private void SpawnElement(string SpawnName)
    {
        GameManager.instance.CreateOnContainer(SpawnName);
    }
}
