using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<int> { 2, 3, 4, 5 };

            Console.WriteLine("First:");
            var output = input. SelectConsecutive();
            foreach (var item in output)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("Second:");
            Tuple<int, int>[] output2 = input.SelectConsecutive((prev, next) => Tuple.Create(prev, next)).ToArray();
            foreach (var item in output2)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadKey();
            // output = [ (2,3), (3,4), (4, 5) ] }        }
        }
    }
}
