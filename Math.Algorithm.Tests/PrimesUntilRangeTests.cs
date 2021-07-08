using FluentAssertions;
using Math.Algorithm.Enumerables;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Math.Algorithm.Tests
{
    public class PrimesUntilRangeTests
    {
        [Fact]
        public void PrimesUntilRangeTest()
        {
            List<int> result = Ranges.PrimesUntil(25).ToList();

            result.Should().BeEquivalentTo(new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23 });
        }
    }
}
