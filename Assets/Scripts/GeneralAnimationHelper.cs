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

    private Btn_Commit2_Night _btn_Commit2_Night = null;

    void Awake()
    {
        EventManager.RegisterEventListener("CustomerUpdateComplete", OnCustomerUpdateComplete);
        //EventManager.RegisterEventListener("UpdateToNextActiveCustomer", OnUpdateToNextActiveCustomer); // animation is triggered in CustomerChoiceSelector just before event called.
        //EventManager.RegisterEventListener("BeginOfTheNight", OnBeginOfTheNight);
        EventManager.RegisterEventListener("BeginOfTheNight", OnBeginOfTheNight);
        EventManager.RegisterEventListener("EndOfTheNight", OnEndOfTheNight);
        EventManager.RegisterEventListener("TheNightEnds", OnATheNightEnds);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("CustomerUpdateComplete", OnCustomerUpdateComplete);
        //EventManager.DeregisterEventListener("UpdateToNextActiveCustomer", OnUpdateToNextActiveCustomer);
        //EventManager.DeregisterEventListener("BeginOfTheNight", OnBeginOfTheNight);
        EventManager.DeregisterEventListener("BeginOfTheNight", OnBeginOfTheNight);
        EventManager.DeregisterEventListener("EndOfTheNight", OnEndOfTheNight);
        EventManager.DeregisterEventListener("TheNightEnds", OnATheNightEnds);
        
    }
    internal void InitNightButtonHelper (Btn_Commit2_Night btn_Commit2_Night)
    {
        Debug.Log($"InitNightButtonHelper");
        _btn_Commit2_Night = btn_Commit2_Night;
        OnBeginOfTheNight();
    }
    void OnBeginOfTheNight()
    {
        Debug.Log($"OnBeginOfTheNight - {_btn_Commit2_Night?.gameObject?.activeSelf} ");
        if (_btn_Commit2_Night != null)
            _btn_Commit2_Night.gameObject.SetActive(false);
        Debug.Log($"OnBeginOfTheNight => {_btn_Commit2_Night?.gameObject?.activeSelf} ");
    }
    void OnATheNightEnds()
    {
        Debug.Log($"OnATheNightEnds - {_btn_Commit2_Night?.gameObject?.activeSelf} ");
        if (_btn_Commit2_Night != null)
            _btn_Commit2_Night.gameObject.SetActive(true);
        Debug.Log($"OnATheNightEnds => {_btn_Commit2_Night?.gameObject?.activeSelf} ");
    }
    void OnCustomerUpdateComplete()
    {
        if (Assets.Scripts.Model.GlobalBar.ActiveCustomer == null)
            return;

        int random = Random.Range(0, MyArmy.Count);
        if (MyArmy[random].IsPrefabUsed)
        {
            int i = random;
            while (true)
            {
                if (++i >= MyArmy.Count)
                    i = 0;
                if (i == random)
                    break;
                if (MyArmy[i].IsPrefabUsed == false)
                    break;
            }
            random = i;
        }
        if (MyArmy[random].IsPrefabUsed)
            Debug.Log("Dublicate prefab!");
        MyArmy[random].IsPrefabUsed = true;
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

    void OnEndOfTheNight()
    {
        foreach (var cst in Assets.Scripts.Model.GlobalBar.ActiveCustomers)
            cst.UI_Character.Anim_Exit();

        foreach (var cst in MyArmy)
            cst.IsPrefabUsed = false;
    }
        
    public Character SpawnZomby()
    {
        if (MyArmy?.Count > 0)
        {
            //gameObject.transform
            
            //{
            //    character.Anim_EnterToShop();
            //    character.Emotion_Sad();
            //    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(6));
            //    character.Emotion_Neutral();
            //    character.Anim_Accepted();
            //    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(8));
            //    character.Emotion_Happy();
            //});
            ////gameObject.transform
            //
            //return character;
        }
        return null;
    }
    
}
