using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Math.Algorithm.Tests
{
    public class PowerDecompositionTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                // one
                new object[] { 1, 1, 1 },
                // non-pure power
                new object[] { 6, 6, 1 },
                new object[] { 175, 175, 1 },
                new object[] { 57, 57, 1 },
                // prime power
                new object[] { 27, 3, 3 },
                new object[] { 121, 11, 2 },
                // composite power
                new object[] { 81, 3, 4 },
                new object[] { 64, 2, 6 }
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void DecomposePower(int toDecompose, int powerBase, int power)
        {
            PowerDecomposition result = Algorithm.Decompose(toDecompose);

            result.Should().BeEquivalentTo(new PowerDecomposition { Base = powerBase, Power = power });
        }
    }
}
