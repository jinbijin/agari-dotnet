using System.Collections.Generic;
using Logic.BracketGenerators.RoundRobin.Cyclic;
using Moq;

namespace Logic.Tests.BracketGenerators.RoundRobin.Cyclic
{
    public class CyclicGeneratorTests
    {
        public CyclicGeneratorTests()
        {
            _cyclicSeedGeneratorMock = new Mock<ICyclicSeedGenerator>();
            _cyclicGenerator = new CyclicGenerator(_cyclicSeedGeneratorMock.Object);
        }

        private readonly Mock<ICyclicSeedGenerator> _cyclicSeedGeneratorMock;
        private readonly ICyclicGenerator _cyclicGenerator;

        public static IEnumerable<object[]> SuccessData =>
            new List<object[]>
            {
                new object[] { 5, 20, new List<List<int>> {
                    new List<int>{ 1, 2, 4, 3 },
                    new List<int>{ 2, 4, 3, 1 },
                    new List<int>{ 4, 3, 1, 2 },
                    new List<int>{ 3, 1, 2, 4 },
                    new List<int>{ 0, 0, 0, 0 }
                } }
            };

        public static IEnumerable<object[]> FailureData =>
            new List<object[]>
            {
                new object[] { 4, 16 },
                new object[] { 5, 16 }
            };

        public static IEnumerable<object[]> InvalidData =>
            new List<object[]>
            {
                new object[] { 3, 20, "Number of rounds must be at least 4." },
                new object[] { 4, 21, "Number of participants should be a multiple of 4." }
            };
    }
}
