using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public static class GlobalBar
    {

        #region Private

        const int POPULARITY_MAX = 90;

        static int _popularity;

        static bool _isRaidingNow = false;

        static bool _isPoisoningNow = false;

        static int _countOfSoldDrinks = 0;

        static object _lockObject_RaidingPoisoning = new object();

        static object _lockObject_RaidProbability = new object();
        #endregion

        // global storage for our bar, could be only one instance

        [System.Flags]
        public enum AlcoholSources
        {
            Detroit = 1 << 0,
            Moonshine = 1 << 1,
            Mafia = 1 << 2,
        }

        /// <summary>
        /// bitvise mask of available sources, all 3 sources = 7 ( 1 | 2 | 4 )
        /// </summary>
        public static AlcoholSources SourceAvailability { get; set; }
        public static int Year { get; set; }
        public static AlcoholSources CurrentSource { get; set; }
        public static AlcoholQualityes CurrentQuality { get; set; }
        public static AlcoholPrices CurrentAlcoholPrices { get; set; }
        public static decimal Money { get; private set; }
        public static decimal EachDrinkCost { get; set; }
        public static int SatisfiedImportantCustomers { get; set; }
        public static int RaidProbability { get; set; }
        public static int RaidProbabilityStep { get; set; }
        public static int PoisonProbability { get; set; }
        public static List<BaseCustomer> NewCustomers { get; set; }
        public static List<BaseCustomer> OldCustomers { get; set; }
        public static List<BaseCustomer> ActiveCustomers { get; set; }
        public static List<ImportantCustomer> SatisfiedImportantCustomerList { get; set; }
        public static BaseCustomer ActiveCustomer { get; set; }
        public static List<string> PossibleCustomerNames { get; set; }
        public static List<string> UsedCustomerNames { get; set; }
        public static bool FirstStrike { get; set; }
        public static bool FinancialConsequences { get; set; }
        

        static GlobalBar()
        {
            Reset();
        }

        public static void Reset()
        {
            foreach (AlcoholSources t in System.Enum.GetValues(typeof(AlcoholSources)))
                SourceAvailability |= t;

            _popularity = 50;
            Year = 1919;
            CurrentSource = 0;
            CurrentQuality = 0;
            CurrentAlcoholPrices = 0;
            Money = 10000.0M;
            EachDrinkCost = 300.0M;
            SatisfiedImportantCustomers = 0;
            RaidProbability = 0;
            RaidProbabilityStep = 5; // could come from some settings
            PoisonProbability = 0;
            NewCustomers = new List<BaseCustomer>();
            OldCustomers = new List<BaseCustomer>();
            ActiveCustomers = new List<BaseCustomer>();
            SatisfiedImportantCustomerList = new List<ImportantCustomer>();
            ActiveCustomer = null;
            PossibleCustomerNames = new List<string>(NamesList.GetNames());
            UsedCustomerNames = new List<string>();
            FirstStrike = true;
            FinancialConsequences = false;
        }


        public static bool IsSourceAvailable (AlcoholSources alcoholSource)
        {
            return SourceAvailability.HasFlag(alcoholSource);
        }

        public static void IncreaseRaidProbability(int stepsForIncrease = 1)
        {
            if (stepsForIncrease == 0)
                return;

            lock (_lockObject_RaidProbability)
            {
                RaidProbability += RaidProbabilityStep * stepsForIncrease;
                if (RaidProbability < 0)
                    RaidProbability = 0;
            }
        }

        public static void Fine()
        {
            Money = Money - 1000.0M;
            FirstStrike = false;
        }

        //called once user finished choose variables for the day
        public static void BuyAlchohol()
        {
            // All settings will be available from Bar
            decimal calculatedMoney = 0.0M;
            // Cost is set per source:
            // Moonshine is $2k
            // Mafia is $4.5k
            // Importing is $8k
            if (CurrentSource == AlcoholSources.Moonshine)
            {
                calculatedMoney = 2000.0M;
            }
            else if(CurrentSource == AlcoholSources.Mafia)
            {
                calculatedMoney = 4500.0M;
            }
            else
            {
                calculatedMoney = 8000.0M;
            }
            // Quality makes drinks more expensive to buy
            // Original Quality is 1.25x
            // Diluted Quality is 1.0x
            // Thinned Quality is .75x
            if (CurrentQuality == AlcoholQualityes.Low)
            {
                calculatedMoney = calculatedMoney*0.75M;
            }
            else if(CurrentQuality == AlcoholQualityes.High)
            {
                calculatedMoney = calculatedMoney*1.25M;
            }
            Money = Money - calculatedMoney;

            // change PoisonProbability base on type and quality
            PoisonProbability = CalculatePoisoningProbability();
            // Add raid probability with source
            if(CurrentSource == AlcoholSources.Mafia)
            {
                IncreaseRaidProbability(4);
            }
        }

        private static int CalculatePoisoningProbability()
        {
            if(CurrentSource == AlcoholSources.Detroit)
            {
                return 0;
            }
            else if(CurrentSource == AlcoholSources.Mafia)
            {
                return 10;
            }
            else
            {
                return 25;
            }
        }

        public static void OnCustomerByDrink()
        {
            System.Threading.Interlocked.Increment(ref _countOfSoldDrinks);
        }

        public static void OnDayChange()
        {
            if (FinancialConsequences)
            {
                Money += EachDrinkCost * _countOfSoldDrinks * 0.75M;
            }
            else
            {
                Money += EachDrinkCost * _countOfSoldDrinks;
            }
            FinancialConsequences = false;

            _countOfSoldDrinks = 0; // re-set count of drinks
        }

        public static int CountOfSoldDrinks { get => _countOfSoldDrinks; }

        public static bool IsRaidingNow 
        {
            get => _isRaidingNow;
            set
            {
                if (_isRaidingNow == value)
                    return;
                lock (_lockObject_RaidingPoisoning)
                {
                    _isRaidingNow = value;
                    if (_isRaidingNow && _isPoisoningNow) // if both need to play, raid has prioriry on poison
                        _isPoisoningNow = false;
                }
            }
        }
        public static bool IsPoisoningNow 
        { 
            get => _isPoisoningNow;
            set
            {
                if (_isPoisoningNow == value)
                    return;
                lock (_lockObject_RaidingPoisoning)
                {
                    // if both need to play, raid has prioriry on poison
                    if (_isRaidingNow == false || value == false)
                        _isPoisoningNow = value;
                }
            }
        }

        public static int Popularity
        {
            get => _popularity;
            set
            {
                if (value < 0)
                    _popularity = 0;
                else if (value > POPULARITY_MAX)
                    _popularity = POPULARITY_MAX;
                else
                    _popularity = value;
            }
        }
    }
}
