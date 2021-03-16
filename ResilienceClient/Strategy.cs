using System;
using System.Threading;
using System.Threading.Tasks;

namespace ResilienceClient
{
    abstract class Strategy
    {
        public abstract Task<TResult> ExecuteAsync<TResult>(
            Func<CancellationToken, Task<TResult>> action,
            CancellationToken cancellationToken);
    }
}
