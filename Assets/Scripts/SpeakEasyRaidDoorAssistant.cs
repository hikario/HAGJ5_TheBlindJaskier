using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakEasyRaidDoorAssistant : MonoBehaviour
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
        anim.Play("SE_PaintingDoor_Open");
        yield return new WaitForSeconds(4);
        anim.Play("SE_PaintingDoor_Close");
    }

}
