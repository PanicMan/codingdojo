using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Reactive.Services
{
    public class ODataPayload<T>
    {
        [JsonProperty(PropertyName = "odata.context")]
        public string Context { get; set; }
        [JsonProperty(PropertyName = "value")]
        public IEnumerable<T> Value { get; set; }
    }
}
