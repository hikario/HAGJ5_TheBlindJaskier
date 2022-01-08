using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCustomerTracker : MonoBehaviour
{
    // Text field to fill in with their name
    private TMPro.TextMeshProUGUI customerName;
    private Assets.Scripts.Model.BaseCustomer currentCustomer;
    private int customerNumber;
    private bool finishedNew;

    [SerializeField]
    private GameObject PasswordPrompt;
    [SerializeField]
    private GameObject CompletedPrompt;


    void Awake()
    {
        finishedNew = false;
        customerNumber = 0;
        customerName = gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        EventManager.RegisterEventListener("SetActiveCustomer", SetActiveCustomer);
        EventManager.RegisterEventListener("UpdateToNextActiveCustomer", UpdateToNextActiveCustomer);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("SetActiveCustomer", SetActiveCustomer);
        EventManager.DeregisterEventListener("UpdateToNextActiveCustomer", UpdateToNextActiveCustomer);
    }

    // this called once at very start
    void SetActiveCustomer()
    {
        CompletedPrompt.SetActive(false);
        PasswordPrompt.SetActive(true);
        if (Assets.Scripts.Model.GlobalBar.NewCustomers.Count <= customerNumber)
        {
            Debug.LogError("Expected at list one customer create in the list!");
            return;
        }

        currentCustomer = Assets.Scripts.Model.GlobalBar.NewCustomers[customerNumber];
        ++customerNumber;
        Assets.Scripts.Model.GlobalBar.ActiveCustomer = currentCustomer;
        customerName.text = currentCustomer.Name;
        EventManager.TriggerEvent("CustomerUpdateComplete");
    }

    void UpdateToNextActiveCustomer()
    {
        if(!finishedNew)
        {
            currentCustomer = Assets.Scripts.Model.GlobalBar.NewCustomers[customerNumber];
            ++customerNumber;
            customerName.text = currentCustomer.Name;
            Assets.Scripts.Model.GlobalBar.ActiveCustomer = currentCustomer;
            EventManager.TriggerEvent("CustomerUpdateComplete");
            if(customerNumber >= Assets.Scripts.Model.GlobalBar.NewCustomers.Count)
            {
                finishedNew = true;
                customerNumber = 0;
            }
        }
        else
        {
            if (customerNumber < Assets.Scripts.Model.GlobalBar.OldCustomers.Count)
            {
                currentCustomer = Assets.Scripts.Model.GlobalBar.OldCustomers[customerNumber];
                customerName.text = currentCustomer.Name;
                ++customerNumber;
                Assets.Scripts.Model.GlobalBar.ActiveCustomer = currentCustomer;
                EventManager.TriggerEvent("CustomerUpdateComplete");
            }
            else
            {
                EventManager.TriggerEvent("AllCustomersProcessed");
                customerNumber = 0;
                finishedNew = false;
                PasswordPrompt.SetActive(false);
                CompletedPrompt.SetActive(true);
            }
        }
    }
}
