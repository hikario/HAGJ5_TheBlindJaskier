using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanicButton : MonoBehaviour
{
    private Button panicButton;
    [SerializeField]
    private GameObject RaidMessage;
    [SerializeField]
    private GameObject PanicPressedMessage;

    // Start is called before the first frame update
    void Awake()
    {
        panicButton = gameObject.GetComponent<Button>();
        panicButton.onClick.AddListener(OnClick);
    }

    void OnClick()
    {   
        // Set Raiding to false
        Assets.Scripts.Model.GlobalBar.IsRaidingNow = false;        
        // Trigger Animations for bottle drop/counter open
        // Trigger Animations for customers to panic leave
        Assets.Scripts.Model.GlobalBar.FinancialConsequences = true;
        // Show Panic Message
        RaidMessage.SetActive(false);
        PanicPressedMessage.SetActive(true);
        // Send Police Down
        EventManager.TriggerEvent("RaidExit");
        EventManager.TriggerEvent("PoliceGoDown");
        EventManager.TriggerEvent("TheNightEnds");
    }

}
