using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpecialEventHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject RaidObject;
    [SerializeField]
    private GameObject PoisonObject;
    [SerializeField]
    private GameObject RaidMessage;
    [SerializeField]
    private GameObject RaidPanicMessage;

    void Awake()
    {
        RaidObject.SetActive(false);
        PoisonObject.SetActive(false);

        EventManager.RegisterEventListener("Raiding", OnRaid);
        EventManager.RegisterEventListener("Poisoning", OnPoisoning);
        EventManager.RegisterEventListener("BeginOfTheNight", ClearUI);
        EventManager.RegisterEventListener("RaidOccurs", CancelPanicButton);
        EventManager.RegisterEventListener("EndOfTheNight", ReenablePanicButton);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("Raiding", OnRaid);
        EventManager.DeregisterEventListener("Poisoning", OnPoisoning);
        EventManager.DeregisterEventListener("BeginOfTheNight", ClearUI);
        EventManager.DeregisterEventListener("RaidOccurs", CancelPanicButton);
        EventManager.DeregisterEventListener("EndOfTheNight", ReenablePanicButton);
    }

    void OnRaid()
    {
        RaidObject.SetActive(true);
        RaidPanicMessage.SetActive(false);
        RaidMessage.SetActive(true);
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

    void ReenablePanicButton()
    {
        RaidObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
