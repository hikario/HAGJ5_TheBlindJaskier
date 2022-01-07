using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    // knows about Bar, creates and process custommers.
    class Generator
    {
        const int POPULARITY_FOR_CUSTOMER = 5;
        List<BaseCustomer> knownCustomers = new List<BaseCustomer>();
        public void OnStartOfTheDay()
        {
            int countOfNewCustomers = GlobalBar.Popularity / POPULARITY_FOR_CUSTOMER;
            countOfNewCustomers += Random.Range(3, 6);
            while (--countOfNewCustomers > 0)
            {
                var newCustomer = createNewCustomer();
                askPlayerToLetCustomerIn(newCustomer);
                if (newCustomer.AllowedToEnter)
                    knownCustomers.Add(newCustomer);
            }
        }

        public void OnMiddleDayPart()
        {
            foreach (var eachKnownCustomer in knownCustomers)
            {
                sendCustomersDown(eachKnownCustomer);
            }
             
            knownCustomers.RemoveAll(customer => customer.DontWantToReturn());
        }

        void sendCustomersDown(BaseCustomer knownCustomer)
        {
            // do something with customer, may be animation..
            
            knownCustomer.ProcessExpectation(GlobalBar.CurrentQuality, GlobalBar.CurrentAlcoholPrices);

        }

        #region New Customers
        BaseCustomer createNewCustomer()
        {
            const int chance_of_polisman = 10; // could be base on raid prob.
            const int chance_of_important_customer = 10;

            BaseCustomer newCustomer;
            int luckyNumber = Random.Range(1, 100);
            if (luckyNumber <= chance_of_polisman)
            {
                newCustomer = new CopCustomer();
            }
            else if (luckyNumber <= chance_of_polisman + chance_of_important_customer)
            {
                newCustomer = new ImportantCustomer();
            }
            else
            {
                newCustomer = new BaseCustomer();
            }
            
            return newCustomer;
        }

        void askPlayerToLetCustomerIn(BaseCustomer someStranger)
        {
            if (someStranger is ImportantCustomer)
            {
                // friendly face to show that big guy comming to the party
                someStranger.AllowedToEnter = true;
            }
            else
            {
                // dialog for player to decide 
                someStranger.AllowedToEnter = Random.Range(0, 1) == 0;
            }
        }
        #endregion

        
    }
}
