using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this List<TSource> source, Func<TSource, bool> func)
        {
            var sourceEnumerator = source.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (func(current))
                {
                    yield return current;
                }
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> func)
        {
            var sourceEnumerator = urls.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                yield return func(current);
            }
        }
        public static IEnumerable<string> JoeySelectWithIndex(IEnumerable<Employee> urls, Func<int, Employee, string> func)
        {
            var sourceEnumerator = urls.GetEnumerator();
            var index = 0;
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                yield return func(index, current);
            }
        }
    }
}