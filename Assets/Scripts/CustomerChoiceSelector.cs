using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerChoiceSelector : MonoBehaviour
{
    public void LetIn()
    {
        Assets.Scripts.Model.GlobalBar.ActiveCustomers.Add(Assets.Scripts.Model.GlobalBar.ActiveCustomer);
        EventManager.TriggerEvent("UpdateToNextActiveCustomer");
    }

    public void KeepOut()
    {
        EventManager.TriggerEvent("UpdateToNextActiveCustomer");
    }
}
