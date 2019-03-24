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

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func)
        {
            var sourceEnumerator = source.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                yield return func(current);
            }
        }
        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> source, 
            Func<int, TSource, TResult> func)
        {
            var sourceEnumerator = source.GetEnumerator();
            var index = 0;
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                index++;
                yield return func(index, current);
            }
        }

        public static IEnumerable<TSource> JoeyTake<TSource>(this IEnumerable<TSource> employees, int count)
        {
            var sourceEnumerator = employees.GetEnumerator();
            var index = 0;
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (++index > count) break;
                yield return current;
            }
        }

        public static IEnumerable<TSource> JoeySelect<TSource>(this IEnumerable<TSource> source, int count)
        {
            var sourceEnumerator = source.GetEnumerator();
            var index = 0;
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (++index > count)
                    yield return current;
            }
        }

        public static IEnumerable<Card> JoeyTakeWhile(this IEnumerable<Card> cards, Func<Card, bool> func)
        {
            var sourceEnumerator = cards.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (func(current))
                {
                    yield break;
                }

                yield return current;
            }
        }

        public static bool JoeyAny<T>(this IEnumerable<T> employees, Func<T, bool> predicate)
        {
            var sourceEnumerator = employees.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (predicate(current))
                {
                    return true;
                }
            }

            return false;
        }

        public static T JoeyFirstOrDefault<T>(this IEnumerable<T> employees)
        {
            var sourceEnumerator = employees.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                if (current != null)
                {
                    return current;
                }
            }

            return default(T);
        }

        public static IEnumerable<Employee> JoeyReverse(this IEnumerable<Employee> employees)
        {
            var stack = new Stack<Employee>(employees);
            var sourceEnumerator = stack.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                yield return current;
            }
        }
    }
}