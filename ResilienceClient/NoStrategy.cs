using System;
using System.Threading;
using System.Threading.Tasks;

namespace ResilienceClient
{
    class NoStrategy : Strategy
    {
        public override Task<TResult> ExecuteAsync<TResult>(
            Func<CancellationToken, Task<TResult>> action,
            CancellationToken cancellationToken)        
            => action(cancellationToken);
        
    }
}
