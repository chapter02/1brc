using System.Runtime.CompilerServices;

namespace _1brc
{
    public struct Summary
    {
        public int Min;
        public int Max;
        public int Sum;
        public int Count;
        public double Average => (double)Sum / Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Apply(int value, bool existing)
        {
            if (existing)
                Apply(value);
            else
                Init(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Init(int value)
        {
            Min = value;
            Max = value;
            Sum = value;
            Count = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Apply(int value)
        {
            Min = GetMin(Min, value);
            Max = GetMax(Max, value);
            Sum += value;
            Count++;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static int GetMin(int a, int b)
            {
                int delta = a - b;
                return b + (delta & (delta >> 31));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static int GetMax(int a, int b)
            {
                int delta = a - b;
                return a - (delta & (delta >> 31));
            }
        }

        public void Merge(Summary other)
        {
            if (other.Min < Min)
                Min = other.Min;
            if (other.Max > Max)
                Max = other.Max;
            Sum += other.Sum;
            Count += other.Count;
        }

        public override string ToString() => $"{Min / 10.0:N1}/{Average / 10.0:N1}/{Max / 10.0:N1}";
    }
}