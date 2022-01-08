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
    }
    
    public void ProgressDay()
    {
        switch(currentState)
        {
            case DayState.MORNING:
                currentState = DayState.EVENING;
                SetCanvasesForEvening();
                EventManager.TriggerEvent("GenerateCustomers");
                Assets.Scripts.Model.GlobalBar.BuyAlchohol();
                break;
            case DayState.EVENING:
                currentState = DayState.NIGHT;
                EventManager.TriggerEvent("BeginOfTheNight");
                SetCanvasesForNight();
                break;
            default:
                currentState = DayState.MORNING;
                ProgressYear();
                SetCanvasesForMorning();
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
}
