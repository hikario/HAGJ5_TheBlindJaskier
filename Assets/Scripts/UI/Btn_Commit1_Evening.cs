using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Btn_Commit1_Evening : MonoBehaviour
{
    GameFlow _gameFlowObject;
    Button _btn;
    Image _rnd;

    bool _allcustomerProcessed = false;

    private void Awake()
    {
        var gameObject = GameObject.Find("/GameManager");
        _gameFlowObject = gameObject.GetComponent<GameFlow>();
        
        _btn = GetComponent<Button>();
        _rnd = GetComponent<Image>();
        _btn.onClick.AddListener(OnClick);
        EventManager.RegisterEventListener("AllCustomersProcessed", OnAllCustomersProcessed);
    }
    private void Update()
    {

    }
    void OnDestroy()
    {
        EventManager.DeregisterEventListener("AllCustomersProcessed", OnAllCustomersProcessed);
    }
    void OnEnable()
    {
        _allcustomerProcessed = false;
        _btn.interactable = false;
    }
    void OnAllCustomersProcessed()
    {
        _allcustomerProcessed = true;
        _btn.interactable = true;
    }


    void OnClick()
    {
        if (_allcustomerProcessed)
            _gameFlowObject.ProgressDay();
    }
}


