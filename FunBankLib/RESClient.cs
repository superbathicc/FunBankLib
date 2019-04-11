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
                CookieContainer = cookieContainer
            };
            httpClient = new HttpClient(httpClientHandler) {
                BaseAddress = this.baseUrl
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
            response.EnsureSuccessStatusCode();
            IEnumerable<string> values;
            if (response.Headers.TryGetValues("Content-Type", out values)) {
                if (new List<string>(values).Contains("application/json")) {
                    return DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                } else {
                    return default;
                }
            } else {
                return default;
            }
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
            var content = new StringContent(SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(address, content);
            return await ParseResponse<T>(response);
        }

        protected virtual string SerializeObject(object o) {
            return JsonConvert.SerializeObject(o, Formatting.Indented);
        }

        protected virtual T DeserializeObject<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected void SetAuthorization(string key, string hash) {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(key, hash);
        }
    }
}
