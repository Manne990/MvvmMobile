using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvvmMobile.Sample.Core.Common
{
    public static class LinqExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> linqResult)
        {
            if (linqResult == null)
            {
                return null;
            }

            return new ObservableCollection<T>(linqResult);
        }

        public static IEnumerable<T> RemoveNulls<T>(this IEnumerable<T> linqResult) where T : class
        {
            if (linqResult == null)
            {
                return new List<T>();
            }

            return linqResult.Where(l => l != null);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}