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

    }
    
}