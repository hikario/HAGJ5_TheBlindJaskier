using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Btn_Commit2_Night : MonoBehaviour
{
    GameFlow _gameFlowObject;
    Button _btn;


    private void Awake()
    {
        var gameObject = GameObject.Find("/GameManager");
        _gameFlowObject = gameObject.GetComponent<GameFlow>();
        
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnClick);
        EventManager.RegisterEventListener("TheNightEnds", OnATheNightEnds);
    }
    private void Update()
    {

    }
    void OnDestroy()
    {
        EventManager.DeregisterEventListener("TheNightEnds", OnATheNightEnds);
    }
    void OnEnable()
    {
        _btn.interactable = false;
    }
    void OnATheNightEnds()
    {
        _btn.interactable = true;
    }


    void OnClick()
    {
        _gameFlowObject.ProgressDay();
    }
}


