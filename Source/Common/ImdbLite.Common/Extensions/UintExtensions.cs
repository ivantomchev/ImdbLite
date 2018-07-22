namespace ImdbLite.Common.Extensions
{
    using System;

    public static class UintExtensions
    {
        public static int ToInt(this uint source) 
            => Convert.ToInt32(source);
    }
}
