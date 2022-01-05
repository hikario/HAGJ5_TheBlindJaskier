using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    /// <summary>
    /// Represents "Important customers"
    /// </summary>
    public class ImportantCustomer : BaseCustomer
    {
        //    mportant customers:
        //    - randomly know or don't know the password
        //    - always on the list
        //    - always want high quality
        //    - always have full price preference
        //    - if quality is middle, no consequence(just an unsatisfied customer)
        //    - if quality is low, increase the number of cops for the following year
        //    - if expectations met, will protect from one raid
        //    - will not be a repeat customer, period

        public ImportantCustomer()
        {
            base.IsOnList = true; ////always on the list
            base.QualityPreference = AlcoholQualityes.High; //always want high quality
            base.PricePreference = AlcoholPrices.High; //always have full price preference
        }
        override public int ProcessExpectation(AlcoholQualityes currentQuality, AlcoholPrices currentPrice)
        {
            var expectationMatch = CheckExpectation(currentQuality, currentPrice);

            if (expectationMatch < 0)
                callTheCops();
            else if (expectationMatch > 0)
                guardFromNextRaid();

            return expectationMatch;
        }
        private void callTheCops()
        {
            // event for anymation..

            GlobalBar.IncreaseRaidProbability(1);
        }
        private void guardFromNextRaid()
        {
            // event for anymation..

            ++GlobalBar.SatisfiedImportantCustomers;
        }
    }
}
