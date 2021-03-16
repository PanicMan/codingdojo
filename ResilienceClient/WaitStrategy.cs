using System;
using System.Threading;
using System.Threading.Tasks;

namespace ResilienceClient
{
    class WaitStrategy : Strategy
    {
        public int WaitTime { get; set; } = 300;

        public override async Task<TResult> ExecuteAsync<TResult>(
            Func<CancellationToken, Task<TResult>> action,
            CancellationToken cancellationToken)
        {
            Canc


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
                    ex = e;
                    await Task.Delay(Wait);
                }
            }

            throw ex ?? new InvalidOperationException();
        }
    }
}
