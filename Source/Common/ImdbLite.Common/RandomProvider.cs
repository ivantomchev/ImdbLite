namespace ImdbLite.Common
{
    public static class RandomProvider
    {
        private static RandomGenerator instance;

        /// <summary>
        /// The instance of the RandonGenerator class
        /// </summary>

        public static RandomGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RandomGenerator();
                }

                return instance;
            }
        }
    }
}
