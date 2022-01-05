namespace Assets.Scripts.Model
{
    public static class GlobalBar
    {
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
        public static AlcoholSources CurrentSource { get; set; }
        public static AlcoholQualityes CurrentQuality { get; set; }
        public static AlcoholPrices CurrentAlcoholPrices { get; set; }
        public static decimal Money { get; set; }
        public static int SatisfiedImportantCustomers { get; set; }
        public static uint Popularity { get; set; }
        public static int RaidProbability { get; set; }
        public static int RaidProbabilityStep { get; set; }

        static GlobalBar()
        {
            foreach (AlcoholSources t in System.Enum.GetValues(typeof(AlcoholSources)))
                SourceAvailability |= t;

            RaidProbabilityStep = 5; // could come from some settings
        }
        
        public static bool IsSourceAvailable (AlcoholSources alcoholSource)
        {
            return SourceAvailability.HasFlag(alcoholSource);
        }

        public static void IncreaseRaidProbability(int stepsForIncrease = 1)
        {
            RaidProbability += RaidProbabilityStep * stepsForIncrease;
            if (RaidProbability < 0)
                RaidProbability = 0;
        }
    }
}
