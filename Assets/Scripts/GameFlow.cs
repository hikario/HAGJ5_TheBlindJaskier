using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DayState
{
    MORNING,
    EVENING,
    NIGHT
}

public class GameFlow : MonoBehaviour
{
    private DayState currentState;
    private int dayNumber;
    private int currentYear;
    [SerializeField]
    private GameObject morningCanvas;
    [SerializeField]
    private GameObject eveningCanvas;
    [SerializeField]
    private GameObject nightCanvas;
    [SerializeField]
    private GameObject LoseScreen;
    [SerializeField]
    private GameObject WinScreen;

    // Start is called before the first frame update
    void Awake()
    {
        currentState = DayState.MORNING;
        morningCanvas.SetActive(true);
        eveningCanvas.SetActive(false);
        nightCanvas.SetActive(false);

        EventManager.TriggerEvent("UpdateMoney");
        EventManager.RegisterEventListener("Consequences", OnConsequences);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("Consequences", OnConsequences);
    }
    
    public void ProgressDay()
    {
        switch(currentState)
        {
            case DayState.MORNING:
                // Moving from morning to evening
                // Pay for alcohol, load up customers lists
                currentState = DayState.EVENING;
                SetCanvasesForEvening();
                EventManager.TriggerEvent("GenerateCustomers");
                Assets.Scripts.Model.GlobalBar.BuyAlchohol();
                EventManager.TriggerEvent("UpdateMoney");
                break;
            case DayState.EVENING:
                // All customers have been checked
                // Have Night stuff play out
                currentState = DayState.NIGHT;
                EventManager.TriggerEvent("BeginOfTheNight");
                SetCanvasesForNight();
                EventManager.TriggerEvent("UpdateMoney");
                break;
            default:
                // Update popularity, update customers lists
                Assets.Scripts.Model.GlobalBar.OnDayChange();
                EventManager.TriggerEvent("EndOfTheNight");
                currentState = DayState.MORNING;
                ProgressYear();
                SetCanvasesForMorning();
                EventManager.TriggerEvent("UpdateMoney");
                SendCustomersToList();
                // Reset Alcohol Choices
                EventManager.TriggerEvent("PopularityUpdate");
                CheckForPoverty();
                break;
        }
    }

    void SetCanvasesForMorning()
    {
        nightCanvas.SetActive(false);
        morningCanvas.SetActive(true);
    }

    void SetCanvasesForEvening()
    {
        morningCanvas.SetActive(false);
        eveningCanvas.SetActive(true);
    }
    
    void SetCanvasesForNight()
    {
        eveningCanvas.SetActive(false);
        nightCanvas.SetActive(true);
    }

    void ProgressYear()
    {
        dayNumber = dayNumber + 1;
        Assets.Scripts.Model.GlobalBar.Year = Assets.Scripts.Model.GlobalBar.Year + 1;
        EventManager.TriggerEvent("YearChanged");
        CheckFor21stAmendment();
    }

    void SendCustomersToList()
    {
        //forget about upset customers.
        Assets.Scripts.Model.GlobalBar.ActiveCustomers.RemoveAll(cust => cust.DontWantToReturn());

        // our active customer become old customers
        Assets.Scripts.Model.GlobalBar.OldCustomers = Assets.Scripts.Model.GlobalBar.ActiveCustomers;

        // and use fresh list for next active customer
        Assets.Scripts.Model.GlobalBar.ActiveCustomers = new List<Assets.Scripts.Model.BaseCustomer>();
    }

    void OnConsequences()
    {
        if(Assets.Scripts.Model.GlobalBar.FirstStrike)
        {
            // Trigger Fine Message and Fine
            Assets.Scripts.Model.GlobalBar.Fine();
            EventManager.TriggerEvent("UpdateMoney");
            Assets.Scripts.Model.GlobalBar.FinancialConsequences = true;
        }
        else
        {
            OnLose();
        }
    }

    void CheckForPoverty()
    {
        Debug.Log("Checking For Poverty");
        if(Assets.Scripts.Model.GlobalBar.Money < 1500.00M)
        {
            Debug.Log("You're Broke");
            OnLose();
        }
    }

    void OnLose()
    {
        Debug.Log("Activating Lose Screen");
        LoseScreen.SetActive(true);
    }

    void CheckFor21stAmendment()
    {
        if(Assets.Scripts.Model.GlobalBar.Year == 1934)
        {
            OnWin();
        }
    }

    void OnWin()
    {
        WinScreen.SetActive(true);
    }

}
