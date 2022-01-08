using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpecialEventHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject RaidObject;
    [SerializeField]
    private GameObject PoisonObject;

    void Awake()
    {
        RaidObject.SetActive(false);
        PoisonObject.SetActive(false);

        EventManager.RegisterEventListener("Raiding", OnRaid);
        EventManager.RegisterEventListener("Poisoning", OnPoisoning);
        EventManager.RegisterEventListener("BeginOfTheNight", ClearUI);
        EventManager.RegisterEventListener("RaidOccurs", CancelPanicButton);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("Raiding", OnRaid);
        EventManager.DeregisterEventListener("Poisoning", OnPoisoning);
        EventManager.DeregisterEventListener("BeginOfTheNight", ClearUI);
        EventManager.DeregisterEventListener("RaidOccurs", CancelPanicButton);
    }

    void OnRaid()
    {
        RaidObject.SetActive(true);
    }

    void OnPoisoning()
    {
        PoisonObject.SetActive(true);
    }

    void ClearUI()
    {
        RaidObject.SetActive(false);
        PoisonObject.SetActive(false);
    }

    void CancelPanicButton()
    {
        RaidObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
