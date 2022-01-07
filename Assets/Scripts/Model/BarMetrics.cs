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
        public static int RaidProbability { get; private set; }
        public static int RaidProbabilityStep { get; set; }
        public static int PoisonProbability { get; set; }
        public static List<BaseCustomer> NewCustomers { get; set; }
        public static List<BaseCustomer> OldCustomers { get; set; }
        public static List<BaseCustomer> ActiveCustomers { get; set; }
        public static BaseCustomer ActiveCustomer { get; set; }
        public static List<string> PossibleCustomerNames { get; }
        public static List<string> UsedCustomerNames { get; set; }
        

        static GlobalBar()
        {
            foreach (AlcoholSources t in System.Enum.GetValues(typeof(AlcoholSources)))
                SourceAvailability |= t;

            RaidProbabilityStep = 5; // could come from some settings
            _popularity = 50;
            Year = 1919;
            PossibleCustomerNames = GeneratePossibleNames();
            UsedCustomerNames = new List<string>();
            OldCustomers = new List<BaseCustomer>();
            ActiveCustomers = new List<BaseCustomer>();
        }

        private static List<string> GeneratePossibleNames()
        {
            List<string> names = new List<string>();
            // Louis Spencer
            // Cameron Hussain
            // Jay Marshall
            // Sam Berry
            // Patrick Lawson
            // Chris Young
            // Issac Brennan
            // Carter Durham
            // Bryan Dunn
            // Omar Lucas
            // Bethany Cooper
            // Willow Lowe
            // Amy Brooks
            // Ava Nicholson
            // Matilda Barnes
            // Aiyana Foreman
            // Emmalyn Farmer
            // Allison Bradford
            // Jacqueline Alston
            // Teresa Whitley
            names.Add("Louis Spencer");
            names.Add("Cameron Hussain");
            names.Add("Jay Marshall");
            names.Add("Sam Berry");
            names.Add("Patrick Lawson");
            names.Add("Chris Young");
            names.Add("Issac Brennan");
            names.Add("Carter Durham");
            names.Add("Bryan Dunn");
            names.Add("Omar Lucas");
            //////////
            names.Add("Bethany Cooper");
            names.Add("Willow Lowe");
            names.Add("Amy Brooks");
            names.Add("Ava Nicholson");
            names.Add("Matilda Barnes");
            names.Add("Aiyana Foreman");
            names.Add("Emmalyn Farmer");
            names.Add("Allison Bradford");
            names.Add("Jacqueline Alston");
            names.Add("Teresa Whitley");

            return names;
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

        //called once user finished choose variables for the day
        public static void BuyAlchohol()
        {
            // All settings will be available from Bar
            decimal calculatedMoney = 0.0M;
            // change PoisonProbability base on type and quality
            // do some calculation base on input parametes and save moneys
            Money = calculatedMoney;
        }

        public static void OnCustomerByDrink()
        {
            System.Threading.Interlocked.Increment(ref _countOfSoldDrinks);
        }

        public static void OnDayChange()
        {
            Money += EachDrinkCost * _countOfSoldDrinks;

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
