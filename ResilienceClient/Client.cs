using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ResilienceClient
{
    class Client
    {
        private int _totalRequests;
        private int _eventualSuccesses;
        private int _retries;
        private int _eventualFailures;

        public string Description =>
            "This demo demonstrates how a faulting server behaves.";

        public async Task ExecuteAsync(CancellationToken cancellationToken, IProgress<DemoProgress> progress)
        {
            if (cancellationToken == null) throw new ArgumentNullException(nameof(cancellationToken));
            if (progress == null) throw new ArgumentNullException(nameof(progress));

            _eventualSuccesses = 0;
            _retries = 0;
            _eventualFailures = 0;

            progress.Report(ProgressWithMessage(typeof(Client).Name));
            progress.Report(ProgressWithMessage("======"));
            progress.Report(ProgressWithMessage(string.Empty));

            var internalCancel = false;
            _totalRequests = 0;

            var client = new HttpClient();

            // Do the following until a key is pressed
            while (!internalCancel && !cancellationToken.IsCancellationRequested)
            {
                _totalRequests++;

                try
                {
                    // Make a request and get a response string

                    //Resiliance
                    var doRetry = new RetryStrategy() { Retries = 15, Wait = 50 };
                    var msg = await new NoStrategy().ExecuteAsync((c) => doRetry.ExecuteAsync((c) => client.GetStringAsync(Configuration.WEB_API_ROOT + "/api/values/"), c), cancellationToken);
                    _retries += doRetry.Tried;

                    // TODO: Get the values
                    //var msg = await new NoStrategy().ExecuteAsync((c) => Task.FromResult<string>("mock"), cancellationToken);

                    // Display the response message on the console
                    progress.Report(ProgressWithMessage("Response : " + msg, Color.Green));
                    _eventualSuccesses++;
                }
                catch (Exception e)
                {
                    progress.Report(ProgressWithMessage(
                        "Request " + _totalRequests + " eventually failed with: " + e.Message, Color.Red));
                    _eventualFailures++;
                }

                // Wait half second
                await Task.Delay(TimeSpan.FromSeconds(0.5), cancellationToken);

                internalCancel = Console.KeyAvailable;
            }
        }

        public Statistic[] LatestStatistics => new[]
        {
            new Statistic("Total requests made", _totalRequests),
            new Statistic("Requests which eventually succeeded", _eventualSuccesses, Color.Green),
            new Statistic("Retries made to help achieve success", _retries, Color.Yellow),
            new Statistic("Requests which eventually failed", _eventualFailures, Color.Red),
        };

        public DemoProgress ProgressWithMessage(string message)
            => new DemoProgress(LatestStatistics, new ColoredMessage(message, Color.Default));


        public DemoProgress ProgressWithMessage(string message, Color color)
            => new DemoProgress(LatestStatistics, new ColoredMessage(message, color));


        public DemoProgress ProgressWithMessages(IEnumerable<ColoredMessage> messages)
            => new DemoProgress(LatestStatistics, messages);

    }
}
