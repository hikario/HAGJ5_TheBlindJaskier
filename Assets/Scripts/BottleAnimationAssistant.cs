using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleAnimationAssistant : MonoBehaviour
{
    private Animation anim;
    private bool pulledSwitch;

    void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
        pulledSwitch = false;

        EventManager.RegisterEventListener("PanicButtonPressed", OnPanic);
        EventManager.RegisterEventListener("EndOfTheNight", ResetFurniture);             
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("PanicButtonPressed", OnPanic);
        EventManager.DeregisterEventListener("EndOfTheNight", ResetFurniture);     
    }

    void OnPanic()
    {
        pulledSwitch = true;
        StartCoroutine(DropBottle());
    }

    void ResetFurniture()
    {
        if(pulledSwitch)
        {
            StartCoroutine(RaiseBottle());
        }
    }

    IEnumerator DropBottle()
    {
        yield return new WaitForSeconds(0.25f);
        anim.Play("AN_BottleDrop");
    }

    IEnumerator RaiseBottle()
    {
        yield return new WaitForSeconds(0.25f);
        anim.Play("AN_BottleLift");
        pulledSwitch = false;
    }
}
