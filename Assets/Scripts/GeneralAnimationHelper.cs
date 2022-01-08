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
    void Awake()
    {
        EventManager.RegisterEventListener("CustomerUpdateComplete", OnCustomerUpdateComplete);
        EventManager.RegisterEventListener("UpdateToNextActiveCustomer", OnUpdateToNextActiveCustomer);

    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("CustomerUpdateComplete", OnCustomerUpdateComplete);
        EventManager.DeregisterEventListener("UpdateToNextActiveCustomer", OnUpdateToNextActiveCustomer);
    }

    void OnCustomerUpdateComplete()
    {
        if (Assets.Scripts.Model.GlobalBar.ActiveCustomer == null)
            return;

        var character = Instantiate(MyArmy[0], new Vector3(0, 0, 0), Quaternion.identity, transform);
        Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character = character;
        Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character.Anim_EnterToShop();
    }

    //
    void OnUpdateToNextActiveCustomer()
    {
        if (Assets.Scripts.Model.GlobalBar.ActiveCustomer.AllowedToEnter)
            Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character.Anim_Accepted();
        else
            Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character.Anim_Rejected();
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
