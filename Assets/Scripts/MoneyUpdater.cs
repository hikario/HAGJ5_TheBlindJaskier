using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUpdater : MonoBehaviour
{
    private TMPro.TextMeshProUGUI moneyField;


    void Awake()
    {
        moneyField = gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        EventManager.RegisterEventListener("UpdateMoney", UpdateMoneyUI);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("UpdateMoney", UpdateMoneyUI);
    }

    void UpdateMoneyUI()
    {
        moneyField.text = "$" + Assets.Scripts.Model.GlobalBar.Money.ToString();
    }
}
