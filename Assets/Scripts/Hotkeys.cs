using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotkeys : MonoBehaviour
{
    public Character TestCharacter = null;
    public GeneralAnimationHelper TestGenerator = null;
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F12))
        //{
        //    Debug.Log("Hotkey Pressed = F12");
        //    //if (TestGenerator != null)
        //    //    TestCharacter = TestGenerator.SpawnZomby();
        //    //GlobalBar.SourceAvailability ^= GlobalBar.AlcoholSources.Moonshine | GlobalBar.AlcoholSources.Mafia;
        //    GlobalBar.SourceAvailability ^= GlobalBar.AlcoholSources.Detroit;
        //}
        //
        //if (Input.GetKeyDown(KeyCode.F9))
        //{
        //    EventManager.TriggerEvent("DropBottels");
        //}
        //
        //if (Input.GetKeyDown(KeyCode.F10))
        //{
        //    EventManager.TriggerEvent("CallPolice"); 
        //}
        //if (Input.GetKeyDown(KeyCode.F11))
        //{
        //    EventManager.TriggerEvent("PoliceGoDown");
        //}

    }
}
