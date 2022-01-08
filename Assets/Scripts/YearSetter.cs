using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearSetter : MonoBehaviour
{
    // Start is called before the first frame update
    private TMPro.TextMeshProUGUI textField;
    void Awake()
    {
        textField = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        textField.text = Assets.Scripts.Model.GlobalBar.Year.ToString();
        
        EventManager.RegisterEventListener("YearChanged", UpdateYear);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("YearChanged", UpdateYear);
    }

    void UpdateYear()
    {
        //if (Assets.Scripts.Model.GlobalBar.Year == 1922)
        //    Assets.Scripts.Model.GlobalBar.SourceAvailability = Assets.Scripts.Model.GlobalBar.AlcoholSources.Moonshine | Assets.Scripts.Model.GlobalBar.AlcoholSources.Mafia;
        //else
        //    Assets.Scripts.Model.GlobalBar.SourceAvailability = Assets.Scripts.Model.GlobalBar.AlcoholSources.Detroit | Assets.Scripts.Model.GlobalBar.AlcoholSources.Moonshine | Assets.Scripts.Model.GlobalBar.AlcoholSources.Mafia;

        textField.text = Assets.Scripts.Model.GlobalBar.Year.ToString();
    }
    
}
