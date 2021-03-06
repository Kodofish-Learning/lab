using System;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab.Extensions
{
    public static class MyOwnLinq
    {
        
        public static IEnumerable<TSource> FishWhere<TSource>(this List<TSource> source, Func<TSource, bool> predict)
        {
//            foreach (var product in source)
//            {
//                if (predict(product))
//                {
//                    yield return product;
//                }
//            }

            var sourceEnumerator = source.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var item = sourceEnumerator.Current;

                if (predict(item))
                {
                    yield return item;
                }
            }
        }
        
        
        
        public static IEnumerable<TSource> FishWhere<TSource>(this List<TSource> source, Func<TSource, int, bool> predict)
        {
            var index = 0;
            foreach (var product in source)
            {
                if (predict(product,index))
                {
                    yield return product;
                }

                index++;
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> mapper)
        {
            foreach (var url in urls)
            {
                yield return mapper(url);
            }
        }

        public static IEnumerable<TResult> JoeySelectWithIndex<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, int, TResult> selector)
        {
            var list = new List<TResult>();
            var index = 0;
            foreach (var url in urls)
            {
                list.Add( selector(url, index));
                index++;
            }

            return list;
        }

        public static IEnumerable<Employee> JoeyTake(this IEnumerable<Employee> employees, int count)
        {
            var enumerator = employees.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                yield return item;
//                index++;
                if (++index == count) yield break;
            }
        }

        public static IEnumerable<T> JoeySkip<T>(this IEnumerable<T> source, int count)
        {
            var sourceEnumerator = source.GetEnumerator();
            var index = 0;
            while (sourceEnumerator.MoveNext())
            {
                if (index++ >= count) yield return sourceEnumerator.Current;
            }
        }

        public static IEnumerable<Card> JoeyTakeWhile(this IEnumerable<Card> cards, Func<Card, bool> predicate)
        {
            foreach (var card in cards)
            {
                if (predicate(card)) yield return card;
                else
                    yield break;
            }
        }

        public static bool JoeyAll(this IEnumerable<Girl> girls, Func<Girl, bool> predicate)
        {
            var sourceGirls = girls.GetEnumerator();
            while (sourceGirls.MoveNext())
            {
                var girl = sourceGirls.Current;
                if (!predicate(girl)) return false;
            }

            return true;
        }

        public static bool JoeyAny(this IEnumerable<Product> enumerable, Func<Product, bool> predicate)
        {
            var source = enumerable.GetEnumerator();
            while (source.MoveNext())
            {
                var item = source.Current;
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static T JoeyFirstOrDefault<T>(this IEnumerable<T> employees)
        {
            var sourceEnumerator = employees.GetEnumerator();

            return sourceEnumerator.MoveNext() ? sourceEnumerator.Current : default(T);
        }

        public static T JoeyLastOrDefault<T>(this IEnumerable<T> employees)
        {
            var sourceEnumerator = employees.GetEnumerator();
            var result = default(T);
            while (sourceEnumerator.MoveNext())
            {
                result = sourceEnumerator.Current;
            }

            return result;
        }

        public static IEnumerable<T> JoeyReverse<T>(IEnumerable<T> employees)
        {
            return new Stack<T>(employees);
        }

        private static IEnumerable<T> JoeyReverse1<T>(IEnumerable<T> employees)
        {
            var stack = new Stack<T>(employees);
            var sourceEnumerator = stack.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                yield return sourceEnumerator.Current;
            }
        }

        private static IEnumerable<T> JoeyReverse2<T>(IEnumerable<T> employees)
        {
            var stack = new Stack<T>(employees);
            var sourceEnumerator = stack.GetEnumerator();
            while (stack.Any())
            {
                yield return stack.Pop();
            }
        }

        public static IEnumerable<TResult> JoeyZip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> girls,
            IEnumerable<TSecond> keys, Func<TFirst, TSecond, TResult> selector)
        {
            var girlsEnumerator = girls.GetEnumerator();
            var keysEnumerator = keys.GetEnumerator();

            while (girlsEnumerator.MoveNext() && keysEnumerator.MoveNext())
            {
                var girl = girlsEnumerator.Current;
                var key = keysEnumerator.Current;
                yield return selector(girl , key);
            }
        }

        public static bool JoeySequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                if (!secondEnumerator.MoveNext()) return false;
                
                if (!comparer.Equals(firstEnumerator.Current, secondEnumerator.Current)) return false;
            }

            return !secondEnumerator.MoveNext();
        }

        public static IEnumerable<T> JoeyConcat<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                yield return firstEnumerator.Current;
            }

            var secondEnumerator = second.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                yield return secondEnumerator.Current;
            }
        }
    }
    
}