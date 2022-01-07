using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    [SerializeField]
    private GameObject POTD;
    [SerializeField]
    private GameObject CustomersPassword;

    private TMPro.TextMeshProUGUI goodPassword;
    private TMPro.TextMeshProUGUI customerPassword;

    void Awake()
    {
        // Get password from good passwords list
        // if customer knows the password, use the same
        // otherwise, get a bad password
        goodPassword = POTD.GetComponent<TMPro.TextMeshProUGUI>();
        customerPassword = CustomersPassword.GetComponent<TMPro.TextMeshProUGUI>();

        EventManager.RegisterEventListener("CustomerUpdateComplete", UpdatePasswords);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("CustomerUpdateComplete", UpdatePasswords);
    }

    void UpdatePasswords()
    {
        // Call to password stuff
        goodPassword.text = "Good Pass";
        if(Assets.Scripts.Model.GlobalBar.ActiveCustomer.Password == Assets.Scripts.Model.PassEnum.HasNewPassword)
        {
            // get the same password
            customerPassword.text = "Good Pass";
        }
        else
        {
            // get from bad passwords
            customerPassword.text = "Bad Pass";
        }
    }

}
