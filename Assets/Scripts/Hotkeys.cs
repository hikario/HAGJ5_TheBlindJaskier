using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotkeys : MonoBehaviour
{
    public Character TestCharacter = null;
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Debug.Log("Hotkey Pressed = F12");
            if (TestCharacter != null)
                TestCharacter.Anim_Enter();
        }
    }
}
