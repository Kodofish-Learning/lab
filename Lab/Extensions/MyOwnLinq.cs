using System;
using System.Collections.Generic;

namespace Lab.Extensions
{
    public static class MyOwnLinq
    {
        public static List<TSource> JoeyWhere2<TSource>(this List<TSource> source, Func<TSource, bool> predict)
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
    }
}