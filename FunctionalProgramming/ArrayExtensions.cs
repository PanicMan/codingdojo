using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionalProgramming
{
    static class ArrayExtensions
    {
        public static IEnumerable<Tuple<T, T>> SelectConsecutive<T>(this IEnumerable<T> input)
        {
            var list = new List<Tuple<T, T>>();
            int i = 0;
            while (i < input.Count() - 1)
            {
                list.Add(Tuple.Create(input.ElementAt(i), input.ElementAt(i+1)));
                i++;
            }
            return list;
        }

        public static IEnumerable<Tresult> SelectConsecutive<T, Tresult>(this IEnumerable<T> input, Func<T, T, Tresult> func)
        {
            var list = new List<Tresult>();
            int i = 0;
            while (i < input.Count() - 1)
            {
                list.Add(func(input.ElementAt(i), input.ElementAt(i + 1)));
                i++;
            }
            return list;
        }
    }
}
