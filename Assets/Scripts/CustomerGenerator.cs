using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    // Configurable properties. Could be extracted at some point
    [SerializeField]
    private int lowThreshold;
    [SerializeField]
    private int highThreshold;
    [SerializeField]
    private int lowCustomerQuantity;
    [SerializeField]
    private int averageCustomerQuantity;
    [SerializeField]
    private int highCustomerQuantity;
    [SerializeField]
    private int yearSetForImportantSpawn;
    [SerializeField]
    private float chanceToSpawnPolice;

    // Derived properties
    private float chanceToSpawnImportant;
    private bool spawnedImportant;
    private float currentChanceToSpawnImportant;
    private int yearsSinceReset;

    void Awake()
    {
        // lowThreshold = 35;
        // highThreshold = 70;
        spawnedImportant = false;
        // yearSetForImportantSpawn = 5;
        yearsSinceReset = 0;
        chanceToSpawnImportant = 1.0f/yearSetForImportantSpawn;
        currentChanceToSpawnImportant = chanceToSpawnImportant;

        EventManager.RegisterEventListener("GenerateCustomers", GenerateCustomers);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("GenerateCustomers", GenerateCustomers);
    }

    int CalculateIncomingCustomerCount()
    {
        // Calculate the number of customers we're going to spawn
        int currentPopularity = Assets.Scripts.Model.GlobalBar.Popularity;
        // Check to see which we fit into
        if (currentPopularity < lowThreshold)
        {
            // Low, so set customer count to low
            return lowCustomerQuantity;
        }
        else if (currentPopularity >= highThreshold)
        {
            // High, so set customer count to high
            return highCustomerQuantity;
        }
        else
        {
            return averageCustomerQuantity;
        }
    }

    string GetName()
    {
        string name = Assets.Scripts.Model.GlobalBar.PossibleCustomerNames[Random.Range(0, Assets.Scripts.Model.GlobalBar.PossibleCustomerNames.Count)];

        Assets.Scripts.Model.GlobalBar.PossibleCustomerNames.Remove(name);
        Assets.Scripts.Model.GlobalBar.UsedCustomerNames.Add(name);
        return name;
    }

    void GenerateCustomers()
    {
        Debug.Log("Making Customers!");
        // How many customers to generate
        int quantityToGenerate = CalculateIncomingCustomerCount();
        int index = 0;
        // Customer List
        Assets.Scripts.Model.BaseCustomer[] customers = new Assets.Scripts.Model.BaseCustomer[quantityToGenerate];
        // Have we hit the cap on how many years it should be per special?
        if (yearsSinceReset == yearSetForImportantSpawn)
        {
            yearsSinceReset = 0;
            spawnedImportant = false;
            currentChanceToSpawnImportant = chanceToSpawnImportant;
        }

        // Random.Range(floats) will return a random number from minInclusive to maxinclusive
        // Figure out if the special should spawn
        if(!spawnedImportant)
        {
            // Spawn this round?
            if(Random.Range(0.0f, 1.0f) <= currentChanceToSpawnImportant)
            {
                // Spawn the special this round
                index = 1;
                Assets.Scripts.Model.ImportantCustomer specialCustomer = new Assets.Scripts.Model.ImportantCustomer();
                specialCustomer.Name = GetName();
                customers[0] = specialCustomer;
                // Generated, now mark as spawned
                spawnedImportant = true;
                Debug.Log("Fancy Customer Generated");
            }
            else
            {
                // Don't spawn the special this round, add to probability
                currentChanceToSpawnImportant = chanceToSpawnImportant * (yearsSinceReset + 1);
            }
        }
        // Generate all non-specials
        for (; index < quantityToGenerate; index++)
        {
            customers[index] = GenerateNormalCustomer();
            customers[index].Name = GetName();
        }

        // Provide this customer list to the bar
        Assets.Scripts.Model.GlobalBar.NewCustomers = new List<Assets.Scripts.Model.BaseCustomer>(customers);
        yearsSinceReset += 1;
        Debug.Log("All Customers Generated!");

        // Notify to update
        EventManager.TriggerEvent("CustomerListUpdated");
        EventManager.TriggerEvent("SetActiveCustomer");
    }

    private Assets.Scripts.Model.BaseCustomer GenerateNormalCustomer()
    {
        // First, see if it should be a cop or a normal
        if(Random.Range(0.0f, 1.0f) < chanceToSpawnPolice)
        {
            Debug.Log("Generated COP Customer!");
            return new Assets.Scripts.Model.CopCustomer();
        }
        else
        {
            Debug.Log("Generated Regular Customer!");
            return new Assets.Scripts.Model.BaseCustomer();
        }
    }

}
