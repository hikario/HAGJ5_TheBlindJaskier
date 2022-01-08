using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Btn_Commit : MonoBehaviour
{
    GameFlow _gameFlowObject;
    Button _btn;
    Image _rnd;
    
    private void Awake()
    {
        var gameObject = GameObject.Find("/GameManager");
        _gameFlowObject = gameObject.GetComponent<GameFlow>();
        
        _btn = GetComponent<Button>();
        _rnd = GetComponent<Image>();
        _btn.onClick.AddListener(OnClick);
        _btn.interactable = false;
        //_rnd.enabled = false;
    }
    private void Update()
    {
        if (_btn.interactable == true)
            return;

        if (GlobalBar.CurrentAlcoholPrices == 0 || GlobalBar.CurrentQuality == 0 || GlobalBar.CurrentSource == 0)
            return;

        _btn.interactable = true;
        _rnd.enabled = true;
    }
    void OnEnable()
    {
        _btn.interactable = false;
    }

    void OnClick()
    {
        if (_btn.interactable)
            _gameFlowObject.ProgressDay();
    }
}


