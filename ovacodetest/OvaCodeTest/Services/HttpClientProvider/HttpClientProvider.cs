using OvaCodeTest.Exceptions;
using OvaCodeTest.Services.HttpClientProvider;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientProvider))]
namespace OvaCodeTest.Services.HttpClientProvider
{
    class HttpClientProvider : IHttpClientProvider
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public HttpClientProvider()
        {
            #region custom serialze setting
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
            #endregion
        }

        #region Get
        public async Task<string> GetAsync(string baseURI, string uri, Dictionary<string, string> Headers = null)
        {
            HttpClient httpClient = CreateHttpClient(baseURI);
            if (Headers != null)
            {
                AddHeaderParameter(httpClient, Headers);
            }
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            string returnString;
            try
            {
                await HandleResponse(response);
                returnString = await response.Content.ReadAsStringAsync();
            }
            catch (ServiceException ex)
            {
                var _errMsg = await response.Content.ReadAsStringAsync();
                returnString = _errMsg;
                Debug.WriteLine(ex.Message);
            }

            return returnString;
        }
        #endregion

        #region Post
        public async Task<string> PostAsync(string baseURI, string uri, JObject data, Dictionary<string, string> Headers = null)
        {
            HttpClient httpClient = CreateHttpClient(baseURI);

            if (Headers != null)
            {
                AddHeaderParameter(httpClient, Headers);
            }
            StringContent stringContent = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage();
            string returnString;
            try
            {
                response = await httpClient.PostAsync(uri, stringContent).ConfigureAwait(false);
                await HandleResponse(response);
                returnString = await response.Content.ReadAsStringAsync();
            }
            catch (ServiceException)
            {
                string _errMsg = await response.Content.ReadAsStringAsync();
                returnString = _errMsg;
            }
            catch (Exception ex)
            {
                string _errMsg = await response.Content.ReadAsStringAsync();
                returnString = _errMsg;
                Debug.WriteLine(ex.Message);
            }

            return returnString;
        }
        #endregion        

        private HttpClient CreateHttpClient(string baseURI)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(@baseURI)
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        private void AddHeaderParameter(HttpClient httpClient, Dictionary<string, string> Headers)
        {
            if (httpClient == null)
                return;

            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        // response handler
        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ServiceException(content);
                }
                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }


    }
}
