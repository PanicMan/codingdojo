using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reactive.Services
{
    public interface IDrugSearch
    {
        Task<IEnumerable<Drug>> SearchDrug(string name, CancellationToken token);
    }
}
