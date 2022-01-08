using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidHandler : MonoBehaviour
{

    void Awake()
    {
        EventManager.RegisterEventListener("RaidOccurs", OnRaidOccurs);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("RaidOccurs", OnRaidOccurs);
    }

    void OnRaidOccurs()
    {
        Assets.Scripts.Model.GlobalBar.DoublePriceNextYear = true;
        // Check if SatisfiedImportantCustomers is > 0;
        // Block if so
        if (Assets.Scripts.Model.GlobalBar.SatisfiedImportantCustomerList.Count > 0)
        {
            Debug.Log("SAVED");
            Assets.Scripts.Model.ImportantCustomer customer = Assets.Scripts.Model.GlobalBar.SatisfiedImportantCustomerList[0];
            Assets.Scripts.Model.GlobalBar.SatisfiedImportantCustomerList.Remove(customer);

            StartCoroutine(PauseAndGoDownstairs(customer));
        }
        else
        {
            Debug.Log("CONSEQUENCES");
            EventManager.TriggerEvent("PoliceGoDown");
            EventManager.TriggerEvent("Consequences");
            EventManager.TriggerEvent("TheNightEnds");
        }
    }

    IEnumerator PauseAndGoDownstairs(Assets.Scripts.Model.ImportantCustomer customer)
    {
        customer.UI_Character.Anim_EnterToShop();
        // Pause 3 seconds, send police out event,
        // pause 3 more seconds, then go downstairs
        yield return new WaitForSeconds(3);
        EventManager.TriggerEvent("SendPoliceHome");
        yield return new WaitForSeconds(3);
        customer.UI_Character.Anim_Accepted();
    }

}
