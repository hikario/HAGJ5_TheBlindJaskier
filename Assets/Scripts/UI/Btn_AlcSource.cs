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
    Image _rnd;
    private void Awake()
    {
        _btn = GetComponent<Button>();
        _rnd = GetComponent<Image>();
        _btn.onClick.AddListener(OnClick);

        _neibors.AddRange(transform.parent.GetComponentsInChildren<Btn_AlcSource>(true)); // get all other buttons in the same group

    }
    private void Update()
    {
        if (_btn.enabled && GlobalBar.SourceAvailability.HasFlag(alcoholSource) == false)
        {
            _btn.enabled = false;
            _rnd.enabled = false;
        }

        if (_btn.enabled == false && GlobalBar.SourceAvailability.HasFlag(alcoholSource))
        {
            _btn.enabled = true;
            _rnd.enabled = true;
        }
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


