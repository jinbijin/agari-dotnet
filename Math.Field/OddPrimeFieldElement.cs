using System;

namespace Math.Field
{
    public readonly struct OddPrimeFieldElement : IEquatable<OddPrimeFieldElement>
    {
        public readonly OddPrimeField Parent { get; init; }
        public readonly int Value { get; init; }

        public readonly bool IsZero => Value == 0;
        public readonly bool HasMultiplicativeInverse => Value != 0;
        public readonly bool IsMultiplicativeUnit => Value == 1;

        public bool Equals(OddPrimeFieldElement other) => this == other;
        public override bool Equals(object? obj) => obj is OddPrimeFieldElement element && Equals(element);
        public override int GetHashCode() => HashCode.Combine(Parent.GetHashCode(), Value.GetHashCode());

        public static OddPrimeFieldElement operator +(OddPrimeFieldElement first, OddPrimeFieldElement second)
        {
            if (first.Parent != second.Parent) { throw new InvalidOperationException("Both elements must have the same parent field."); }

            return new() { Parent = first.Parent, Value = (first.Value + second.Value) % first.Parent.Characteristic };
        }

        public static OddPrimeFieldElement operator *(OddPrimeFieldElement first, OddPrimeFieldElement second)
        {
            if (first.Parent != second.Parent) { throw new InvalidOperationException("Both elements must have the same parent field."); }

            return new() { Parent = first.Parent, Value = (first.Value * second.Value) % first.Parent.Characteristic };
        }

        public static bool operator ==(OddPrimeFieldElement first, OddPrimeFieldElement second) => first.Parent == second.Parent && first.Value == second.Value;
        public static bool operator !=(OddPrimeFieldElement first, OddPrimeFieldElement second) => first.Parent != second.Parent || first.Value != second.Value;
    }
}
