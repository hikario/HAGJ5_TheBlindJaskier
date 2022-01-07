using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Btn_AlcSource : MonoBehaviour
{
    public GlobalBar.AlcoholSources alcoholSource;
    List<Btn_AlcSource> _neibors = new List<Btn_AlcSource>();
    Button _btn;
    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnClick);

        _neibors.AddRange(transform.parent.GetComponentsInChildren<Btn_AlcSource>(true)); // get all other buttons in the same group
    }
        
    void OnClick()
    {
        GlobalBar.CurrentSource = alcoholSource;
        foreach (var eachNeibor in _neibors)
            eachNeibor.RefreshState();
    }

    void RefreshState()
    {
        _btn.interactable = alcoholSource != GlobalBar.CurrentSource;
    }
}


