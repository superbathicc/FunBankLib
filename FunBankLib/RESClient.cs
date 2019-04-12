using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

namespace FunBankLib
{
    public class RESTClient
    {
        protected Uri baseUrl = new Uri("http://localhost/");
        protected CookieContainer cookieContainer;
        protected HttpClientHandler httpClientHandler;
        protected HttpClient httpClient;

        public RESTClient(string baseUrl) {
            this.baseUrl = new Uri(baseUrl);
            cookieContainer = new CookieContainer();
            httpClientHandler = new HttpClientHandler() {
                CookieContainer = cookieContainer,
                DefaultProxyCredentials = CredentialCache.DefaultCredentials,
                Proxy = WebRequest.GetSystemWebProxy(),
                UseProxy = true
            };
            httpClient = new HttpClient(httpClientHandler) {
                BaseAddress = this.baseUrl,
            };
        }

        public string GetQueryString(Dictionary<string, string> query) {
            if (query == null || query.Count == 0) {
                return "";
            } else {
                string result = "";
                int count = 0;
                foreach (KeyValuePair<string, string> pair in query) {
                    if (count++ == 0) {
                        result += $"?{HttpUtility.UrlEncode(pair.Key)}={HttpUtility.UrlEncode(pair.Value)}";
                    } else {
                        result += $"&{HttpUtility.UrlDecode(pair.Key)}={HttpUtility.UrlEncode(pair.Value)}";
                    }
                }
                return result;
            }
        }

        public async Task<T> ParseResponse<T>(HttpResponseMessage response) {
            Console.WriteLine($"Got Response {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created) {
                if (response.Content.Headers.ContentType.MediaType == "application/json") {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                } else {
                    Console.WriteLine("was not json");
                    return default;
                }
            } else throw new Exception($"got error response (${response.StatusCode}\n{await response.Content.ReadAsStringAsync()}");
        }

        public async Task<T> Get<T>(string path, Dictionary<string, string> query) {
            var address = new UriBuilder(httpClient.BaseAddress) {
                Path = path,
                Query = GetQueryString(query)
            }.ToString();
            var response = await httpClient.GetAsync(address);
            return await ParseResponse<T>(response);
        }

        public async Task<T> Post<T>(string path, object body) {
            var address = new UriBuilder(httpClient.BaseAddress) {
                Path = path
            }.ToString();
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            Console.WriteLine($"Posting {await content.ReadAsStringAsync()} to {address}");
            var response = await httpClient.PostAsync(address, content);
            return await ParseResponse<T>(response);
        }

        protected void SetAuthorization(string key, string hash) {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(key, hash);
        }
    }
}
