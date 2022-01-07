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
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Debug.Log("Hotkey Pressed = F12");
            if (TestGenerator != null)
                TestCharacter = TestGenerator.SpawnZomby();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            if (TestCharacter != null)
                TestCharacter.Emotion_Happy();
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            if (TestCharacter != null)
                TestCharacter.Emotion_Neutral();
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            if (TestCharacter != null)
                TestCharacter.Emotion_Sad();
        }

    }
}
