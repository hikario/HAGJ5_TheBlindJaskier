using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAssistant : MonoBehaviour
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
        StartCoroutine(PopDrawer());
    }

    void ResetFurniture()
    {
        if(pulledSwitch)
        {
            StartCoroutine(WithdrawFromDrawer());
        }
    }

    IEnumerator PopDrawer()
    {
        anim.Play("AN_BarOpen");
        yield return new WaitForSeconds(1);
        anim.Play("AN_BarClose");
    }

    IEnumerator WithdrawFromDrawer()
    {
        anim.Play("AN_BarOpen");
        yield return new WaitForSeconds(1.25f);
        anim.Play("AN_BarClose");
        pulledSwitch = false;
    }

}
