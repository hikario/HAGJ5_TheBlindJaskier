using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakEasyFloorDoorAssistant : MonoBehaviour
{
    private Animation anim;

    void Awake()
    {
        anim = gameObject.GetComponent<Animation>();

        EventManager.RegisterEventListener("RaidExit", OnPanic);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("RaidExit", OnPanic);
    }

    void OnPanic()
    {
        StartCoroutine(PanicExit());
    }

    IEnumerator PanicExit()
    {
        anim.Play("SE_FloorDoor_Open");
        yield return new WaitForSeconds(2.5f);
        anim.Play("SE_FloorDoor_Close");
    }
}
