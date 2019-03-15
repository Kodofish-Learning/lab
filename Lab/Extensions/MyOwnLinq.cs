using System;
using System.Collections.Generic;

namespace Lab.Extensions
{
    public static class MyOwnLinq
    {
        
        public static IEnumerable<TSource> FishWhere<TSource>(this List<TSource> source, Func<TSource, bool> predict)
        {
            foreach (var product in source)
            {
                if (predict(product))
                {
                    yield return product;
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
    }
    
}