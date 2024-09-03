using System;
using System.Collections.Generic;

namespace Sample01
{
    public static class MyEnumerable
    {
        public static IEnumerable<T> Every<T>(this IEnumerable<T> source, int step)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(step, 0, nameof(step));

            var sourceEnumerator = source.GetEnumerator();
            int i;

            do
            {
                i = step;

                while (i > 0 && sourceEnumerator.MoveNext())
                    i--;

                if (i == 0)
                    yield return sourceEnumerator.Current;
            }
            while (i == 0);
        }

        public static IEnumerable<T> Every<T>(this IEnumerable<T> source, Func<T, int> stepFunction)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentNullException.ThrowIfNull(stepFunction, nameof(stepFunction));

            var sourceEnumerator = source.GetEnumerator();

            int i;

            if (sourceEnumerator.MoveNext())
            {
                yield return sourceEnumerator.Current;

                do
                {
                    i = Math.Max(stepFunction(sourceEnumerator.Current), 1);

                    while (i > 0 && sourceEnumerator.MoveNext())
                        i--;

                    if (i == 0)
                        yield return sourceEnumerator.Current;
                }
                while (i == 0);
            }
        }


        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

            foreach (var e in source)
            {
                if (predicate(e))
                    yield return e;
            }
        }
    }
}
