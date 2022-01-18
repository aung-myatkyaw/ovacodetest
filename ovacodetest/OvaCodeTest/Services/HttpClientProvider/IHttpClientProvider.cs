using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OvaCodeTest.Services.HttpClientProvider
{
    public interface IHttpClientProvider
    {
        Task<string> GetAsync(string baseURI, string uri, Dictionary<string, string> Headers = null);
        Task<string> PostAsync(string baseURI, string uri, JObject data, Dictionary<string, string> Headers = null);
    }
}
