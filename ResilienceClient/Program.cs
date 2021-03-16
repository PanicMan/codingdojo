using System;
using System.Linq;
using System.Threading;

namespace ResilienceClient
{
    class Program
    {
        private static readonly object _door = new object();

        static void Main(string[] args)
        {
            Statistic[] statistics = new Statistic[0];
            var progress = new Progress<DemoProgress>();
            progress.ProgressChanged += (sender, progressArgs) =>
            {
                foreach (var message in progressArgs.Messages)
                {
                    WriteLineInColor(message.Message, message.Color.ToConsoleColor());
                }

                statistics = progressArgs.Statistics;
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            var client = new Client();

            client.ExecuteAsync(cancellationToken, progress).Wait();

            // Keep the console open.
            Console.ReadKey();
            cancellationSource.Cancel();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            // Output statistics.
            int longestDescription = statistics.Max(s => s.Description.Length);
            foreach (Statistic stat in statistics)
            {
                WriteLineInColor(stat.Description.PadRight(longestDescription) + ": " + stat.Value,
                    stat.Color.ToConsoleColor());
            }

            // Keep the console open.
            Console.ReadKey();
        }

        public static void WriteLineInColor(string msg, ConsoleColor color)
        {
            lock (_door)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(msg);
                Console.ResetColor();
            }
        }
    }
}
