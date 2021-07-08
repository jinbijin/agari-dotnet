using System;

namespace Math.Algorithm
{
    public static class PowerExtensions
    {
        public static int ToPowerTruncated(this int powerBase, int power, int limit)
        {
            if (powerBase < 1) { throw new ArgumentOutOfRangeException(nameof(powerBase), "Power base must be at least 1."); }
            if (power < 0) { throw new ArgumentOutOfRangeException(nameof(power), "Power must be at least 0."); }
            if (limit < 0) { return 0; }
            if (power == 0) { return 1; }

            int truncatedPower = ToPowerTruncated(powerBase, power, limit, 1);
            return truncatedPower > limit ? limit + 1 : truncatedPower;
        }

        private static int ToPowerTruncated(int powerBase, int power, int limit, int surplus)
        {
            if (powerBase * surplus > limit || power == 1) { return powerBase; }

            int factor = power % 2 == 0 ? 1 : powerBase;
            return factor * ToPowerTruncated(powerBase * powerBase, power >> 1, limit, surplus * factor);
        }
    }
}
