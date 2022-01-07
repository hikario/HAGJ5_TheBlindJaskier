using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTracker : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textField;
    private int denominator;
    private int numerator;
    
    void Awake()
    {
        textField = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        textField.text = "0/0";

        EventManager.RegisterEventListener("CustomerListUpdated", UpdateCustomerCount);
        EventManager.RegisterEventListener("UpdateToNextActiveCustomer", MoveCustomerCount);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("CustomerListUpdated", UpdateCustomerCount);
        EventManager.DeregisterEventListener("UpdateToNextActiveCustomer", MoveCustomerCount);
    }

    void UpdateCustomerCount()
    {
        denominator = Assets.Scripts.Model.GlobalBar.OldCustomers.Count + Assets.Scripts.Model.GlobalBar.NewCustomers.Count;
        numerator = 1;
        textField.text = numerator.ToString() + "/" + denominator.ToString();
    }

    void MoveCustomerCount()
    {
        numerator += 1;
        if (numerator > denominator)
        {
            numerator = denominator;
        }
        textField.text = numerator.ToString() + "/" + denominator.ToString();
    }

}
