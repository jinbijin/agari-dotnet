using System;
using System.Collections.Immutable;
using System.Linq;

namespace Math.Polynomial
{
    public readonly struct Polynomial
    {
        public int Degree => Coefficients.Length - 1;

        /// <summary>
        /// The array of coefficients, from low degree to high degree.
        /// </summary>
        public readonly ImmutableArray<int> Coefficients { get; init; }

        public static Polynomial operator *(in Polynomial first, in Polynomial second)
        {
            int degree = first.Degree + second.Degree;
            var coefficientArray = new int[degree + 1];

            for (int i = 0; i <= first.Degree; i++)
            {
                for (int j = 0; j <= first.Degree; j++)
                {
                    coefficientArray[i + j] += first.Coefficients[i] * second.Coefficients[j];
                }
            }

            return new() { Coefficients = coefficientArray.ToImmutableArray() };
        }

        public static Polynomial CreateIrreducibleMod(int degree, int prime)
        {
            if (prime < 2) { throw new ArgumentOutOfRangeException(nameof(prime), "Modulus must be at least 2."); }
            if (degree < 2) { throw new ArgumentOutOfRangeException(nameof(degree), "Degree must be at least 2."); }

            if (degree > 2) { throw new NotImplementedException(); }

            foreach (var (c0, c1) in Enumerable.Range(0, prime).SelectMany(c0 => Enumerable.Range(0, prime).Select(c1 => (c0, c1))))
            {
                var candidate = new Polynomial { Coefficients = new[] { c0, c1, 1 }.ToImmutableArray() };
                if (Enumerable.Range(0, prime).All(d => ((d*(c1 - d) - c0) % prime) != 0))
                {
                    return candidate;
                }
            }

            throw new InvalidOperationException("No result found.");
        }
    }
}
