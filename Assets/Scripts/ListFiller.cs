using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListFiller : MonoBehaviour
{

    private TMPro.TextMeshProUGUI textField;
    private int probability;
    void Awake()
    {
        textField = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        probability = 80;
        
        EventManager.RegisterEventListener("CustomerListUpdated", UpdateList);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("CustomerListUpdated", UpdateList);
    }
    
    void UpdateList()
    {
        List<Assets.Scripts.Model.BaseCustomer> oldCustomers = Assets.Scripts.Model.GlobalBar.OldCustomers;
        List<Assets.Scripts.Model.BaseCustomer> newCustomers = Assets.Scripts.Model.GlobalBar.NewCustomers;
        List<string> onTheList = new List<string>();
        string output = "";

        for(int i = 0; i < oldCustomers.Count; i++)
        {
            onTheList.Add(oldCustomers[i].Name);
        }
        for(int i = 0; i < newCustomers.Count; i++)
        {
            if ((Random.Range(0, 100) <= probability && !(newCustomers[i] is Assets.Scripts.Model.CopCustomer)) || newCustomers[i] is Assets.Scripts.Model.ImportantCustomer)
            {
                onTheList.Add(newCustomers[i].Name);
            }
        }

        for(int i = 0; i < onTheList.Count; i++)
        {
            output = output + onTheList[i];
            if(i % 2 == 0)
            {
                output = output + "\t";
            }
            else if(i % 2 == 1)
            {
                output = output + "\n";
            }
        }
        textField.text = output;
    }


}
