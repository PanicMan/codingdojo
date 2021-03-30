using System;
using System.Linq;
using System.Reactive.Linq;

namespace Reactive
{
    public static class ObservableExtentions
    {
        public static IObservable<(TSource previous, TSource current)> PairWithPrevious<TSource>(this IObservable<TSource> source)
        {
            return source.Scan(
                new ValueTuple<TSource, TSource>(default, default),
                (acc, curr) => (acc.Item2, curr));
        }
    }
}
