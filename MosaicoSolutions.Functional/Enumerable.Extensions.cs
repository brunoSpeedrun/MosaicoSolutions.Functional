using System;
using System.Collections.Generic;
using System.Linq;

namespace MosaicoSolutions.Functional.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var element in source)
                action(element);
        }

        public static IEnumerable<TSource> PeekForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            source.ForEach(action);
            return source;
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
            => !source.Any();

        public static Option<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> source)
            => source.IsEmpty()
                ? Option.None<TSource>()
                : Option.Of(source.First());

        public static Option<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var element in source)
                if (predicate(element))
                    return element;
            
            return Option.None<TSource>();
        }

        public static Option<TSource> LastOrNone<TSource>(this IEnumerable<TSource> source)
            => source.IsEmpty()
                ? Option.None<TSource>()
                : Option.Of(source.Last());

        public static Option<TSource> LastOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var exists = false;
            var result = default(TSource);

            foreach (var element in source)
                if (predicate(element))
                {
                    exists = true;
                    result = element;
                }

            return exists
                ? result
                : Option.None<TSource>();
        }

        public static Option<TSource> SingleOrNone<TSource>(this IEnumerable<TSource> source)
        {
            if (source is IList<TSource> list)
            {
                if (list.Count == 1)
                    return list[0];
            }
            else
                using (var e = source.GetEnumerator())
                {
                    if (!e.MoveNext())
                        return Option.None<TSource>();

                    var result = e.Current;

                    if (!e.MoveNext())
                        return result;
                }

            return Option.None<TSource>();
        }
        
        
        public static Option<TSource> SingleOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var count = 0;
            var result = default(TSource);

            foreach (var element in source)
                if (predicate(element))
                {
                    count++;
                    result = element;

                    if (count > 1)
                        return Option.None<TSource>();
                }

            return count == 1
                    ? result
                    : Option.None<TSource>();
        }
        
        public static Option<TSource> ElementAtOrNone<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (index < 0)
                return Option.None<TSource>();
            
            if (source is IList<TSource> list)
            {
                if (index < list.Count)
                    return list[index];
            }
            else
                using (var e = source.GetEnumerator())
                    while (true)
                    {
                        if (!e.MoveNext())
                            break;

                        if (index == 0)
                            return e.Current;

                        index--;
                    }
            
            return Option.None<TSource>();
        }
    }
}