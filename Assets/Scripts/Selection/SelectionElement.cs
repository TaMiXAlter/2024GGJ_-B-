using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionElement : MonoBehaviour
{
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
        }
    }
    public Sprite Img
    {
        get => _img;
        set
        {
            if(value)_img = value;
        }
    }
    
    private string _name;
    private Sprite _img;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    public void BindActionOnButton(UnityAction newAction)
    {
        _button.onClick.AddListener(newAction);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
