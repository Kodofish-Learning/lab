using System;
using System.Collections.Generic;

namespace Lab.Extensions
{
    public static class MyOwnLinq
    {
        
        public static List<TSource> FishWhere<TSource>(this List<TSource> source, Func<TSource, bool> predict)
        {
            var list = new List<TSource>();
            foreach (var product in source)
            {
                if (predict(product))
                {
                    list.Add(product);
                }
            }

            return list;
        }
        
        public static List<TSource> FishWhere<TSource>(this List<TSource> source, Func<TSource, int, bool> predict)
        {
            var list = new List<TSource>();
            var index = 0;
            foreach (var product in source)
            {
                if (predict(product,index))
                {
                    list.Add(product);
                }

                index++;
            }

            return list;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> mapper)
        {
            var list = new List<TResult>();
            foreach (var url in urls)
            {
                
                list.Add(mapper(url));
            }

            return list;
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