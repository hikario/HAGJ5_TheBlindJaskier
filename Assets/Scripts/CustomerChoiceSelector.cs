using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerChoiceSelector : MonoBehaviour
{
    public void LetIn()
    {
        Assets.Scripts.Model.GlobalBar.ActiveCustomer.AllowedToEnter = true;
        Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character.Anim_Accepted();
        Assets.Scripts.Model.GlobalBar.ActiveCustomers.Add(Assets.Scripts.Model.GlobalBar.ActiveCustomer);
        EventManager.TriggerEvent("UpdateToNextActiveCustomer");
    }

    public void KeepOut()
    {
        Assets.Scripts.Model.GlobalBar.ActiveCustomer.AllowedToEnter = false;
        Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character.Anim_Rejected();
        EventManager.TriggerEvent("UpdateToNextActiveCustomer");
    }
}
