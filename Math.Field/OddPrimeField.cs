using System;
using System.Collections.Generic;
using System.Linq;

namespace Math.Field
{
    public class OddPrimeField
    {
        private readonly int _prime;
        private List<OddPrimeFieldElement>? _elements;
        private OddPrimeFieldElement? _primitiveRoot;
        private List<OddPrimeFieldElement>? _units;

        public int Characteristic => _prime;
        public int Size => _prime;
        public IReadOnlyCollection<OddPrimeFieldElement> Elements => _elements ?? ComputeElements();
        public OddPrimeFieldElement PrimitiveRoot => _primitiveRoot ?? ComputePrimitiveRoot();
        public IReadOnlyCollection<OddPrimeFieldElement> Units => _units ?? ComputeUnits();

        public OddPrimeField(int prime)
        {
            _prime = prime;
        }

        public OddPrimeFieldElement Create(int value) => new() { Parent = this, Value = value };

        private List<OddPrimeFieldElement> ComputeElements()
        {
            List<OddPrimeFieldElement> result = Enumerable.Range(0, _prime).Select(value => new OddPrimeFieldElement { Parent = this, Value = value }).ToList().Shuffle();
            _elements = result;
            return result;
        }

        private OddPrimeFieldElement ComputePrimitiveRoot()
        {
            foreach (OddPrimeFieldElement element in Elements.Where(element => element.HasMultiplicativeInverse))
            {
                if (IsPrimitiveRoot(element))
                {
                    _primitiveRoot = element;
                    return element;
                }
            }

            throw new InvalidOperationException("Unreachable");
        }

        private bool IsPrimitiveRoot(OddPrimeFieldElement element)
        {
            OddPrimeFieldElement power = Create(1);
            foreach (int exponent in Enumerable.Range(1, _prime / 2))
            {
                power *= element;
                if (power.IsMultiplicativeUnit)
                {
                    return false;
                }
            }

            return true;
        }

        private List<OddPrimeFieldElement> ComputeUnits()
        {
            List<OddPrimeFieldElement> result = new();
            OddPrimeFieldElement power = Create(1);
            foreach (int exponent in Enumerable.Range(1, _prime - 1))
            {
                power *= PrimitiveRoot;
                result.Add(power);
            }
            _units = result;
            return result;
        }
    }
}
