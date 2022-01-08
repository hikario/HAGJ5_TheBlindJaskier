using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Btn_AlcPrice : MonoBehaviour
{
    public AlcoholPrices alcoholPrice;
    List<Btn_AlcPrice> _neibors = new List<Btn_AlcPrice>();
    Button _btn;
    private AlcoholPrices oldPriceValue;
    private void Awake()
    {
        oldPriceValue = 0;
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnClick);

        _neibors.AddRange(transform.parent.GetComponentsInChildren<Btn_AlcPrice>(true)); // get all other buttons in the same group
    }
    private void Update()
    {
        if (oldPriceValue != GlobalBar.CurrentAlcoholPrices)
        {
            oldPriceValue = GlobalBar.CurrentAlcoholPrices;
            RefreshState();
        }
    }

    void OnClick()
    {
        GlobalBar.CurrentAlcoholPrices = alcoholPrice;
        foreach (var eachNeibor in _neibors)
            eachNeibor.RefreshState();
    }

    void RefreshState()
    {
        _btn.interactable = alcoholPrice != GlobalBar.CurrentAlcoholPrices;
    }
}


