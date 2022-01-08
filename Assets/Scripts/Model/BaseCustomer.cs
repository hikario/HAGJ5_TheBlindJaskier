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

    public enum FanLevel
    {
        NotFanYet,
        Fan,
        NotFanAlready,
        Upset,
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
        FanLevel _fanLevel = Model.FanLevel.NotFanYet;

        public PassEnum Password { get; set; }
        public AlcoholQualityes QualityPreference { get; set; }
        public AlcoholPrices PricePreference { get; set; }
        public AlcoholQualityes QualityPreferenceOriginal { get; }
        public AlcoholPrices PricePreferenceOriginal { get; }
        public bool AllowedToEnter { get; set; } = false;
        public string Name { get; set; }
        public Character UI_Character { get; set; }

        public BaseCustomer() 
        {
            Password = (PassEnum)Random.Range(0, 2);
            QualityPreference = QualityPreferenceOriginal = (AlcoholQualityes)Random.Range(1, 3);
            PricePreference = PricePreferenceOriginal = (AlcoholPrices)Random.Range(1, 3);
        }

        //different for each type of customer
        virtual public int ProcessExpectation(AlcoholQualityes currentQuality, AlcoholPrices currentPrice)
        {
            // if poison in play - all 
            int expectationMatch = GlobalBar.IsPoisoningNow ? -1: CheckExpectation(currentQuality, currentPrice);

            if (expectationMatch < 0)
                decreaseFanLevel();

            //- if expectations met, become repeat customers and increase popularity,
            if (expectationMatch >= 0)
                increaseFanLevel();
            
            switch(_fanLevel)
            {
                case FanLevel.Fan:
                    UI_Character.Emotion_Happy();
                    break;
                case FanLevel.NotFanAlready:
                    UI_Character.Emotion_Neutral();
                    break;
                case FanLevel.Upset:
                    UI_Character.Emotion_Sad();
                    break;
            }

            //if expectations exceeded in one, buy 3 drinks total
            int i = expectationMatch + 1;
            if (i < 1) i = 1;
            if (i > 2) i = 2;
            while (i-- > 0)
                BuyExtraDrink();

            return expectationMatch;
        }

        virtual public bool DontWantToReturn()
        {
            return _fanLevel == FanLevel.Upset;
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

        private void increaseFanLevel()
        {
            if (_fanLevel == FanLevel.NotFanYet) // can be fan only once per discussion
            {
                _fanLevel = FanLevel.Fan;

                if (QualityPreference != AlcoholQualityes.None)
                    --QualityPreference;

                if (PricePreference != AlcoholPrices.High)
                    ++PricePreference;
            }
        }

        private void decreaseFanLevel()
        {
            if (_fanLevel == FanLevel.NotFanAlready || _fanLevel == FanLevel.NotFanYet)
            {
                _fanLevel = FanLevel.Upset;
            }

            if (_fanLevel == FanLevel.Fan)
            {
                _fanLevel = FanLevel.NotFanAlready;

                QualityPreference = QualityPreferenceOriginal;
                PricePreference = PricePreferenceOriginal;
            }
        }

        protected void BuyExtraDrink()
        {
            // event for anymation..
            GlobalBar.OnCustomerByDrink();
        }
    }
}
