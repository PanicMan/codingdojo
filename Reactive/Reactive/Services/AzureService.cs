using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Services
{
    public class AzureService
    {
        HttpClient HttpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
        public async Task<IEnumerable<AzureWorkItem>> GetWorkItems(string title)
        {
            var url = $"http://ds-vm-csovcs01:8080/NxCollection/Medication/_odata/v2.0/WorkItems?" +
                $"$select=WorkItemId,Title" +
                $"&$filter=startswith(Title,'{title}')" +
                $"&$expand=Iteration($select=IterationPath)";
            return await WindowsIdentity.RunImpersonated(WindowsIdentity.GetCurrent().AccessToken, async () => { 
                var response = await HttpClient.GetStringAsync(url);
                var a = JsonConvert.DeserializeObject<ODataPayload<AzureWorkItem>>(response);
                return a.Value;
            });
        }
    }
}
