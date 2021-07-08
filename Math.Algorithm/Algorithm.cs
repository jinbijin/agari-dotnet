using Traversable;
using System;

namespace Math.Algorithm
{
    public static class Algorithm
    {
        public static PowerDecomposition Decompose(int toDecompose)
        {
            if (toDecompose <= 0)
            {
                throw new InvalidOperationException("The integer to decompose must be positive.");
            }
            if (toDecompose == 1)
            {
                return new() { Base = 1, Power = 1 };
            }

            for (int power = 2; toDecompose >> power >= 1; power++)
            {
                var decomposition = BinarySearchable.Range(1, toDecompose).Find(number => TryComparePowerToNumber(number, power, toDecompose));
                if (decomposition.Power != default || decomposition.Base != default)
                {
                    var subDecomposition = Decompose(decomposition.Base);
                    return new() { Base = subDecomposition.Base, Power = subDecomposition.Power * decomposition.Power };
                }
            }

            return new() { Base = toDecompose, Power = 1 };
        }

        // If exact is false, then compareTo is rounded down.
        private static ComparisonResult ComparePowerToNumber(int powerBase, int power, int compareTo, bool exact)
        {
            if (power == 1 && powerBase == compareTo && exact)
            {
                return ComparisonResult.Equal;
            }
            if (power == 1 && powerBase <= compareTo) // In case of equality, exact must be false here.
            {
                return ComparisonResult.Less;
            }
            if (powerBase > compareTo)
            {
                return ComparisonResult.Greater;
            }

            var factor = power % 2 == 0 ? 1 : powerBase;
            return ComparePowerToNumber(powerBase * powerBase, power / 2, compareTo / factor, compareTo % factor == 0);
        }

        private static IBinarySearchResult<PowerDecomposition> TryComparePowerToNumber(int powerBase, int power, int compareTo) =>
            ComparePowerToNumber(powerBase, power, compareTo, true) switch
            {
                ComparisonResult.Equal => BinarySearchResult.Found<PowerDecomposition>(new() { Base = powerBase, Power = power }),
                ComparisonResult.Less => BinarySearchResult.BeforeCurrent<PowerDecomposition>(),
                ComparisonResult.Greater => BinarySearchResult.AfterCurrent<PowerDecomposition>(),
                _ => throw new ArgumentOutOfRangeException("returnValue", "Method returned unexpected value.")
            };
    }
}
