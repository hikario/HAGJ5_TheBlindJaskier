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
    void Start()
    {
        currentState = DayState.MORNING;
        dayNumber = 1;
        currentYear = 1919 + dayNumber;
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
                break;
            case DayState.EVENING:
                currentState = DayState.NIGHT;
                SetCanvasesForNight();
                break;
            default:
                currentState = DayState.MORNING;
                ProgressYear();
                SetCanvasesForMorning();
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
        currentYear = currentYear + 1;
        dayNumber = dayNumber + 1;
    }
}
