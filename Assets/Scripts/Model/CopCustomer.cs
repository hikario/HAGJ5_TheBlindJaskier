using UnityEngine;

namespace Assets.Scripts.Model
{
    public class CopCustomer : BaseCustomer
    {
        //Cops:
        //    - randomly know or don't know the password
        //    - never on the list
        //    - 1/20 chance if they're allowed in they demand bribe
        //    - randomly determine quality preference (Igor: assume they have better income and livestyle than workers from moving companies..)
        //    - randomly determine price preference
        //    - if expectations unmet, increase probability of raid
        //    - if expectations met, lower probability of raid
        //    -* if expectations surpassed, greatly lower probability of raid
        //    - will not be repeat customer, period   (Igor: Why not ? it could be fun to have full bar of cops and get raid right after price increase ;) 

        public bool IsDemandBribe { get; set; }
        public CopCustomer()
        {
            base.IsOnList = false;
            base.QualityPreference = (AlcoholQualityes)Random.Range(1, 3); // Igor: assume they have better income and livestyle than workers from moving companies..
            base.PricePreference = (AlcoholPrices)Random.Range(1, 3); // it could be med-high instead
            IsDemandBribe = Random.Range(1, 20) == 1; // 1/20 chance if they're allowed in they demand bribe
        }

        override public int ProcessExpectation(AlcoholQualityes currentQuality, AlcoholPrices currentPrice)
        {
            var expectationMatch = CheckExpectation(currentQuality, currentPrice);

            if (expectationMatch < 0)
                callTheCops();
            else if (expectationMatch > 0)
                guardFromNextRaid(expectationMatch);

            return expectationMatch;
        }
        private void callTheCops()
        {
            // event for anymation..
            GlobalBar.IncreaseRaidProbability(1);
        }
        private void guardFromNextRaid(int enjoyLvl)
        {
            // event for anymation..
            GlobalBar.IncreaseRaidProbability(enjoyLvl * -1);
        }
    }
}
