using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAssistant : MonoBehaviour
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
        StartCoroutine(PopDrawer());
    }

    IEnumerator PopDrawer()
    {
        anim.Play("AN_BarOpen");
        yield return new WaitForSeconds(1);
        anim.Play("AN_BarClose");
    }

}
