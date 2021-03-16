using System;
using System.Threading;
using System.Threading.Tasks;

namespace ResilienceClient
{
    class RetryStrategy : Strategy
    {
        public int Retries { get; set; } = 10;
        public int Wait { get; set; } = 100;
        public int Tried { get; set; } = 0;

        public override async Task<TResult> ExecuteAsync<TResult>(
            Func<CancellationToken, Task<TResult>> action,
            CancellationToken cancellationToken)
        {
            int counter = 0;
            Exception ex = null;
            while (counter++ < Retries)
            {
                try
                {
                    TResult result = await action(cancellationToken);
                    return result;
                }
                catch (Exception e) when (!typeof(OperationCanceledException).IsAssignableFrom(e.GetType()))
                {
                    Tried++;
                    ex = e;
                    await Task.Delay(Wait);
                }
            }

            throw ex ?? new InvalidOperationException();
        }
    }
}
