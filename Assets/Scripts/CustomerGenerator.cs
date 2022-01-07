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

    // Start is called before the first frame update
    void Start()
    {
        // lowThreshold = 35;
        // highThreshold = 70;
        spawnedImportant = false;
        // yearSetForImportantSpawn = 5;
        yearsSinceReset = 0;
        chanceToSpawnImportant = 1/yearSetForImportantSpawn;
        currentChanceToSpawnImportant = chanceToSpawnImportant;
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

    public void GenerateCustomers()
    {
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
            if(Random.Range(0.0f, 1.0f) < currentChanceToSpawnImportant)
            {
                // Spawn the special this round
                index = 1;
                Assets.Scripts.Model.ImportantCustomer specialCustomer = new Assets.Scripts.Model.ImportantCustomer();
                customers[0] = specialCustomer;
                // Generated, now mark as spawned
                spawnedImportant = true;
            }
            else
            {
                // Don't spawn the special this round, add to probability
                currentChanceToSpawnImportant = currentChanceToSpawnImportant + chanceToSpawnImportant;
            }
        }
        // Generate all non-specials
        for (; index < quantityToGenerate; index++)
        {
            customers[index] = GenerateNormalCustomer();
        }

        // Provide this customer list to the bar
        Assets.Scripts.Model.GlobalBar.NewCustomers = new List<Assets.Scripts.Model.BaseCustomer>(customers);
        yearsSinceReset += 1;
    }

    private Assets.Scripts.Model.BaseCustomer GenerateNormalCustomer()
    {
        // First, see if it should be a cop or a normal
        if(Random.Range(0.0f, 1.0f) < chanceToSpawnPolice)
        {
            return new Assets.Scripts.Model.CopCustomer();
        }
        else
        {
            return new Assets.Scripts.Model.BaseCustomer();
        }
    }

}
