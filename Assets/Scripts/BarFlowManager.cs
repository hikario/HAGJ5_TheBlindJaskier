using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarFlowManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.RegisterEventListener("BeginOfTheNight", RunTheBar);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("BeginOfTheNight", RunTheBar);
    }


    void RunTheBar()
    {
        // Calculate if raid or poisoning is occurring
        bool isRaiding = GetRaidStatus();
        bool isPoisoning = GetPoisoningStatus();
        // Pass that info to the global bar
        Assets.Scripts.Model.GlobalBar.IsRaidingNow = isRaiding;
        Assets.Scripts.Model.GlobalBar.IsPoisoningNow = isPoisoning;
        // Have all the customers drink
        foreach (Assets.Scripts.Model.BaseCustomer customer in Assets.Scripts.Model.GlobalBar.ActiveCustomers)
        {
            //customer handle expectations
            // check for raiding; raise raid event if so
            // check for poisoning; raise poisoning event if so
            customer.ProcessExpectation(Assets.Scripts.Model.GlobalBar.CurrentQuality, Assets.Scripts.Model.GlobalBar.CurrentAlcoholPrices);

            if (isRaiding)
            {
                StartCoroutine(TriggerRaid());
            }
            else if(isPoisoning)
            {
                StartCoroutine(TriggerPoisoning());
            }
            StartCoroutine(MoveOnWithDay());
        }
    }

    IEnumerator TriggerRaid()
    {
        yield return new WaitForSeconds(10);
        EventManager.TriggerEvent("Raiding");
        Debug.Log("Raiding!");
        Assets.Scripts.Model.GlobalBar.RaidProbability = 0;
    }

    IEnumerator TriggerPoisoning()
    {
        yield return new WaitForSeconds(10);
        EventManager.TriggerEvent("Poisoning");
        Debug.Log("Poisoning!");

    }

    IEnumerator MoveOnWithDay()
    {
        yield return new WaitForSeconds(20);
        EventManager.TriggerEvent("TheNightEnds");
    }

    bool GetRaidStatus()
    {
        Debug.Log("Checking Raid Status");
        Debug.Log("Raid Probability: " + Assets.Scripts.Model.GlobalBar.RaidProbability.ToString());
        int randomEvent = Random.Range(0, 100);
        Debug.Log("Random Number: " + randomEvent.ToString());
        if (randomEvent < Assets.Scripts.Model.GlobalBar.RaidProbability)
        {
            return true;
        }
        return false;
    }

    bool GetPoisoningStatus()
    {
        int randomEvent = Random.Range(0, 100);
        if (randomEvent < Assets.Scripts.Model.GlobalBar.PoisonProbability)
        {
            return true;
        }
        return false;
    }


}
