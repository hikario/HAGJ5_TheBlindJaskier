﻿using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Btn_AlcQuality : MonoBehaviour
{
    public AlcoholQualityes alcoholQuality;
    List<Btn_AlcQuality> _neibors = new List<Btn_AlcQuality>();
    Button _btn;
    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnClick);

        _neibors.AddRange(transform.parent.GetComponentsInChildren<Btn_AlcQuality>(true)); // get all other buttons in the same group
    }
    void OnClick()
    {
        GlobalBar.CurrentQuality = alcoholQuality;
        foreach (var eachNeibor in _neibors)
            eachNeibor.RefreshState();
    }

    void RefreshState()
    {
        _btn.interactable = alcoholQuality != GlobalBar.CurrentQuality;
    }
}


