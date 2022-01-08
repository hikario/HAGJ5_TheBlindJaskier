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

    // Start is called before the first frame update
    void Awake()
    {
        currentState = DayState.MORNING;
        morningCanvas.SetActive(true);
        eveningCanvas.SetActive(false);
        nightCanvas.SetActive(false);

        // EventManager.RegisterEventListener("CustomersVacated", SendCustomersToList);
        // EventManager.RegisterEventListener("EndOfTheNight", Test);
    }

    void OnDestroy()
    {
        // EventManager.DeregisterEventListener("CustomersVacated", SendCustomersToList);
        // EventManager.RegisterEventListener("EndOfTheNight", Test);
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
                break;
            case DayState.EVENING:
                // All customers have been checked
                // Have Night stuff play out
                currentState = DayState.NIGHT;
                EventManager.TriggerEvent("BeginOfTheNight");
                SetCanvasesForNight();
                break;
            default:
                // Update popularity, update customers lists
                EventManager.TriggerEvent("EndOfTheNight");
                currentState = DayState.MORNING;
                ProgressYear();
                SetCanvasesForMorning();
                SendCustomersToList();
                // Reset Alcohol Choices
                EventManager.TriggerEvent("PopularityUpdate");
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

    void Test()
    {
        Debug.Log("Received OnEndOfTheNight Event");
    }

}
