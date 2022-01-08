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
        EventManager.RegisterEventListener("BeginOfTheNight", OnBeginOfTheNight);
        EventManager.RegisterEventListener("EndOfTheNight", OnEndOfTheNight);
        
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("CustomerUpdateComplete", OnCustomerUpdateComplete);
        EventManager.DeregisterEventListener("UpdateToNextActiveCustomer", OnUpdateToNextActiveCustomer);
        EventManager.DeregisterEventListener("BeginOfTheNight", OnBeginOfTheNight);
        EventManager.DeregisterEventListener("EndOfTheNight", OnEndOfTheNight);
    }

    void OnCustomerUpdateComplete()
    {
        if (Assets.Scripts.Model.GlobalBar.ActiveCustomer == null)
            return;

        int random = Random.Range(0, MyArmy.Count);
        var character = Instantiate(MyArmy[random], new Vector3(0, 0, 0), Quaternion.identity, transform);
        Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character = character;
        Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character.Anim_EnterToShop();
    }

    //
    void OnUpdateToNextActiveCustomer()
    {
        //if (Assets.Scripts.Model.GlobalBar.ActiveCustomer.AllowedToEnter)
        //    Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character.Anim_Accepted();
        //else
        //    Assets.Scripts.Model.GlobalBar.ActiveCustomer.UI_Character.Anim_Rejected();
    }

    void OnBeginOfTheNight()
    {

    }

    void OnEndOfTheNight()
    {
        foreach(Assets.Scripts.Model.BaseCustomer customer in Assets.Scripts.Model.GlobalBar.ActiveCustomers)
        {
            customer.UI_Character.Anim_Exit();
        }
        // EventManager.TriggerEvent("CustomersVacated");
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
