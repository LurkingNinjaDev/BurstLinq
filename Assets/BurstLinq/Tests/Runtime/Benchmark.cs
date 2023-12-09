using System;
using System.Linq;
using NUnit.Framework;
using Unity.PerformanceTesting;

#pragma warning disable CS0219

namespace BurstLinq.Tests
{
    public class BenchmarkIntSum
    {
        const int WarmupCount = 5;
        const int MeasurementCount = 100;

        static readonly int[] array = Enumerable.Range(0, 10000).ToArray();

        [TearDown]
        public void TearDown()
        {
            GC.Collect();
        }

        [Test, Performance]
        public void For()
        {
            Measure.Method(() =>
            {
                var result = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    result += array[i];
                }
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .SampleGroup(new SampleGroup("Int Sum: For", SampleUnit.Microsecond))
            .Run();
        }

        [Test, Performance]
        public void LINQ()
        {
            Measure.Method(() =>
            {
                Enumerable.Sum(array);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .SampleGroup(new SampleGroup("Int Sum: LINQ", SampleUnit.Microsecond))
            .Run();
        }

        [Test, Performance]
        public void BurstLinq()
        {
            Measure.Method(() =>
            {
                BurstLinqExtensions.Sum(array);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .SampleGroup(new SampleGroup("Int Sum: BurstLinq", SampleUnit.Microsecond))
            .Run();
        }
    }

    public class BenchmarkIntSequenceEqual
    {
        const int WarmupCount = 5;
        const int MeasurementCount = 100;

        static readonly int[] array1 = Enumerable.Range(0, 10000).ToArray();
        static readonly int[] array2 = Enumerable.Range(0, 10000).ToArray();

        [TearDown]
        public void TearDown()
        {
            GC.Collect();
        }

        [Test, Performance]
        public void For()
        {
            Measure.Method(() =>
            {
                var result = true;
                if (array1.Length != array2.Length)
                {
                    result = false;
                    return;
                }

                for (int i = 0; i < array1.Length; i++)
                {
                    if (array1[i] != array2[i])
                    {
                        result = false;
                        break;
                    }
                }
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .SampleGroup(new SampleGroup("Int SequenceEqual: For", SampleUnit.Microsecond))
            .Run();
        }

        [Test, Performance]
        public void LINQ()
        {
            Measure.Method(() =>
            {
                Enumerable.SequenceEqual(array1, array2);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .SampleGroup(new SampleGroup("Int SequenceEqual: LINQ", SampleUnit.Microsecond))
            .Run();
        }

        [Test, Performance]
        public void BurstLinq()
        {
            Measure.Method(() =>
            {
                BurstLinqExtensions.SequenceEqual(array1, array2);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .SampleGroup(new SampleGroup("Int SequenceEqual: BurstLinq", SampleUnit.Microsecond))
            .Run();
        }
    }

    public class BenchmarkIntMinEqual
    {
        const int WarmupCount = 5;
        const int MeasurementCount = 100;

        static readonly int[] array = Enumerable.Range(0, 10000).ToArray();

        [TearDown]
        public void TearDown()
        {
            GC.Collect();
        }

        [Test, Performance]
        public void For()
        {
            Measure.Method(() =>
            {
                var result = int.MaxValue;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] < result) result = array[i];
                }
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .SampleGroup(new SampleGroup("Int Min: For", SampleUnit.Microsecond))
            .Run();
        }

        [Test, Performance]
        public void LINQ()
        {
            Measure.Method(() =>
            {
                Enumerable.Min(array);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .SampleGroup(new SampleGroup("Int Min: LINQ", SampleUnit.Microsecond))
            .Run();
        }

        [Test, Performance]
        public void BurstLinq()
        {
            Measure.Method(() =>
            {
                BurstLinqExtensions.Min(array);
            })
            .WarmupCount(WarmupCount)
            .MeasurementCount(MeasurementCount)
            .SampleGroup(new SampleGroup("Int Min: BurstLinq", SampleUnit.Microsecond))
            .Run();
        }
    }
}

#pragma warning restore CS0219