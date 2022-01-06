using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearSetter : MonoBehaviour
{
    // Start is called before the first frame update
    private TMPro.TextMeshProUGUI textField;
    void Start()
    {
        textField = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        textField.text = Assets.Scripts.Model.GlobalBar.Year.ToString();
    }

    void Awake()
    {
        EventManager.RegisterEventListener("YearChanged", UpdateYear);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("YearChanged", UpdateYear);
    }

    void UpdateYear()
    {
        textField.text = Assets.Scripts.Model.GlobalBar.Year.ToString();
    }
    
}
