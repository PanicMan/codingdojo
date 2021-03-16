using System;
using System.Diagnostics;

namespace FunctionalPyramid
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new int[][] {new int[] {3},
                                    new int[] {7,4},
                                   new int[] {2,4,6},
                                  new int[] {8,5,9,3}};

            var biggestValue = FindBiggestPath(input, 0, 0);
            Console.WriteLine(biggestValue.ToString());
            Console.ReadKey();

            input = new int[][] {   new int[] { 75 },
                                   new int[] { 95,64 },
                                  new int[] { 17,47,82 },
                                 new int[] { 18,35,87,10 },
                                new int[] { 20,04,82,47,65 },
                               new int[] { 19,01,23,75,03,34 },
                              new int[] { 88,02,77,73,07,63,67 },
                             new int[] { 99,65,04,28,06,16,70,92 },
                            new int[] { 41,41,26,56,83,40,80,70,33 },
                           new int[] { 41,48,72,33,47,32,37,16,94,29 },
                          new int[] { 53,71,44,65,25,43,91,52,97,51,14 },
                         new int[] { 70,11,33,28,77,73,17,78,39,68,17,57 },
                        new int[] { 91,71,52,38,17,14,91,43,58,50,27,29,48 },
                       new int[] { 63,66,04,68,89,53,67,30,73,16,69,87,40,31 },
                      new int[] { 04,62,98,27,23,09,70,98,73,93,38,53,60,04,23 } };

            var time = Stopwatch.StartNew();
            biggestValue = FindBiggestPath(input, 0, 0);
            var ticks = time.ElapsedTicks;
            Console.WriteLine(biggestValue.ToString());
            Console.WriteLine($"Time: {ticks}");
            
            time = Stopwatch.StartNew();
            biggestValue = GetPath(input);
            ticks = time.ElapsedTicks;
            Console.WriteLine(biggestValue.ToString());
            Console.WriteLine($"Time: {ticks}");


            Console.ReadKey();
        }

        private static int FindBiggestPath(int[][] input, int y, int x)
        {
            int ret = 0;

            if (y < input.GetLength(0) && x < input[y].GetLength(0))
            {
                int myVal = input[y][x];
                int res1 = myVal + FindBiggestPath(input, y + 1, x);
                int res2 = myVal + FindBiggestPath(input, y + 1, x + 1);
                ret = Math.Max(res1, res2);
            }

            return ret;
        }

        private static int GetPath(int[][] param)
        {
            for (var index = param.Length - 2; index >= 0; index -= 1)
            {
                for (var j = 0; j < param[index].Length; j += 1)
                {
                    param[index][j] = MaxValueOfChild(param[index][j], param[index + 1][j], param[index + 1][j + 1]);
                }
            }
            return param[0][0];
        }

        private static int MaxValueOfChild(int parent, int left, int right) =>
            Math.Max(left, right) + parent;
    }
}
