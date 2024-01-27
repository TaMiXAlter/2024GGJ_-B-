using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    private void Start()
    {
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit) {
            Debug.Log ("CLICKED " + hit.collider.name);
        }
    }
   
}
