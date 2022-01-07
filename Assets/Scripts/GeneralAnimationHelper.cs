using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAnimationHelper : MonoBehaviour
{
    public AnimationClip Entry;
    public AnimationClip Accepted;
    public AnimationClip Rejected;
    public AnimationClip Exit;
    // Start is called before the first frame update

    public List<Character> MyArmy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Character SpawnZomby()
    {
        if (MyArmy?.Count > 0)
        {
            var character = Instantiate(MyArmy[0], new Vector3(0, 0, 0), Quaternion.identity, transform);
            System.Threading.Tasks.Task.Run(async () =>
            {
                character.Anim_EnterToShop();
                character.Emotion_Sad();
                await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(6));
                character.Emotion_Neutral();
                character.Anim_Accepted();
                await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(8));
                character.Emotion_Happy();
            });
            //gameObject.transform
            
            return character;
        }
        return null;
    }
    
}
