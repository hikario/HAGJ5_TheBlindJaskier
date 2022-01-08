using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleAnimationAssistant : MonoBehaviour
{
    private Animation anim;

    void Awake()
    {
        anim = gameObject.GetComponent<Animation>();

        EventManager.RegisterEventListener("PanicButtonPressed", OnPanic);        
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("PanicButtonPressed", OnPanic);
    }

    void OnPanic()
    {
        StartCoroutine(DropBottle());
    }

    IEnumerator DropBottle()
    {
        yield return new WaitForSeconds(0.25f);
        anim.Play("AN_BottleDrop");
    }
}
