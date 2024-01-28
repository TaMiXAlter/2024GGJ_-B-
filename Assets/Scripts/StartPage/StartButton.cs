using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Button _button;
    private Animator _animator;
    private Image BG;
    static private float FadeDelta = .05f;
    void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        BG = GameObject.Find("Bg").GetComponent<Image>();
    }

    private void Start()
    {
        _button.onClick.AddListener(GoGamePlay);
    }

    void GoGamePlay()
    {
        AudioSystem.instance.PlaySoundEffect("drop");
        SceneManager.LoadScene("TestScene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioSystem.instance.PlaySoundEffect("start");
        _animator.SetBool("Hover",true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool("Hover",false);
    }
}
