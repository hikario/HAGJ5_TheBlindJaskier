using UnityEngine;

namespace Assets.Scripts.Model
{
    public enum PassEnum
    {
        NoPassword,
        HasNewPassword,
        HasOldPassword,
    }
    public enum AlcoholQualityes
    {
        None,
        Low,
        Medium,
        High,
    }

    public enum AlcoholPrices
    {
        None,
        Low,
        Medium,
        High,
    }

    /// <summary>
    /// Represents customer in general ("Regular customers" and "Repeat Customers")
    /// </summary>
    public class BaseCustomer 
    {
        //Regular customers:
        //    - randomly know or don't know the password
        //    - randomly on or not on the list
        //    - randomly determine quality preference
        //    - randomly determine price preference
        //    - if expectations unmet, popularity down
        //    - if expectations met, become repeat customers and increase popularity,
        //      and increase price and quality preference range, buy extra drink
        //        - if expectations exceeded in one, buy 3 drinks total
        //    - if expectations met or exceeded, they become repeat customers
        //
        //Repeat Customers:
        //    - know password
        //    - on list
        //    - quality preference extended down by one
        //    - price preference extended up by one
        //    - if expectations met, popularity up, buy two extra drinks
        //    - if expectations exceeded, buy four extra drinks
        //    - if expectations unmet, lose expanded preference
        //        - if expectations unmet twice, they lose repeat status
        int _fanLevel = 0;

        public PassEnum Password { get; set; }
        public AlcoholQualityes QualityPreference { get; set; }
        public AlcoholPrices PricePreference { get; set; }
        public AlcoholQualityes QualityPreferenceOriginal { get; }
        public AlcoholPrices PricePreferenceOriginal { get; }
        public bool IsOnList { get; protected set; } = false;
        
        public BaseCustomer() 
        {
            Password = (PassEnum)Random.Range(0, 1);
            QualityPreference = QualityPreferenceOriginal = (AlcoholQualityes)Random.Range(0, 3);
            PricePreference = PricePreferenceOriginal = (AlcoholPrices)Random.Range(0, 3);
        }

        virtual public int ProcessExpectation(AlcoholQualityes currentQuality, AlcoholPrices currentPrice)
        {
            // not sure if we want to do trade offs, like if cutomer want to pay more money if quality is more than he expected.
            var expectationMatch = CheckExpectation(currentQuality, currentPrice);

            if (expectationMatch < 0)
                --FanLevel;

            //- if expectations met, become repeat customers and increase popularity,
            if (expectationMatch >= 0)
            {
                if (IsOnList == false) IsOnList = true;
                ++FanLevel; //and increase popularity
            }
            
            //if expectations exceeded in one, buy 3 drinks total
            int i = expectationMatch;
            if (i > 3) i = 3;
            while (--i > 0)
                BuyExtraDrink();

            return expectationMatch;
        }

        /// <summary>
        /// return -1 if expectetion unment, 0 - if met exactly, >0 - if expectation exceeded
        /// </summary>
        /// <param name="currentQuality"></param>
        /// <param name="currentPrice"></param>
        /// <returns></returns>
        protected int CheckExpectation(AlcoholQualityes currentQuality, AlcoholPrices currentPrice)
        {
            // not sure if we want to do trade offs, like if cutomer want to pay more money if quality is more than he expected.
            if (currentQuality < this.QualityPreference || currentPrice > this.PricePreference)
                return -1;

            int extraexpectations = 0;
            if (currentQuality > this.QualityPreference)
                ++extraexpectations;

            if (currentPrice < this.PricePreference)
                ++extraexpectations;

            return extraexpectations;
        }

        public int FanLevel
        {
            get => _fanLevel;
            set
            {
                if (value > _fanLevel && _fanLevel > 0 ) // it is already positive..
                    return;

                if (value < _fanLevel && _fanLevel < 0) // it is already negative..
                    return;

                _fanLevel = value;
                if (_fanLevel == 0)
                {
                    QualityPreference = QualityPreferenceOriginal;
                    PricePreference = PricePreferenceOriginal;
                }
                else if (_fanLevel > 0)
                {
                    // and increase price and quality preference range

                    if (QualityPreference != AlcoholQualityes.None)
                        --QualityPreference;

                    if (PricePreference != AlcoholPrices.High)
                        ++PricePreference;
                }
                //else  if _fanLevel < 0 customer will not come back


            }
        }

        protected void BuyExtraDrink()
        {
            // event for anymation..

            
        }
    }
}
