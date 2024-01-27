using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movable : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        Vector3 offset = transform.position - Input.mousePosition;
        transform.position = Input.mousePosition + offset;
        yield return null;

        while (!Input.GetKeyUp(KeyCode.Mouse0))
        {
            transform.position = Input.mousePosition + offset;
            yield return null;
        }
    }
}
